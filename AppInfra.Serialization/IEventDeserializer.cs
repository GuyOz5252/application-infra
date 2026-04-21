namespace AppInfra.Serialization;

public interface IEventDeserializer
{
    TEvent Deserialize<TEvent>(ReadOnlyMemory<byte> data);
}
