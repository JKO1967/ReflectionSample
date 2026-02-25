using System.Text.Json.Serialization;

namespace JsonSerialisierungsSample;

[JsonDerivedType(typeof(PersonenInhaber), "pi")]
[JsonDerivedType(typeof(FirmenInhaber), "fi")]
public interface IInhaber
{
    public string Name { get; set; }

    public string Adresse { get; set; }
}
