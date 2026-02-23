
namespace ReflectionSample;
public class Person : ICloneable
{
    [CloneRule(DontClone = true)]
    public int Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    [CloneRule(Format = CasingFormat.UpperCase)]
    public string LastName { get; set; } = string.Empty;

    public DateOnly? DateofBirth { get; set; }

    public object Clone()
    {
        Type personType = this.GetType();

        Person clone = new Person();

        foreach (var item in personType.GetProperties())
        {
            item.SetValue(clone, item.GetValue(this));
        }
        return clone;
    }

    public string SayMyName()
    {
        return $"My name is {FirstName} {LastName}";
    }

    private string SayMyNamePrivate()
    {
        return $"My name is {FirstName} {LastName} (very private)";
    }
}
