using System.Diagnostics;
using System.Windows;

namespace WpfApp1;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        ConsoleWriteLine("Progamm wird gestartet");
       

        var tasks = new List<Task<int>>()
        {
            CalculateSomething("A", 500),
            CalculateSomething("B", 1500),
            CalculateSomething("C", 200)
        }; // Alle Tasks werden hier sofort gestartet

        ConsoleWriteLine("Alle tasks wurden gestartet");
    }

    private  async Task<int> CalculateSomething(string prozess, int delay)
    {
        ConsoleWriteLine($"Prozess {prozess} wurde gestartet");
        for (int i = 0; i < 5; i++)
        {
            ConsoleWriteLine($"Aufruf {i} von Prozess {prozess} gestartet");
            //await Task.Delay(delay);
            Title = await Task<string>.Run(async ()  => 
            {
                ConsoleWriteLine("Really working not sleeping");
                await Task.Delay( delay );
                return "5";
            });
            ConsoleWriteLine($"Aufruf {i} von Prozess {prozess} beendet");
        }

        ConsoleWriteLine($"Prozess {prozess} gibt Ergebnis zurück");
        return 42 * delay;
    }

    private  void ConsoleWriteLine(string message)
    {
        int threadId = Thread.CurrentThread.ManagedThreadId;
        //Console.ForegroundColor = threadId == 1 ? ConsoleColor.White : ConsoleColor.Cyan;
        Debug.WriteLine($"Thread {threadId} {message}");
    }
}