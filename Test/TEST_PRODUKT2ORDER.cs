using System;
using Models;
using Repositories;

namespace Test;
/// <summary>
/// Innehåller alla metoder för att testa Produkt2OrderRepo i KonsolAppen
/// </summary>
static class TEST_PRODUKT2ORDER
{
    public static void AddProdukt2Order(Produkt2OrderRepository produkt2OrderRepository, Produkt2Order produkt2Order)
    {
        produkt2OrderRepository.AddProdukt2Order(produkt2Order);
        Console.WriteLine($"{produkt2Order}\n\nRelationen har lagts till i databasen.");
    }

    public static void GetProdukterByOrderID(Produkt2OrderRepository produkt2OrderRepository, int orderID)
    {
        var produkt2Orders = produkt2OrderRepository.GetProdukterByOrderID(orderID);

        if (produkt2Orders.Count > 0)
        {
            foreach (var relation in produkt2Orders)
            {
                Console.WriteLine(relation);
            }
        }
        else
        {
            Console.WriteLine($"Inga produkter hittades för order med ID {orderID}.");
        }
    }

    public static void UpdateProdukt2Order(Produkt2OrderRepository produkt2OrderRepository, Produkt2Order produkt2Order)
    {
        if (produkt2Order.ID <= 0)
        {
            throw new ArgumentException("Ogiltigt eller tomt ID, fyll i korrekt ID\n\n");
        }

        produkt2OrderRepository.UpdateProdukt2Order(produkt2Order);
        Console.WriteLine($"Relationen har uppdaterats:\n{produkt2Order}");
    }

    public static void DeleteProdukt2Order(Produkt2OrderRepository produkt2OrderRepository, int id)
    {
        produkt2OrderRepository.DeleteProdukt2Order(id);
        Console.WriteLine($"Relationen med ID {id} har tagits bort från databasen.");
    }
}
