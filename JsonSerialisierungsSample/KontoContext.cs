using System.Text.Json.Serialization;

namespace JsonSerialisierungsSample;

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(List<Konto>))]
internal partial class KontoContext : JsonSerializerContext
{
}
