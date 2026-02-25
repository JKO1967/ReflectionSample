using System.Text.Json.Serialization;

namespace JsonSerialisierungsSample;

[JsonDerivedType(typeof(Sparkonto), "sk")]
[JsonDerivedType(typeof(Girokonto), "gk")]
public abstract class Konto
{
    protected Konto(IInhaber inhaber)
    {
        Inhaber = inhaber;
        Random r = new Random();
        int nummer = r.Next(100_000, int.MaxValue);
        Iban = $"De9974150000{nummer.ToString().PadLeft(10, '0')}";
    }

    public string Iban { get; init; }  // init der wert kann nur bei der Objektinitialisierung gesetzt werden, danach nicht mehr

    public Dictionary<DateTime, double> Buchung { get; set; } = [];

    public IInhaber Inhaber { get; set; }

    //[JsonInclude]
    public double Saldo { get; set; }

    public double Dispo { get; set; }

    public double Auszahlen(double betrag)
    {
        Saldo -= betrag;
        return Saldo;
    }

    public abstract double Einzahlen(double betrag);


}
