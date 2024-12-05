using Models;
using Repositories;
namespace Test;

internal class Program
{
    
    static void Main(string[] args)
    {
        // Fyll i egen connectionstring här.
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Tellus;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";


        // Regioner nedan innehåller metoder och data för att testa, avkommentera det man vill testa.


        #region KUND CRUD TESTER

        //KundRepository kundRepository = new KundRepository(connectionString);

        //Kund kund = new Kund()
        //{
        //    ID = 1011,
        //    Personnr = "19671223-1234",
        //    Förnamn = "Ånkel",
        //    Efternamn = "Kånkel",
        //};


        //// (CREATE)Test för att lägga till ny kund, fyll i fält ovanför.
        //TEST_KUND.AddKund(kundRepository, kund);


        //// (READ) Test för att hämta kund by ID
        //TEST_KUND.GetKundByID(kundRepository, 1009);


        //// (READ) Test för att hämta samtliga kunder
        //TEST_KUND.GetKunder(kundRepository);


        //// (UPDATE) Test för att updatera kund
        //TEST_KUND.UpdateKund(kundRepository, kund);


        //// (DELETE)Test för att deletea kund
        //TEST_KUND.DeleteKundByID(kundRepository, 1009);
        #endregion

        #region KUND2KONTAKT CRUD TESTER

        //Kund2KontaktRepository kund2KontaktRepository = new Kund2KontaktRepository(connectionString);

        //Kund2Kontakt kund2Kontakt = new Kund2Kontakt()
        //{
        //    KundID = 1001,
        //    KontaktuppgiftID = 1
        //};

        //// (CREATE) Test för att lägga till ny relation mellan kund och kontaktuppgift
        //TEST_KUND2KONTAKT.AddKund2Kontakt(kund2KontaktRepository, kund2Kontakt);

        //// (READ) Test för att hämta kontaktuppgifter för en viss kund
        //TEST_KUND2KONTAKT.GetKontaktuppgifterByKundID(kund2KontaktRepository, 1001);

        //// (READ) Test för att hämta kunder för en viss kontaktuppgift
        //TEST_KUND2KONTAKT.GetKunderByKontaktuppgiftID(kund2KontaktRepository, 1);

        //// (UPDATE) Test för att uppdatera en relation mellan kund och kontaktuppgift
        //kund2Kontakt.ID = 1; // Ange korrekt ID för att uppdatera
        //kund2Kontakt.KontaktuppgiftID = 2; // Ändra kontaktuppgift
        //TEST_KUND2KONTAKT.UpdateKund2Kontakt(kund2KontaktRepository, kund2Kontakt);

        //// (DELETE) Test för att ta bort en relation mellan kund och kontaktuppgift
        //TEST_KUND2KONTAKT.DeleteKund2Kontakt(kund2KontaktRepository, 1);

        #endregion

        #region KUND2ADRESS CRUD TESTER

        //Kund2AdressRepository kund2AdressRepository = new Kund2AdressRepository(connectionString);

        //Kund2Adress kund2Adress = new Kund2Adress()
        //{
        //    ID = 1,
        //    KundID = 1002, // Existerande KundID
        //    AdressID = 4   // Existerande AdressID
        //};


        //// (CREATE) Test för att lägga till en relation mellan Kund och Adress, fyll i fält ovanför.
        //TEST_KUND2ADRESS.AddKund2Adress(kund2AdressRepository, kund2Adress);


        //// (READ) Test för att hämta adresser kopplade till en kund
        // TEST_KUND2ADRESS.GetAdresserByKundID(kund2AdressRepository, 1001);


        //// (READ) Test för att hämta kunder kopplade till en adress
        // TEST_KUND2ADRESS.GetKunderByAdressID(kund2AdressRepository, 1);


        //// (UPDATE) Test för att uppdatera en relation mellan Kund och Adress
        // kund2Adress.AdressID = 2; // Exempel på ny AdressID
        // TEST_KUND2ADRESS.UpdateKund2Adress(kund2AdressRepository, kund2Adress);


        //// (DELETE) Test för att ta bort en relation mellan Kund och Adress
        // TEST_KUND2ADRESS.DeleteKund2Adress(kund2AdressRepository, 1);

        #endregion

        #region ADRESS CRUD TESTER

        //AdressRepository adressRepository = new AdressRepository(connectionString);
        //Adress adress = new Adress()
        //{
        //    Gatuadress = "Krokgatan 5 C",
        //    Ort = "Kungälv",
        //    Postnr = "459 32",
        //    LghNummer = null,
        //    ID = 11
        //};

        //// (CREATE) Test för att lägga till ny Adress.
        //TEST_ADRESS.AddAdress(adressRepository, adress);

        //// (READ) Test för att hämta Adress by ID
        //TEST_ADRESS.GetAdressByID(adressRepository, 7);

        //// (READ) Test för att hämta samtliga Adresser
        //TEST_ADRESS.GetAdresser(adressRepository);

        //// (UPDATE) Test för att uppdatera en Adress
        //TEST_ADRESS.UpdateAdress(adressRepository, adress);

        //// (DELETE) Test för att deletea en Adress
        //TEST_ADRESS.DeleteAdressByID(adressRepository, 7);

        #endregion

        #region KONTAKTUPPGIFT CRUD TESTER

        //KontaktuppgiftRepository kontaktuppgiftRepository = new KontaktuppgiftRepository(connectionString);
        //Kontaktuppgift kontaktuppgift = new Kontaktuppgift()
        //{
        //    Kontakttyp = "E-post",
        //    Kontaktvärde = "blabla@example.com",
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
        //TEST_KONTAKTUPPGIFT.DeleteKontaktuppgiftByID(kontaktuppgiftRepository, 5);

        #endregion

        #region ORDER CRUD TESTER

        //OrderRepository orderRepository = new OrderRepository(connectionString);
        //Order order = new Order()
        //{
        //    ÄrSkickad = false,
        //    ÄrLevererad = false,
        //    ÄrBetald = true,
        //    Betalsystem = "Kort",
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
        //TEST_ORDER.DeleteOrderByID(orderRepository, 5);

        #endregion

        #region PRODUKT2ORDER CRUD TESTER

        //Produkt2OrderRepository produkt2OrderRepository = new Produkt2OrderRepository(connectionString);

        //Produkt2Order produkt2Order = new Produkt2Order()
        //{
        //    ProduktID = 1,
        //    OrderID = 1,
        //    Antal = 3
        //};

        //// (CREATE) Test för att lägga till ny relation mellan produkt och order
        //TEST_PRODUKT2ORDER.AddProdukt2Order(produkt2OrderRepository, produkt2Order);

        //// (READ) Test för att hämta produkter för en viss order
        //TEST_PRODUKT2ORDER.GetProdukterByOrderID(produkt2OrderRepository, 1);

        //// (UPDATE) Test för att uppdatera en relation mellan produkt och order
        //produkt2Order.ID = 1; // Ange korrekt ID för att uppdatera
        //produkt2Order.Antal = 5; // Ändra antal
        //TEST_PRODUKT2ORDER.UpdateProdukt2Order(produkt2OrderRepository, produkt2Order);

        //// (DELETE) Test för att ta bort en relation mellan produkt och order
        //TEST_PRODUKT2ORDER.DeleteProdukt2Order(produkt2OrderRepository, 1);

        #endregion

        #region PRODUKT CRUD TESTER

        //ProduktRepository produktRepository = new ProduktRepository(connectionString);
        //Produkt produkt = new Produkt()
        //{
        //    ProduktTyp = "Elektronik",
        //    Produktnamn = "Trådlös Mus",
        //    ProduktNummer = "123-456-787",
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
        //TEST_PRODUKT.DeleteProduktByID(produktRepository, 5);

        #endregion
    }


}
