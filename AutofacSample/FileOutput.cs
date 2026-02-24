namespace AutofacSample;
public class FileOutput : ITextWriter
{
    public void Write(string content)
    {
        File.AppendAllText("C:\\temp\\Autofac.txt", content);
    }
}
