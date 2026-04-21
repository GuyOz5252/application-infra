using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AppInfra.Serialization;

public static class SerializationServiceCollectionExtensions
{
    public static IServiceCollection AddJsonEventSerialization(this IServiceCollection services)
    {
        services.TryAddSingleton<JsonEventDeserializer>();
        services.TryAddSingleton<JsonEventSerializer>();
        return services;
    }
}
