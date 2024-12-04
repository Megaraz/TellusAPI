using System;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories;

namespace Test;
/// <summary>
/// Innehåller alla metoder för att testa KundRepo i KonsolAppen(Program)
/// </summary>
static class TEST_KUND
{

    public static void UpdateKund(KundRepository kundRepository, Kund kund)
    {
        if (kund.ID <= 0)
        {
            throw new ArgumentException("Ogiltigt eller tomt ID, fyll i korrekt ID\n\n");
        }

        if (kundRepository.GetKundByID(kund.ID) == null)
        {
            throw new ArgumentException($"Ingen kund med ID {kund.ID} hittades i databasen. \n\n");
        }

        kundRepository.UpdateKund(kund);
        Console.WriteLine($"Kunden har uppdaterats:\n{kund}");
    }

    public static void GetKunder(KundRepository kundRepository)
    {
        var kunder = kundRepository.GetKunder();

        if (kunder != null && kunder.Count > 0)
        {
            foreach (var item in kunder)
            {
                Console.WriteLine(item);
            }
        }
        else
        {
            Console.WriteLine("Inga kunder hittades i databasen");
        }
    }

    public static void DeleteKundByID(KundRepository kundRepository, int id)
    {
        Kund? kund = kundRepository.GetKundByID(id);

        if (kund != null)
        {
            Console.WriteLine($"{kund}\n\nKunden har deletats från databasen");

            kundRepository.DeleteKundByID(id);

        }
        else
            Console.WriteLine("Ingen kund i databasen med detta ID");
    }

    public static void GetKundByID(KundRepository kundRepository, int id)
    {
        Kund? kund = kundRepository.GetKundByID(id);
        

        if (kund != null)
        {

            Console.WriteLine(kund);
        }
        else
            Console.WriteLine("Ingen kund i databasen med detta ID");
    }

    public static void AddKund(KundRepository kundRepository, Kund kund)
    {
        kundRepository.AddKund(kund);
        Console.WriteLine($"{kund}\n\nKunden har lagts till i databasen");
    }
}
