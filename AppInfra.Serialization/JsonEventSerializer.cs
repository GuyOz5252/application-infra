using System.Text.Json;

namespace AppInfra.Serialization;

public sealed class JsonEventSerializer : IEventSerializer
{
    private readonly JsonSerializerOptions _options;

    public JsonEventSerializer(JsonSerializerOptions? options = null)
    {
        _options = options ?? new JsonSerializerOptions(JsonSerializerDefaults.Web);
    }

    public byte[] Serialize<TEvent>(TEvent @event)
    {
        return JsonSerializer.SerializeToUtf8Bytes(@event, _options);
    }
}
