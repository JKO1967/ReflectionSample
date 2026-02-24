using Autofac;

namespace AutofacSample;

internal class Program
{
    private static IContainer _container;

    static void Main(string[] args)
    {
        var builder = new ContainerBuilder();

        builder.RegisterType<FileOutput>().As<ITextWriter>();
        builder.RegisterType<TodayWriter>().As<IDateWriter>();
        
        _container = builder.Build();

        WriteDate();

        Console.ReadLine();
    }

    private static void WriteDate()
    {
        using (var scope = _container.BeginLifetimeScope())
        {
            var writer = scope.Resolve<IDateWriter>();
            writer.WriteData();
        }
    }
}
