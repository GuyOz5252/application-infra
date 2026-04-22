namespace ApplicationInfra.Serialization.Abstract;

public interface IEventSerializer
{
    byte[] Serialize<TEvent>(TEvent @event);
}
