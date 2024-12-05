using System;
using Models;
using Repositories;

namespace Test;
/// <summary>
/// Innehåller alla metoder för att testa Kund2KontaktRepo i KonsolAppen
/// </summary>
static class TEST_KUND2KONTAKT
{
    public static void AddKund2Kontakt(Kund2KontaktRepository kund2KontaktRepository, Kund2Kontakt kund2Kontakt)
    {
        try
        {
            kund2KontaktRepository.AddKund2Kontakt(kund2Kontakt);
            Console.WriteLine($"{kund2Kontakt}\n\nRelationen har lagts till i databasen.");

        }
        catch
        {
            Console.WriteLine("Finns redan en relation mellan dessa två IDn. Försök igen.");
        }
    }

    public static void GetKontaktuppgifterByKundID(Kund2KontaktRepository kund2KontaktRepository, int kundID)
    {
        var kund2Kontakter = kund2KontaktRepository.GetKontaktuppgifterByKundID(kundID);

        if (kund2Kontakter.Count > 0)
        {
            foreach (var relation in kund2Kontakter)
            {
                Console.WriteLine(relation);
            }
        }
        else
        {
            Console.WriteLine($"Inga kontaktuppgifter hittades för kund med ID {kundID}.");
        }
    }

    public static void GetKunderByKontaktuppgiftID(Kund2KontaktRepository kund2KontaktRepository, int kontaktuppgiftID)
    {
        var kund2Kontakter = kund2KontaktRepository.GetKunderByKontaktuppgiftID(kontaktuppgiftID);

        if (kund2Kontakter.Count > 0)
        {
            foreach (var relation in kund2Kontakter)
            {
                Console.WriteLine(relation);
            }
        }
        else
        {
            Console.WriteLine($"Inga kunder hittades för kontaktuppgift med ID {kontaktuppgiftID}.");
        }
    }

    public static void UpdateKund2Kontakt(Kund2KontaktRepository kund2KontaktRepository, Kund2Kontakt kund2Kontakt)
    {
        if (kund2Kontakt.ID <= 0)
        {
            throw new ArgumentException("Ogiltigt eller tomt ID, fyll i korrekt ID\n\n");
        }

        kund2KontaktRepository.UpdateKund2Kontakt(kund2Kontakt);
        Console.WriteLine($"Relationen har uppdaterats:\n{kund2Kontakt}");
    }

    public static void DeleteKund2Kontakt(Kund2KontaktRepository kund2KontaktRepository, int id)
    {
        kund2KontaktRepository.DeleteKund2Kontakt(id);
        Console.WriteLine($"Relationen med ID {id} har tagits bort från databasen.");
    }
}
