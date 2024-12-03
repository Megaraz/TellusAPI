using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models;
public class Kund
{
    public Kund(string personnr, string förnamn, string efternamn, int iD = -1)
    {
        ID = iD;
        Personnr = personnr ?? throw new ArgumentNullException(nameof(personnr));
        Förnamn = förnamn ?? throw new ArgumentNullException(nameof(förnamn));
        Efternamn = efternamn ?? throw new ArgumentNullException(nameof(efternamn));
    }

    public Kund()
    {

    }

    public int ID { get; set; }
    public string Personnr { get; set; } = string.Empty;
    public string Förnamn { get; set; } = string.Empty;
    public string Efternamn { get; set; } = string.Empty;

    public override string ToString()
    {
        return $"ID: {ID}{Environment.NewLine}Personnr: {Personnr}{Environment.NewLine}Förnamn: {Förnamn}{Environment.NewLine}Efternamn: {Efternamn}{Environment.NewLine}";
    }
}
