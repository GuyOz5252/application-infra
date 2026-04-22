using ApplicationInfra.Serialization.Abstract;
using Google.Protobuf;

namespace ApplicationInfra.Serialization.Protobuf;

public sealed class ProtobufEventSerializer : IEventSerializer
{
    public byte[] Serialize<TEvent>(TEvent @event)
    {
        if (@event is not IMessage message)
        {
            throw new ArgumentException(
                $"Type {typeof(TEvent).FullName} must be a protobuf message ({nameof(IMessage)}).",
                nameof(@event));
        }

        return message.ToByteArray();
    }
}
