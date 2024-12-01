using TellusAPI;
namespace Test;

internal class Program
{
    static void Main(string[] args)
    {

        Tellus tellus = new Tellus(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Tellus;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

        Kund kund = new Kund
            (
                "19910410-5235",
                "Rasmus",
                "Läckberg"
            );

        tellus.AddKund(kund);
    }
}
