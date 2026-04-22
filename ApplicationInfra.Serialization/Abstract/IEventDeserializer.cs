namespace ApplicationInfra.Serialization.Abstract;

public interface IEventDeserializer
{
    TEvent Deserialize<TEvent>(ReadOnlyMemory<byte> data);
}
