﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models;
public class Kund
{
    public Kund(string personnr, string förnamn, string efternamn, int iD = -1)
    {
        Personnr = personnr ?? throw new ArgumentNullException(nameof(personnr));
        Förnamn = förnamn ?? throw new ArgumentNullException(nameof(förnamn));
        Efternamn = efternamn ?? throw new ArgumentNullException(nameof(efternamn));
        ID = iD;
    }

    public int ID { get; set; }
    public string Personnr { get; set; } = string.Empty;
    public string Förnamn { get; set; } = string.Empty;
    public string Efternamn { get; set; } = string.Empty;

    public override string ToString()
    {
        return $"{ID}   {Personnr}, {Förnamn} {Efternamn}";
    }
}