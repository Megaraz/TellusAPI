using System;
using Models;
using System.Collections.Generic;
using Repositories;

namespace Test;
/// <summary>
/// Innehåller alla metoder för att testa AdressRepo i KonsolAppen(Program)
/// </summary>
static class TEST_ADRESS
{
    public static void UpdateAdress(AdressRepository adressRepository, Adress adress)
    {

        if (adress.ID <= 0)
        {
            throw new ArgumentException("Ogiltigt eller tomt ID, fyll i korrekt ID\n\n");
        }

        if (adressRepository.GetAdressByID(adress.ID) == null)
        {
            throw new ArgumentException($"Ingen adress med ID {adress.ID} hittades i databasen.\n\n");
        }
        adressRepository.UpdateAdress(adress);
        Console.WriteLine($"Adressen har uppdaterats:\n{adress}");
    }

    public static void GetAdresser(AdressRepository adressRepository)
    {
        var adresser = adressRepository.GetAdresser();
        if (adresser != null && adresser.Count > 0)
        {
            foreach (var item in adresser)
            {
                Console.WriteLine(item);
            }
        }
        else
        {
            Console.WriteLine("Inga adresser hittades i databasen.");
        }
    }

    public static void DeleteAdressByID(AdressRepository adressRepository, int id)
    {
        Adress? adress = adressRepository.GetAdressByID(id);

        if (adress != null)
        {
            Console.WriteLine($"{adress}\n\nAdressen har deletats från databasen");

            adressRepository.DeleteAdressByID(id);
        }
        else
        {
            Console.WriteLine("Ingen adress i databasen med detta ID.");
        }
    }

    public static void GetAdressByID(AdressRepository adressRepository, int id)
    {
        Adress? adress = adressRepository.GetAdressByID(id);

        if (adress != null)
        {
            Console.WriteLine(adress);
        }
        else
        {
            Console.WriteLine("Ingen adress i databasen med detta ID.");
        }
    }

    public static void AddAdress(AdressRepository adressRepository, Adress adress)
    {
        adressRepository.AddAdress(adress);
        Console.WriteLine($"{adress}\n\nAdressen har lagts till i databasen.");
    }
}
