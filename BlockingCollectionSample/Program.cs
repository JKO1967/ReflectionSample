using System.Collections.Concurrent;

namespace BlockingCollectionSample;

internal class Program
{
    private static BlockingCollection<string> _products = new BlockingCollection<string>();

    static void Main(string[] args)
    {
        // ThreadPool.QueueUserWorkItem(Produce);
        Task produce = Task.Run(Produce);
        Task consume = Task.Run(Consume);

        // Hauptprogramm wird blockiert bis beide tasks fertig sind
        Task.WaitAll(produce, consume);
    }


    static async Task Produce()
    {
        for (int i = 1; i <= 100; i++)
        {
            _products.Add($"Product {i}");
            Console.WriteLine($"Product {i} wurde erstellt! fertig zum Verkauf!");
            await Task.Delay(50);
            if (i % 10 == 0)
            {
                await Task.Delay(2000);
            }
        }
        // Befüllen der Liste beendet
        _products.CompleteAdding();
        Console.WriteLine("Feierabend");
    }

    static async Task Consume()
    {
        foreach (var item in _products.GetConsumingEnumerable())
        {
            Console.WriteLine($"{item} wurde verkauft");
            await Task.Delay(50);
        }

        Console.WriteLine("Verkauf beendet");
    }
}
