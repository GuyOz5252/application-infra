using AppInfra.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AppInfra.Kafka;

public static class KafkaServiceCollectionExtensions
{
    public static IServiceCollection AddKafkaConsumer<TEvent, THandler, TDeserializer>(this IServiceCollection services)
        where THandler : class, IKafkaEventHandler<TEvent>
        where TDeserializer : class, IEventDeserializer
    {
        services.TryAddSingleton<TDeserializer>();
        services.AddSingleton<THandler>();
        services.AddSingleton<IKafkaEventHandler<TEvent>>(sp => sp.GetRequiredService<THandler>());
        services.AddHostedService<KafkaConsumerHostedService<TEvent, TDeserializer>>();
        return services;
    }

    public static IServiceCollection AddKafkaConsumer<TEvent, THandler, TDeserializer>(
        this IServiceCollection services,
        Action<KafkaConsumerOptions> configure)
        where THandler : class, IKafkaEventHandler<TEvent>
        where TDeserializer : class, IEventDeserializer
    {
        ArgumentNullException.ThrowIfNull(configure);
        services.Configure(configure);
        return services.AddKafkaConsumer<TEvent, THandler, TDeserializer>();
    }
}
