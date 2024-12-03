using Models;
using Repositories;
namespace Test;

internal class Program
{
    static void Main(string[] args)
    {

        KundRepository kundRepository = new KundRepository(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Tellus;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

        string personnr = "19950410-5235";
        string förnamn = "Rasmus";
        string efternamn = "Läckberg";

        Kund kund = new Kund(personnr, förnamn, efternamn);



        #region KUND CRUD TESTER
        //// (CREATE)Test för att lägga till ny kund, fyll i fält ovanför.
        //Test_AddKund(kundRepository, kund);


        //// (READ) Test för att hämta kund by ID
        //Test_GetKundByID(kundRepository, 1025);


        //// (READ) Test för att hämta samtliga kunder
        //foreach (var item in kundRepository.GetKunder()!)
        //{
        //    Console.WriteLine(item);
        //}


        //// (UPDATE) Test för att updatera kund
        //kundRepository.UpdateKund(kund);


        //// (DELETE)Test för att deletea kund
        //Test_DeleteKundByID(kundRepository, 1026);
        #endregion

    }

    private static void Test_DeleteKundByID(KundRepository kundRepository, int id)
    {
        Kund? kund = kundRepository.GetKundByID(id);

        if (kund != null)
        {
            Console.WriteLine($"{kund}\n\nKunden har deletats från databasen");

            kundRepository.DeleteKundByID(id);

        }
        else
            Console.WriteLine("Ingen kund i databas med detta ID");
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

    private static void Test_AddKund(KundRepository kundRepository, Kund kund)
    {
        kundRepository.AddKund(kund);
        Console.WriteLine($"{kund}\n\nKunden har lagts till i databasen");
    }
}
