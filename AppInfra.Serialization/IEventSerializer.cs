namespace AppInfra.Serialization;

public interface IEventSerializer
{
    byte[] Serialize<TEvent>(TEvent @event);
}
