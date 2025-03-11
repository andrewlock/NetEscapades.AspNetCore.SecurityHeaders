using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.EnvironmentInfo;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

[ShutdownDotNetAfterServerBuild]
class Build : NukeBuild
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode
    public static int Main() => Execute<Build>(x => x.Test, x => x.Pack);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    readonly string Version = "1.0.0-preview.4"; 
    
    [Solution(GenerateProjects = true)] readonly Solution Solution;

    AbsolutePath SourceDirectory => RootDirectory / "src";
    AbsolutePath TestsDirectory => RootDirectory / "test";
    AbsolutePath ArtifactsDirectory => RootDirectory / "artifacts";
    AbsolutePath OuptutPackagesDirectory => ArtifactsDirectory / "packages";
    AbsolutePath TestResultsDirectory => ArtifactsDirectory / "results";
    AbsolutePath SbomDirectory => ArtifactsDirectory / "sboms";

    [Parameter] readonly string GithubToken;
    [Parameter] readonly string NuGetToken;
    const string NugetOrgUrl = "https://api.nuget.org/v3/index.json";
    bool IsTag => GitHubActions.Instance?.Ref.StartsWith("refs/tags/") ?? false;

    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
            SourceDirectory.GlobDirectories("**/bin", "**/obj").ForEach(d => d.DeleteDirectory());
            TestsDirectory.GlobDirectories("**/bin", "**/obj").ForEach(d => d.DeleteDirectory());
            ArtifactsDirectory.CreateOrCleanDirectory();
        });

    Target Restore => _ => _
        .Executes(() =>
        {
            DotNetRestore(s => s
                .SetProperty("Version", Version)
                .SetProjectFile(Solution));
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetBuild(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .SetProperty("ContinuousIntegrationBuild", "true")
                .SetProperty("Version", Version)
                .EnableNoRestore());
        });

    Target Test => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            DotNetTest(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .SetProperty("Version", Version)
                .When(IsServerBuild, x => x
                    .SetLoggers("trx")
                    .SetResultsDirectory(TestResultsDirectory))
                .EnableNoBuild()
                .EnableNoRestore());
        });

    Target Pack => _ => _
        .DependsOn(Compile)
        .After(Test)
        .Produces(OuptutPackagesDirectory)
        .Executes(() =>
        {
            var projects = SourceDirectory.GlobFiles("**/*.csproj");

            DotNetPack(s => s
                .SetConfiguration(Configuration)
                .SetOutputDirectory(OuptutPackagesDirectory)
                .SetProperty("ContinuousIntegrationBuild", "true")
                .SetProperty("Version", Version)
                .EnableNoBuild()
                .EnableNoRestore()
                .CombineWith(projects, (x, project) => x
                    .SetProject(Solution.AllProjects.SingleOrError(x => x == project,
                        $"Could not find project with name {project}"))));
        });

    Target PushToNuGet => _ => _
        .DependsOn(Compile)
        .OnlyWhenStatic(() => IsTag && IsServerBuild && IsWin)
        .Requires(() => NuGetToken)
        .After(Pack, GenerateSbom)
        .Executes(() =>
        {
            var packages = OuptutPackagesDirectory.GlobFiles("*.nupkg");
            DotNetNuGetPush(s => s
                .SetApiKey(NuGetToken)
                .SetSource(NugetOrgUrl)
                .EnableSkipDuplicate()
                .CombineWith(packages, (x, package) => x
                    .SetTargetPath(package)));
        });

    Target GenerateSbom => _ => _
        .After(Compile, Pack)
        .Executes(() =>
        {
            DotNetToolRestore();

            var projects = new[]
            {
                Solution.src.NetEscapades_AspNetCore_SecurityHeaders,
                Solution.src.NetEscapades_AspNetCore_SecurityHeaders_TagHelpers,
            };
            foreach (var project in projects)
            {
                var projectName = project.Name;
                DotNet($"""tool run dotnet-CycloneDX "{project}" --json --recursive --disable-package-restore """ +
                       $"""--set-name {projectName} --set-version {Version} """ +
                       $"""--base-intermediate-output-path "{ArtifactsDirectory}" """ +
                       $"""--output "{SbomDirectory}" """ +
                       $"""--filename {projectName.Replace('.', '-').ToLowerInvariant()}.bom.json --set-type library""");
            }
        });
}