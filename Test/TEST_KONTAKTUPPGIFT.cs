using System;
using Models;
using Repositories;

namespace Test;
static class TEST_KONTAKTUPPGIFT
{
    public static void UpdateKontaktuppgift(KontaktuppgiftRepository kontaktuppgiftRepository, Kontaktuppgift kontaktuppgift)
    {
        kontaktuppgiftRepository.UpdateKontaktuppgift(kontaktuppgift);
        Console.WriteLine($"Kontaktuppgiften har uppdaterats:\n{kontaktuppgift}");
    }

    public static void GetKontaktuppgifter(KontaktuppgiftRepository kontaktuppgiftRepository)
    {
        var kontaktuppgifter = kontaktuppgiftRepository.GetKontaktuppgifter();
        if (kontaktuppgifter != null && kontaktuppgifter.Count > 0)
        {
            foreach (var kontaktuppgift in kontaktuppgifter)
            {
                Console.WriteLine(kontaktuppgift);
            }
        }
        else
        {
            Console.WriteLine("Inga kontaktuppgifter hittades i databasen.");
        }
    }

    public static void DeleteKontaktuppgiftByID(KontaktuppgiftRepository kontaktuppgiftRepository, int id)
    {
        Kontaktuppgift? kontaktuppgift = kontaktuppgiftRepository.GetKontaktuppgiftByID(id);

        if (kontaktuppgift != null)
        {
            Console.WriteLine($"{kontaktuppgift}\n\nKontaktuppgiften har deletats från databasen");

            kontaktuppgiftRepository.DeleteKontaktuppgiftByID(id);
        }
        else
        {
            Console.WriteLine("Ingen kontaktuppgift i databasen med detta ID.");
        }
    }

    public static void GetKontaktuppgiftByID(KontaktuppgiftRepository kontaktuppgiftRepository, int id)
    {
        Kontaktuppgift? kontaktuppgift = kontaktuppgiftRepository.GetKontaktuppgiftByID(id);

        if (kontaktuppgift != null)
        {
            Console.WriteLine(kontaktuppgift);
        }
        else
        {
            Console.WriteLine("Ingen kontaktuppgift i databasen med detta ID.");
        }
    }

    public static void AddKontaktuppgift(KontaktuppgiftRepository kontaktuppgiftRepository, Kontaktuppgift kontaktuppgift)
    {
        kontaktuppgiftRepository.AddKontaktuppgift(kontaktuppgift);
        Console.WriteLine($"{kontaktuppgift}\n\nKontaktuppgiften har lagts till i databasen.");
    }
}
