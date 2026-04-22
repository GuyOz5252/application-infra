using Microsoft.Extensions.Logging;

namespace ApplicationInfra.Messaging.Kafka.Loggers;

internal static partial class KafkaLogger
{
    [LoggerMessage(Level = LogLevel.Debug, Message = "Kafka producer flush error. ProducerName={ProducerName}")]
    internal static partial void ProducerFlushError(ILogger logger, Exception exception, string producerName);

    [LoggerMessage(Level = LogLevel.Information, Message = "Kafka consumer {ConsumerName} subscribed. Topic={Topic}, GroupId={GroupId}")]
    internal static partial void ConsumerSubscribed(ILogger logger, string consumerName, string topic, string groupId);

    [LoggerMessage(Level = LogLevel.Error, Message = "Kafka consume error. ConsumerName={ConsumerName}")]
    internal static partial void ConsumeError(ILogger logger, Exception exception, string consumerName);

    [LoggerMessage(Level = LogLevel.Error, Message = "Failed to process Kafka event. ConsumerName={ConsumerName}, Destination={Destination}, Partition={Partition}, Offset={Offset}")]
    internal static partial void EventProcessingFailed(ILogger logger, Exception exception, string consumerName, string destination, int partition, long offset);

    [LoggerMessage(Level = LogLevel.Debug, Message = "Error closing Kafka consumer.")]
    internal static partial void ConsumerCloseError(ILogger logger, Exception exception);
}
