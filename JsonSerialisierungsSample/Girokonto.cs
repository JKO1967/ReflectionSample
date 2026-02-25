namespace JsonSerialisierungsSample;
public class Girokonto : Konto
{
    public Girokonto(IInhaber inhaber) : base(inhaber)
    {
        Dispo = 1000;
        Gebuehr = 5.9;
    }

    public override double Einzahlen(double betrag)
    {
        Saldo += betrag;
        Buchung.Add(DateTime.Now, betrag);
        return Saldo;
    }

    public double Gebuehr { get; set; }
}
