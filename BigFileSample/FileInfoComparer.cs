namespace BigFileSample;
public class FileInfoComparer : IComparer<FileInfo>
{
    public int Compare(FileInfo? x, FileInfo? y)
    {
        if (x == null && y == null) return 0;
        if (x == null) return -1;
        if (y == null) return 1;
        return y.Length.CompareTo(x.Length);
    }
}
