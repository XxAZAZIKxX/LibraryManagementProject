using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using LibraryManagement.Api.Config;

namespace LibraryManagement.Api.Core.Extensions;

public static class ServiceProviderExtensions
{
    [RequiresUnreferencedCode($"Loading configs which implements {nameof(IConfig)} from assemlies")]
    public static T LoadConfigsFromAssembly<T>(this T services, params Assembly[] assemblies) where T : IServiceCollection
    {
        if (assemblies.Length is 0) assemblies = [Assembly.GetExecutingAssembly()];
        var added = new HashSet<Type>();
        foreach (var assembly in assemblies)
        {
            var types = assembly.GetTypes()
                .Where(p => typeof(IConfig).IsAssignableFrom(p))
                .Where(p => p.IsInterface is false)
                .Where(p => p.IsAbstract is false);

            foreach (var type in types)
            {
                if (added.Contains(type)) continue;
                services.AddSingleton(type);
                added.Add(type);
            }
        }

        return services;
    }
}