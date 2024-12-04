using System;

namespace Models;
public class Kund2Kontakt
{
    public int ID { get; set; } = -1;
    public int KundID { get; set; } = -1;
    public int KontaktuppgiftID { get; set; } = -1;

    public Kund? Kund { get; set; } = null;
    public Kontaktuppgift? Kontaktuppgift { get; set; } = null;

    public override string ToString()
    {
        return $"ID: {ID}, KundID: {KundID}, KontaktuppgiftID: {KontaktuppgiftID}, " +
               $"Kund: {(Kund != null ? $"{Kund.Förnamn} {Kund.Efternamn}" : "Ingen Kund")}, " +
               $"Kontaktuppgift: {(Kontaktuppgift != null ? $"{Kontaktuppgift.Kontakttyp}: {Kontaktuppgift.Kontaktvärde}" : "Ingen Kontaktuppgift")}";
    }
}
