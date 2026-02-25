namespace JsonSerialisierungsSample;
public class Sparkonto : Konto
{
    public Sparkonto(IInhaber inhaber) : base(inhaber)
    {
        Zinssatz = 0.01;
    }

    public override double Einzahlen(double betrag)
    {
        Saldo += betrag;
        Buchung.Add(DateTime.Now, betrag);
        return Saldo;
    }

    public double Zinssatz { get; set; }
}
