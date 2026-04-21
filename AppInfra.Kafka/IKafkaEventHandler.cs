namespace AppInfra.Kafka;

public interface IKafkaEventHandler<in TEvent>
{
    Task HandleAsync(TEvent @event, CancellationToken cancellationToken);
}
