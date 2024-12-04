using Azure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models;
/// <summary>
/// Klass som representerar 1 post i tabellen Adresser
/// </summary>
public class Adress
{
    public Adress(string gatuadress, string ort, string postnr, string? lghNummer = null, int id = -1)
    {
        ID = id;
        Gatuadress = gatuadress ?? throw new ArgumentNullException(nameof(gatuadress));
        Ort = ort ?? throw new ArgumentNullException(nameof(ort));
        Postnr = postnr ?? throw new ArgumentNullException(nameof(postnr));
        LghNummer = lghNummer;
    }
    /// <summary>
    /// Tom Constructor för att kunna användas i Generiska Repositoryn
    /// </summary>
    public Adress()
    {

    }

    public int ID { get; set; } = -1;
    public string Gatuadress { get; set; } = string.Empty;
    public string Ort { get; set; } = string.Empty;
    public string Postnr { get; set; } = string.Empty;
    public string? LghNummer { get; set; }

    public override string ToString()
    {
        return $"ID: {ID}, Gatuadress: {Gatuadress}, Ort: {Ort}, Postnr: {Postnr}, LghNummer: {LghNummer}";
    }
}
