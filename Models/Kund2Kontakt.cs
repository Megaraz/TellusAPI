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
        return $"Kund2KontaktID: {ID}" +
               $"\nKund: {(Kund != null ? $"{Kund}" : "Ingen Kund")} " +
               $"\nKontaktuppgift: {(Kontaktuppgift != null ? $"{Kontaktuppgift}\n" : "Ingen Kontaktuppgift\n")}";
    }
}
