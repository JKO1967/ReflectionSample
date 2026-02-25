
namespace ThreadingSample;

internal class Program
{
    private static bool _printed = false;
    private static object _lock = new object();
    private static int result = 0;

    static void Main(string[] args)
    {
        Thread t1 = new Thread(PrintOnce);
        t1.Name = "Thread 1";
        //t1.IsBackground = true;
        Thread t2 = new Thread(PrintOnce);
        t2.Name = "Thread 2";
        //t2.IsBackground = true;

        t1.Start();
        t2.Start();
        // t1.Join();

        // optimierteste Version einen Background Thread zu erstellen (mir fehlt aber dann die Kontrolle)
        //ThreadPool.QueueUserWorkItem(PrintOnceBackground);
    }

    private static void PrintOnceBackground(object? state)
    {
        PrintOnce();
    }

    private static void PrintOnce()
    {
        lock (_lock)
        {
            //Monitor.Enter(_lock);

            if (!_printed)
            {
                Console.WriteLine($"Print in Thread {Thread.CurrentThread.Name} at {DateTime.Now.ToLongTimeString()}");
                _printed = true;
            }
        }

        for (int i = 0; i < 100_000_000; i++)
        {
            //result++;
            Interlocked.Increment(ref result);
        }

        Console.WriteLine(result);
        Console.WriteLine($"Thread {Thread.CurrentThread.Name} at {DateTime.Now.ToLongTimeString()}");
        //Monitor.Exit(_lock);
    }
}
