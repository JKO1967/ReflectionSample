namespace ReflectionSample;
public class Person : CloneableObject, IName
{
    [CloneRule(DontClone = true)]
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    [CloneRule(Format = CasingFormat.UpperCase)]
    public string LastName { get; set; } = string.Empty;

    public DateOnly? DateofBirth { get; set; }

    

    public string SayMyName()
    {
        return $"My name is {Name} {LastName}";
    }

    private string SayMyNamePrivate()
    {
        return $"My name is {Name} {LastName} (very private)";
    }
}
