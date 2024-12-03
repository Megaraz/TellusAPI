using System;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories;

namespace Test;
static class TEST_KUND
{

    public static void UpdateKund(KundRepository kundRepository, Kund kund)
    {
        kundRepository.UpdateKund(kund);
    }

    public static void GetKunder(KundRepository kundRepository)
    {
        foreach (var item in kundRepository.GetKunder()!)
        {
            Console.WriteLine(item);
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
            Console.WriteLine("Ingen kund i databas med detta ID");
    }

    public static void GetKundByID(KundRepository kundRepository, int id)
    {
        if (kundRepository.GetKundByID(id) != null)
        {
            Kund? kund = kundRepository.GetKundByID(id);

            Console.WriteLine(kund);
        }
        else
            Console.WriteLine("Ingen kund i databas med detta ID");
    }

    public static void AddKund(KundRepository kundRepository, Kund kund)
    {
        kundRepository.AddKund(kund);
        Console.WriteLine($"{kund}\n\nKunden har lagts till i databasen");
    }
}
