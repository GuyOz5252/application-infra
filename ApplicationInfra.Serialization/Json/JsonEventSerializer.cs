using System.Text.Json;
using ApplicationInfra.Serialization.Abstract;

namespace ApplicationInfra.Serialization.Json;

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
