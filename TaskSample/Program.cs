namespace TaskSample;

internal class Program
{
    static void Main(string[] args)
    {
        Task t = new Task(Test);
        t.Start();
       
        Console.WriteLine("Die Methode Test wurde gestartet");

        // für den Aufruf mit Parameter
        Task<int> t2 = Task.Run(() => CalculateSomething(2));

        Task factory = Task.Factory.StartNew(Test);

        Console.WriteLine(t2.Result);  // blockierender Aufruf 
        factory.Wait();  // blockierender Aufruf der auf die Abarbeitung des Tasks wartet
    }


    private static void Test()
    {
        Thread.Sleep(3000);
        Console.WriteLine("Ich tue irgendwas");
    }

    static int CalculateSomething(int i)
    {
        return 42 * i;
    }
}
