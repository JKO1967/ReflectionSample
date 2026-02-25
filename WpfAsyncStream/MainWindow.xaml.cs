using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAsyncStream;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private async void Button_Click(object sender, RoutedEventArgs e)
    {
        lbZeilen.Items.Clear();

        await foreach (var item in ReadFile())
        {
            lbZeilen.Items.Insert(0, item);
        }
    }

    private async IAsyncEnumerable<string> ReadFile()
    {
        using StreamReader reader = new StreamReader("Demo.txt");

        while (!reader.EndOfStream)
        {
            await Task.Delay(20);
            yield return await reader.ReadLineAsync() ?? string.Empty;
        }
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        if (!File.Exists("Demo.txt"))
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 1000; i++)
            {
                sb.AppendLine($"Zeile {i}");
            }
            File.WriteAllText("Demo.txt", sb.ToString());
        }
    }

   
}