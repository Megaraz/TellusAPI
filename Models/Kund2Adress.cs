using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models;
public class Kund2Adress
{
    public int ID { get; set; } = -1;
    public int KundID { get; set; } = -1;
    public int AdressID { get; set; } = -1;

    public Kund? Kund { get; set; } = null;
    public Adress? Adress { get; set; } = null;

    public override string ToString()
    {
        return $"ID: {ID}, KundID: {KundID}, AdressID: {AdressID} " +
               $"\nKund: {(Kund != null ? $"{Kund}" : "Ingen Kund")} " +
               $"\nAdress: {(Adress != null ? $"{Adress}\n" : "Ingen Adress")}";
    }

}

