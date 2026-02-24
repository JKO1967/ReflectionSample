namespace AutofacSample;
public class TodayWriter : IDateWriter
{
    private readonly ITextWriter _writer;

    public TodayWriter(ITextWriter textWriter)
    {
        _writer = textWriter;
    }

    public void WriteData()
    {
        string content = DateTime.Today.ToShortDateString();
        _writer.Write(content);
    }
}
