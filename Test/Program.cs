using Models;
using Repositories;
namespace Test;

internal class Program
{
    static void Main(string[] args)
    {







        #region KUND CRUD TESTER

        KundRepository kundRepository = new KundRepository(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Tellus;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

        string personnr = "19950410-5235";
        string förnamn = "Rasmus";
        string efternamn = "Läckberg";

        Kund kund = new Kund(personnr, förnamn, efternamn);


        // (CREATE)Test för att lägga till ny kund, fyll i fält ovanför.
        TEST_KUND.AddKund(kundRepository, kund);


        // (READ) Test för att hämta kund by ID
        TEST_KUND.GetKundByID(kundRepository, 1025);


        // (READ) Test för att hämta samtliga kunder
        TEST_KUND.GetKunder(kundRepository);


        // (UPDATE) Test för att updatera kund
        TEST_KUND.UpdateKund(kundRepository, kund);


        // (DELETE)Test för att deletea kund
        TEST_KUND.DeleteKundByID(kundRepository, 1026);
        #endregion

    }

    
}
