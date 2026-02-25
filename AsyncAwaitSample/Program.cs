
namespace AsyncAwaitSample;

internal class Program
{
    static async Task Main(string[] args)
    {
        ConsoleWriteLine("Progamm wird gestartet");
        List<WorkItem> workItems = new List<WorkItem>();

        List<int> list = new List<int>();
        for (int i = 0; i < 15_000; i++)
        {
            list.Add(i);
        }

        var tasks = new List<Task<List<WorkItem>>>();
        //{
        //    CalculateSomething("A", 500),
        //    CalculateSomething("B", 1500),
        //    CalculateSomething("C", 200)
        //}; // Alle Tasks werden hier sofort gestartet

        for (int i = 0; i < list.Count / 200; i++)
        {
            Task<List<WorkItem>> t = CallRestApi(list.Skip(i * 200).Take(200).ToList());
            //t.ContinueWith(t =>
            //{
            //    // Call other api mit DependentIds
            //});
            tasks.Add(t); 
        }

        while (tasks.Any())
        {
            Task<List<WorkItem>> terminatedTask = await Task.WhenAny(tasks);
            workItems.AddRange(terminatedTask.Result);
            tasks.Remove(terminatedTask);
        }


    }

    private static async Task<List<WorkItem>> CallRestApi(List<int> list)
    {
        // REST Call
        await Task.Delay(100);
        Console.WriteLine(list.Max());
        var result = new List<WorkItem>();
        Random r = new Random();
        foreach (int i in list)
        {
            result.Add(new WorkItem() { Id = i, Name = $"WorkItem {i}", DependentIds = [r.Next(50, 2000), r.Next(50, 2000)] });
        }

        return result;
    }


    private static async Task<int> CalculateSomething(string prozess, int delay)
    {
        ConsoleWriteLine($"Prozess {prozess} wurde gestartet");
        for (int i = 0; i < 5; i++)
        {
            ConsoleWriteLine($"Aufruf {i} von Prozess {prozess} gestartet");
            await Task.Delay( delay );
            ConsoleWriteLine($"Aufruf {i} von Prozess {prozess} beendet");
        }

        ConsoleWriteLine($"Prozess {prozess} gibt Ergebnis zurück");
        return 42 * delay;
    }

    private static void ConsoleWriteLine(string message)
    {
        int threadId = Thread.CurrentThread.ManagedThreadId;
        Console.ForegroundColor = threadId == 1 ? ConsoleColor.White : ConsoleColor.Cyan;
        Console.WriteLine($"Thread {threadId} {message}");
    }

}
