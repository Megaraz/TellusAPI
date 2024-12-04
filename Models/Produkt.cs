using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models;
/// <summary>
/// Klass som representerar 1 post i tabellen Produkter
/// </summary>
public class Produkt
{
    public Produkt(string produktnamn, string produktNummer, decimal pris, string? produktTyp = null, int id = -1)
    {
        ID = id;
        Produktnamn = produktnamn ?? throw new ArgumentNullException(nameof(produktnamn));
        ProduktNummer = produktNummer ?? throw new ArgumentNullException(nameof(produktNummer));
        Pris = pris;
        ProduktTyp = produktTyp;
    }
    /// <summary>
    /// Tom Constructor för att kunna användas i Generiska Repositoryn
    /// </summary>
    public Produkt()
    {

    }

    public int ID { get; set; } = -1;
    public string Produktnamn { get; set; } = string.Empty;
    public string ProduktNummer { get; set; } = string.Empty;
    public decimal Pris { get; set; }
    public string? ProduktTyp { get; set; }

    public override string ToString()
    {
        return $"ID: {ID}, Produktnamn: {Produktnamn}, ProduktNummer: {ProduktNummer}, Pris: {Pris}, ProduktTyp: {ProduktTyp}";
    }
}

