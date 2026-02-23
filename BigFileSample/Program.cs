
namespace BigFileSample;

internal class Program
{
    static void Main(string[] args)
    {
        string path = @"C:\Windows";

        ShowLargeFilesWithoutLinq(path);
        Console.WriteLine("----------------------------------------------------------------------");
        ShowLargeFilesWithLinq(path);
    }

    private static void ShowLargeFilesWithLinq(string path)
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(path);

        var files = directoryInfo.GetFiles()
            .OrderByDescending(f => f.Length)
            .Take(5);

        foreach (var file in files)
        {
            Console.WriteLine($"{file.FullName} ist {file.Length} Bytes groß!");
        }
    }

    private static void ShowLargeFilesWithoutLinq(string path)
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(path);
        FileInfo[] files = directoryInfo.GetFiles();

        Array.Sort(files, new FileInfoComparer());

        int length = 5;

        if (files.Length < length)
        {
            length = files.Length;
        }

        for (int i = 0; i < length; i++)
        {
            Console.WriteLine($"{files[i].FullName} ist {files[i].Length} Bytes groß!");
        }
    }
}
