using Models;
using Repositories;
namespace Test;

internal class Program
{
    
    static void Main(string[] args)
    {
        // Fyll i egen connectionstring här.
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Tellus;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

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

        #region KONTAKTUPPGIFT CRUD TESTER

        //KontaktuppgiftRepository kontaktuppgiftRepository = new KontaktuppgiftRepository(connectionString);
        //Kontaktuppgift kontaktuppgift = new Kontaktuppgift()
        //{
        //    Kontakttyp = "E-post",
        //    Kontaktvärde = "test@example.com",
        //    ID = 5
        //};

        //// (CREATE) Test för att lägga till ny Kontaktuppgift
        //TEST_KONTAKTUPPGIFT.AddKontaktuppgift(kontaktuppgiftRepository, kontaktuppgift);

        //// (READ) Test för att hämta Kontaktuppgift by ID
        //TEST_KONTAKTUPPGIFT.GetKontaktuppgiftByID(kontaktuppgiftRepository, 5);

        //// (READ) Test för att hämta samtliga Kontaktuppgifter
        //TEST_KONTAKTUPPGIFT.GetKontaktuppgifter(kontaktuppgiftRepository);

        //// (UPDATE) Test för att uppdatera en Kontaktuppgift
        //TEST_KONTAKTUPPGIFT.UpdateKontaktuppgift(kontaktuppgiftRepository, kontaktuppgift);

        //// (DELETE) Test för att deletea en Kontaktuppgift
        //TEST_KONTAKTUPPGIFT.DeleteKontaktuppgiftByID(kontaktuppgiftRepository, 20);

        #endregion

        #region ADRESS CRUD TESTER

        //AdressRepository adressRepository = new AdressRepository(connectionString);
        //Adress adress = new Adress()
        //{
        //    Gatuadress = "Strandvägen 5 C",
        //    Ort = "Ljungskile",
        //    Postnr = "459 32",
        //    LghNummer = null,
        //    ID = 5
        //};

        //// (CREATE) Test för att lägga till ny Adress.
        //TEST_ADRESS.AddAdress(adressRepository, adress);

        //// (READ) Test för att hämta Adress by ID
        //TEST_ADRESS.GetAdressByID(adressRepository, 5);

        //// (READ) Test för att hämta samtliga Adresser
        //TEST_ADRESS.GetAdresser(adressRepository);

        //// (UPDATE) Test för att uppdatera en Adress
        //TEST_ADRESS.UpdateAdress(adressRepository, adress);

        //// (DELETE) Test för att deletea en Adress
        //TEST_ADRESS.DeleteAdressByID(adressRepository, 20);

        #endregion



        #region ORDER CRUD TESTER

        //OrderRepository orderRepository = new OrderRepository(connectionString);
        //Order order = new Order()
        //{
        //    Ordernr = 1001,
        //    ÄrSkickad = false,
        //    ÄrLevererad = false,
        //    ÄrBetald = false,
        //    Betalsystem = "Swish",
        //    TidVidBeställning = DateTime.Now,
        //    BeräknadLeverans = DateTime.Now.AddDays(5),
        //    Kund2KontaktID = 1,
        //    ID = 5
        //};

        //// (CREATE) Test för att lägga till ny Order
        //TEST_ORDER.AddOrder(orderRepository, order);

        //// (READ) Test för att hämta Order by ID
        //TEST_ORDER.GetOrderByID(orderRepository, 5);

        //// (READ) Test för att hämta samtliga Ordrar
        //TEST_ORDER.GetOrders(orderRepository);

        //// (UPDATE) Test för att uppdatera en Order
        //TEST_ORDER.UpdateOrder(orderRepository, order);

        //// (DELETE) Test för att deletea en Order
        //TEST_ORDER.DeleteOrderByID(orderRepository, 20);

        #endregion

        #region PRODUKT CRUD TESTER

        //ProduktRepository produktRepository = new ProduktRepository(connectionString);
        //Produkt produkt = new Produkt()
        //{
        //    ProduktTyp = "Elektronik",
        //    Produktnamn = "Trådlösa Hörlurar",
        //    ProduktNummer = "123-456-789",
        //    Pris = 1999.99m,
        //    ID = 5
        //};

        //// (CREATE) Test för att lägga till ny Produkt
        //TEST_PRODUKT.AddProdukt(produktRepository, produkt);

        //// (READ) Test för att hämta Produkt by ID
        //TEST_PRODUKT.GetProduktByID(produktRepository, 5);

        //// (READ) Test för att hämta samtliga Produkter
        //TEST_PRODUKT.GetProdukter(produktRepository);

        //// (UPDATE) Test för att uppdatera en Produkt
        //TEST_PRODUKT.UpdateProdukt(produktRepository, produkt);

        //// (DELETE) Test för att deletea en Produkt
        //TEST_PRODUKT.DeleteProduktByID(produktRepository, 20);

        #endregion








    }


}
