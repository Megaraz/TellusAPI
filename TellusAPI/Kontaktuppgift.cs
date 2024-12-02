using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models;
internal class Kontaktuppgift
{
    public Kontaktuppgift(string kontakttyp, string kontaktvärde, int id = -1)
    {
        ID = id;
        Kontakttyp = kontakttyp ?? throw new ArgumentNullException(nameof(kontakttyp));
        Kontaktvärde = kontaktvärde ?? throw new ArgumentNullException(nameof(kontaktvärde));
    }

    public Kontaktuppgift()
    {

    }

    public int ID { get; set; }
    public string Kontakttyp { get; set; } = string.Empty;
    public string Kontaktvärde { get; set; } = string.Empty;

    public override string ToString()
    {
        return $"ID: {ID}, Kontakttyp: {Kontakttyp}, Kontaktuppgift: {Kontaktvärde}";
    }
}
