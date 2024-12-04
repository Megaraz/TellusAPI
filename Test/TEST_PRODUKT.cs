using System;
using Models;
using Repositories;

namespace Test;
/// <summary>
/// Innehåller alla metoder för att testa ProduktRepo i KonsolAppen(Program)
/// </summary>
static class TEST_PRODUKT
{
    public static void UpdateProdukt(ProduktRepository produktRepository, Produkt produkt)
    {
        if (produkt.ID <= 0)
        {
            throw new ArgumentException("Ogiltigt eller tomt ID, fyll i korrekt ID\n\n");
        }
        if (produktRepository.GetProduktByID(produkt.ID) == null)
        {
            throw new ArgumentException($"Ingen produkt med ID {produkt.ID} hittades i databasen.\n\n");
        }

        produktRepository.UpdateProdukt(produkt);
        Console.WriteLine($"Produkten har uppdaterats:\n{produkt}");
    }

    public static void GetProdukter(ProduktRepository produktRepository)
    {
        var produkter = produktRepository.GetProdukter();
        if (produkter != null && produkter.Count > 0)
        {
            foreach (var produkt in produkter)
            {
                Console.WriteLine(produkt);
            }
        }
        else
        {
            Console.WriteLine("Inga produkter hittades i databasen.");
        }
    }

    public static void DeleteProduktByID(ProduktRepository produktRepository, int id)
    {
        Produkt? produkt = produktRepository.GetProduktByID(id);

        if (produkt != null)
        {
            Console.WriteLine($"{produkt}\n\nProdukten har deletats från databasen");

            produktRepository.DeleteProduktByID(id);
        }
        else
        {
            Console.WriteLine("Ingen produkt i databasen med detta ID.");
        }
    }

    public static void GetProduktByID(ProduktRepository produktRepository, int id)
    {
        Produkt? produkt = produktRepository.GetProduktByID(id);

        if (produkt != null)
        {
            Console.WriteLine(produkt);
        }
        else
        {
            Console.WriteLine("Ingen produkt i databasen med detta ID.");
        }
    }

    public static void AddProdukt(ProduktRepository produktRepository, Produkt produkt)
    {
        produktRepository.AddProdukt(produkt);
        Console.WriteLine($"{produkt}\n\nProdukten har lagts till i databasen.");
    }
}
