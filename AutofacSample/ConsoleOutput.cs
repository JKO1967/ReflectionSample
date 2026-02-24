namespace AutofacSample;
public class ConsoleOutput : ITextWriter
{
    public void Write(string content)
    {
        Console.WriteLine(content);
    }
}
