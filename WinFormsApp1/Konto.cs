namespace WinFormsApp1;
public class Konto
{
    public string Name { get; set; } = string.Empty;
    public decimal Kontostand { get; set; }

    public decimal Einzahlen(decimal betrag)
    {
        Kontostand += betrag;
        return Kontostand;
    }

    public decimal Auszahlen(decimal betrag)
    {
        Kontostand -= betrag;
        return Kontostand;
    }

    public decimal Buchen(Func<decimal, decimal> buchungsfunktion, decimal betrag)
    {
        Kontostand = buchungsfunktion(betrag);

        return Kontostand;
    }
}
