
using System.Collections.Concurrent;
using System.Data;
using System.Diagnostics;

namespace ParallelSample;

internal class Program
{
    static void Main(string[] args)
    {
        //ParallelForSample();

        //ParallelForEachSample();

        //ParallelInvokeSample();
        PraktischesBeispiel();
    }

    private static void PraktischesBeispiel()
    {
        ConcurrentBag<Testset> data = new ConcurrentBag<Testset>();
        List<Testset> list = new List<Testset>();

        for (int i = 0; i < 50_000; i++)
        {
            data.Add(new Testset() { Data = i * 2.27 });
            list.Add(new Testset() { Data = i * 2.27 });
        }


        Parallel.ForEach(data, d =>
        {
            d.Result = d.Data * 1.19;
        });

        foreach (var d in data.Skip(49500).Take(500))
        {
            Console.WriteLine($"Data: {d.Data} - Result: {d.Result}");
        }

        //var results2 = dt.AsEnumerable()
        //         .AsParallel()
        //         .Select(r => (double)r["Spalte1"] * 1.19)
        //         .ToArray();

        //for (int i = 0; i < results2.Length; i++)
        //    dt.Rows[i]["Spalte2"] = results2[i];
        // dt.Columns["Spalte2"].Expression = "Spalte1 * 1.19";

    }

    private static void ParallelInvokeSample()
    {
        Stopwatch sw = Stopwatch.StartNew();

        Parallel.Invoke(new ParallelOptions() { MaxDegreeOfParallelism = 6 }, AufgabeA, AufgabeB, AufgabeC);
        //AufgabeA();
        //AufgabeB();
        //AufgabeC();

        sw.Stop();

        Console.WriteLine($"Berechnung hat gedauert: {sw.Elapsed.TotalSeconds}");
    }

    private static void AufgabeA()
    { 
        Thread.Sleep(2000);
        Console.WriteLine("A fertig");
    }

    private static void AufgabeB()
    {
        Thread.Sleep(1000);
        Console.WriteLine("B fertig");
    }

    private static void AufgabeC()
    {
        Thread.Sleep(6000);
        Console.WriteLine("C fertig");
    }

    private static void ParallelForEachSample()
    {
        string[] names = { "Jürgen", "Markus", "Alexander", "Pascal", "Christian" };
        Parallel.ForEach(names, x =>
        {
            Console.WriteLine(x);
        });
    }

    private static void ParallelForSample()
    {
        Stopwatch sw = Stopwatch.StartNew();
        Console.WriteLine("Traditionelle Ausführung");

        string result = "";
        string letters = "Mittlerweile ist die Sonne da und der regen hat aufgehört";

        for (int i = 0; i < 20_000; i++)
        {
            result += letters;
        }
        sw.Stop();

        Console.WriteLine($"Traditionelle Berechnung hat gedauert: {sw.Elapsed.TotalSeconds}");

        sw.Restart();
        result = "";

        Parallel.For(0, 20_000, new ParallelOptions() { MaxDegreeOfParallelism = 4 } , i =>
        {
            result += letters;
        });
        sw.Stop();

        Console.WriteLine($"Parallele Berechnung hat gedauert: {sw.Elapsed.TotalSeconds}");
    }
}
