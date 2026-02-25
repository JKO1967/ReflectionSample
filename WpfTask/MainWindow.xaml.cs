using System.Windows;

namespace WpfTask;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly CancellationToken token = Task.Factory.CancellationToken;
    private readonly TaskScheduler scheduler = TaskScheduler.FromCurrentSynchronizationContext();

    public MainWindow()
    {
        InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        lbItems.ItemsSource = null;
        lbItems.Items.Clear();
        

        Task.Run(FillListV1);
    }

    

    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
        lbItems.ItemsSource = null;
        lbItems.Items.Clear();
        


        Task<List<string>> t = Task.Run(FillListV2);
        t.ContinueWith(t =>
        {
            lbItems.ItemsSource = t.Result;
        }, token, TaskContinuationOptions.OnlyOnRanToCompletion, scheduler);

        t.ContinueWith(t =>
        {
            lbItems.Items.Add($"Ein Fehler ist aufgetreten: {t.Exception?.Message}");
        }, token, TaskContinuationOptions.OnlyOnFaulted, scheduler);
    }

    private List<string> FillListV2()
    {
        List<string> list = new List<string>();
        for (int i = 0; i < 10; i++)
        {
            list.Add($"Eintrag {i}");
            Thread.Sleep(1000);
        }
        if (DateTime.Now.Second > 30)
        {
            throw new InvalidOperationException("Um diese Uhrzeit nicht möglich");
        }
        return list;
    }

    private void FillListV1()
    {
        for (int i = 0; i < 10; i++)
        {
            Task.Factory.StartNew(() =>
            {
                lbItems.Items.Insert(0, $"Eintrag {i}");
            }, token, TaskCreationOptions.None, scheduler);


            Thread.Sleep(1000);
        }
       
    }
}