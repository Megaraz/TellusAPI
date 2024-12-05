using System;
using Models;
using System.Collections.Generic;
using Repositories;

namespace Test;
/// <summary>
/// Innehåller alla metoder för att testa Kund2AdressRepo i KonsolAppen(Program)
/// </summary>
static class TEST_KUND2ADRESS
{
    public static void AddKund2Adress(Kund2AdressRepository kund2AdressRepository, Kund2Adress kund2Adress)
    {
        kund2AdressRepository.AddKund2Adress(kund2Adress);

        Console.WriteLine($"Relationen mellan Kund och Adress har lagts till:\n{kund2Adress}");
    }

    public static void GetAdresserByKundID(Kund2AdressRepository kund2AdressRepository, int kundID)
    {
        List<Kund2Adress> kund2Adresser = kund2AdressRepository.GetAdresserByKundID(kundID);

        if (kund2Adresser != null && kund2Adresser.Count > 0)
        {
            foreach (Kund2Adress item in kund2Adresser)
            {
                Console.WriteLine($"{item}");
            }
        }
        else
        {
            Console.WriteLine($"Inga adresser hittades för KundID {kundID}.");
        }
    }

    public static void GetKunderByAdressID(Kund2AdressRepository kund2AdressRepository, int adressID)
    {
        List<Kund2Adress> kund2Adresser = kund2AdressRepository.GetKunderByAdressID(adressID);

        if (kund2Adresser != null && kund2Adresser.Count > 0)
        {
            foreach (Kund2Adress item in kund2Adresser)
            {
                Console.WriteLine($"{item}");
            }
        }
        else
        {
            Console.WriteLine($"Inga kunder hittades för AdressID {adressID}.");
        }
    }

    public static void UpdateKund2Adress(Kund2AdressRepository kund2AdressRepository, Kund2Adress kund2Adress)
    {
        if (kund2Adress.ID <= 0)
        {
            throw new ArgumentException("Ogiltigt eller tomt ID, fyll i korrekt ID\n\n");
        }

        kund2AdressRepository.UpdateKund2Adress(kund2Adress);

        Console.WriteLine($"Relationen mellan Kund och Adress har uppdaterats:\n{kund2Adress}");
    }

    public static void DeleteKund2Adress(Kund2AdressRepository kund2AdressRepository, int id)
    {
        kund2AdressRepository.DeleteKund2Adress(id);

        Console.WriteLine($"Relationen mellan Kund och Adress med Kund2AdressID {id} har tagits bort.");
    }
}
