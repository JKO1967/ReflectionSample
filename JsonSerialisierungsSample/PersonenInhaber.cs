namespace JsonSerialisierungsSample;
public class PersonenInhaber:IInhaber
{
    public string Name { get; set; } = string.Empty;
    public string Adresse { get; set; } = string.Empty;

    public DateOnly? Geburtsdatum { get; set; }
}
