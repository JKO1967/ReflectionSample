namespace WinFormsApp1;

public partial class Form1 : Form
{
    private Konto konto = new Konto() { Name = "Girokonto", Kontostand = 0 };

    public Form1()
    {
        InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        konto.Buchen(konto.Einzahlen, 50);
    }



    private void Form1_Load(object sender, EventArgs e)
    {

    }

    private void button1_Click_1(object sender, EventArgs e)
    {
        konto.Buchen(konto.Auszahlen,50);
    }
}
