using SampleInterface;

namespace SampleLibrary;

public class ToUpperWriter : ISampleWriter
{
    public string Write(string input)
    {
        return input.ToUpper();
    }
}
