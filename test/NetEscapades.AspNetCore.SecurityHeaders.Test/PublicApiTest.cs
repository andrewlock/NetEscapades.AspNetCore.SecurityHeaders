using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using PublicApiGenerator;
using VerifyXunit;
using Xunit;

namespace NetEscapades.AspNetCore.SecurityHeaders.Test;

[UsesVerify]
public class PublicApiTest
{
    [Fact]
    public Task PublicApiHasNotChanged()
    {
        var assembly = typeof(HeaderPolicyCollection).Assembly;
        var options = new ApiGeneratorOptions
        {
            ExcludeAttributes =
            [
                typeof(InternalsVisibleToAttribute).FullName,
                "System.Runtime.CompilerServices.IsByRefLike",
                typeof(TargetFrameworkAttribute).FullName,
            ],
            DenyNamespacePrefixes = []
        };

        var publicApi = assembly.GeneratePublicApi(options);

        return Verifier.Verify(publicApi);
    }
}