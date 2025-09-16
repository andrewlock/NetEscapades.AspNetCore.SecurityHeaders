using System;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure;

/// <summary>
/// Utility class for registering <see cref="CustomHeaderOptions"/> configuration actions
/// </summary>
/// <param name="configure">The method to execute</param>
internal class ConfigureHeaderOptions(Action<CustomHeaderOptions, IServiceProvider> configure)
{
    private readonly Action<CustomHeaderOptions, IServiceProvider> _configure = configure;

    /// <summary>
    /// Configure the provided <see cref="CustomHeaderOptions"/>
    /// </summary>
    /// <param name="options">The <see cref="CustomHeaderOptions"/> to configure</param>
    /// <param name="provider">The <see cref="IServiceProvider"/> to use</param>
    public void Configure(CustomHeaderOptions options, IServiceProvider provider)
    {
        _configure(options, provider);
    }
}