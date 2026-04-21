using AppInfra.Serialization;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AppInfra.Kafka;

public sealed class KafkaConsumerHostedService<TEvent, TDeserializer> : BackgroundService
    where TDeserializer : class, IEventDeserializer
{
    private readonly IOptions<KafkaConsumerOptions> _options;
    private readonly TDeserializer _deserializer;
    private readonly IKafkaEventHandler<TEvent> _handler;
    private readonly ILogger<KafkaConsumerHostedService<TEvent, TDeserializer>> _logger;

    public KafkaConsumerHostedService(
        IOptions<KafkaConsumerOptions> options,
        TDeserializer deserializer,
        IKafkaEventHandler<TEvent> handler,
        ILogger<KafkaConsumerHostedService<TEvent, TDeserializer>> logger)
    {
        _options = options;
        _deserializer = deserializer;
        _handler = handler;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var options = _options.Value;

        var config = new ConsumerConfig
        {
            BootstrapServers = options.BootstrapServers,
            GroupId = options.Username,
            SaslUsername = options.Username,
            SaslPassword = options.Password,
            SecurityProtocol = SecurityProtocol.SaslPlaintext,
            SaslMechanism = SaslMechanism.ScramSha256,
            AutoOffsetReset = options.AutoOffsetReset,
            EnableAutoCommit = true,
        };

        using var consumer = new ConsumerBuilder<string, byte[]>(config)
            .SetValueDeserializer(Deserializers.ByteArray)
            .Build();

        consumer.Subscribe(options.Topic);
        _logger.LogInformation(
            "Kafka consumer subscribed. Topic={Topic}, GroupId={GroupId}",
            options.Topic,
            options.Username);

        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                ConsumeResult<string, byte[]>? result;
                try
                {
                    result = consumer.Consume(stoppingToken);
                }
                catch (ConsumeException consumeException)
                {
                    _logger.LogError(consumeException, "Kafka consume error.");
                    continue;
                }

                if (result is null || result.IsPartitionEOF)
                {
                    continue;
                }

                try
                {
                    var @event = _deserializer.Deserialize<TEvent>(result.Message.Value);
                    
                    await _handler.HandleAsync(@event, stoppingToken).ConfigureAwait(false);
                    
                    consumer.Commit(result);
                }
                catch (Exception exception) when (!stoppingToken.IsCancellationRequested)
                {
                    _logger.LogError(
                        exception,
                        "Failed to process Kafka event. Topic={Topic}, Partition={Partition}, Offset={Offset}",
                        result.Topic,
                        result.Partition.Value,
                        result.Offset.Value);
                }
            }
        }
        finally
        {
            try
            {
                consumer.Close();
            }
            catch (Exception exception)
            {
                _logger.LogDebug(exception, "Error closing Kafka consumer.");
            }
        }
    }
}
