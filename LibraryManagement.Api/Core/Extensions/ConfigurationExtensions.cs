using LibraryManagement.Api.Core.Exceptions;

namespace LibraryManagement.Api.Core.Extensions;

public static class ConfigurationExtensions
{
    public static IConfigurationSection GetSectionOrThrow(this IConfiguration configuration, string path)
    {
        var section = configuration.GetSection(path);
        return section.Exists() ? section : throw new ConfigurationSectionNotFoundException(path);
    }

    public static string GetValueOrThrow<T>(this T configuration, string path) where T : IConfiguration
    {
        var section = configuration.GetSectionOrThrow(path);
        return section.Value ?? throw new ConfigurationSectionValueIsMissingException(path);
    }
}