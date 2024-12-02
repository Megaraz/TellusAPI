using Models;
using Repositories;
namespace Test;

internal class Program
{
    static void Main(string[] args)
    {

        KundRepository kundRepository = new KundRepository(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Tellus;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        Kund? kund = new Kund("19910410-5235", "Rasmus", "Lackberg", 1001);

        //// Test för att lägga till ny kund, fyll i de tomma ""
        //Test_AddKund(kundRepository);


        //// Test för att hämta kund by ID
        //Test_GetKundByID(kundRepository, 1001);


        //// Test för att updatera kund
        //kundRepository.UpdateKund(kund);





    }

    private static void Test_GetKundByID(KundRepository kundRepository, int id)
    {
        if (kundRepository.GetKundByID(id) != null)
        {
            Kund? kund = kundRepository.GetKundByID(id);

            Console.WriteLine(kund);
        }
        else
            Console.WriteLine("Ingen kund i databas med detta ID");
    }

    private static void Test_AddKund(KundRepository kundRepository)
    {
        Kund kund = new Kund
                    (
                        "",
                        "",
                        ""
                    );
        kundRepository.AddKund(kund);
    }
}
