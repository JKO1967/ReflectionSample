namespace ReflectionSample;
public class CloneRuleAttribute :Attribute
{
    public bool DontClone { get; set; }

    public CasingFormat Format { get; set; }
}

public enum CasingFormat
{
    None,
    UpperCase,
    LowerCase
}
