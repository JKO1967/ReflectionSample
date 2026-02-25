namespace JsonSerialisierungsSample;
public class FirmenInhaber : IInhaber
{
    public string Name { get ; set ; } = string.Empty;
    public string Adresse { get; set; } = string.Empty;

    public string Handelsregisterauszug { get; set; } = string.Empty;
}
