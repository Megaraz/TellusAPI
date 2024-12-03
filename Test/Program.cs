using Models;
using Repositories;
namespace Test;

internal class Program
{
    

    static void Main(string[] args)
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Tellus;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";







        #region ADRESS CRUD TESTER

        AdressRepository adressRepository = new AdressRepository(connectionString);
        Adress adress = new Adress()
        {
            Gatuadress = "Strandvägen 5 C",
            Ort = "Ljungskile",
            Postnr = "459 32",
            LghNummer = null,
            ID = 5
        };

        // (CREATE) Test för att lägga till ny Adress.
        TEST_ADRESS.AddAdress(adressRepository, adress);

        // (READ) Test för att hämta Adress by ID
        TEST_ADRESS.GetAdressByID(adressRepository, 5);

        // (READ) Test för att hämta samtliga Adresser
        TEST_ADRESS.GetAdresser(adressRepository);

        // (UPDATE) Test för att uppdatera en Adress
        TEST_ADRESS.UpdateAdress(adressRepository, adress);

        // (DELETE) Test för att deletea en Adress
        TEST_ADRESS.DeleteAdressByID(adressRepository, 20);

        #endregion
        #region KUND CRUD TESTER

        //KundRepository kundRepository = new KundRepository(_connectionString);

        //string personnr = "19950410-5235";
        //string förnamn = "Rasmus";
        //string efternamn = "Läckberg";

        //Kund kund = new Kund(personnr, förnamn, efternamn);


        //// (CREATE)Test för att lägga till ny kund, fyll i fält ovanför.
        //TEST_KUND.AddKund(kundRepository, kund);


        //// (READ) Test för att hämta kund by ID
        //TEST_KUND.GetKundByID(kundRepository, 1025);


        //// (READ) Test för att hämta samtliga kunder
        //TEST_KUND.GetKunder(kundRepository);


        //// (UPDATE) Test för att updatera kund
        //TEST_KUND.UpdateKund(kundRepository, kund);


        //// (DELETE)Test för att deletea kund
        //TEST_KUND.DeleteKundByID(kundRepository, 1026);
        #endregion

    }


}
