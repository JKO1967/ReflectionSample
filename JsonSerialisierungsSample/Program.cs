using System.Text.Json;

namespace JsonSerialisierungsSample;

internal class Program
{
    static void Main(string[] args)
    {
        List<Konto> konten = [];

        Girokonto gKonto = new Girokonto(new FirmenInhaber() { Name = "Cegos", Handelsregisterauszug = "Ab34256" });
        gKonto.Einzahlen(200);
        konten.Add(gKonto);

        Sparkonto sKonto = new Sparkonto(new PersonenInhaber() { Name = "Jürgen" });
        sKonto.Einzahlen(100);
        konten.Add(sKonto);

        //var seralizerOptions = new JsonSerializerOptions()
        //{
        //    WriteIndented = true             
        //};

        var json = JsonSerializer.Serialize(konten, KontoContext.Default.ListKonto);
        Console.WriteLine(json);


        List<Konto>? newKonten = JsonSerializer.Deserialize(json, typeof(List<Konto>), KontoContext.Default) as List<Konto>;
        Console.ReadLine();
    }
}
