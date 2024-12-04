using System;

namespace Models;
public class Produkt2Order
{
    public int ID { get; set; } = -1;
    public int ProduktID { get; set; } = -1;
    public int OrderID { get; set; } = -1;
    public int Antal { get; set; } = 1;

    
    public Produkt? Produkt { get; set; }
    public Order? Order { get; set; }

    public override string ToString()
    {
        return $"ID: {ID}, ProduktID: {ProduktID}, OrderID: {OrderID}, Antal: {Antal}, " +
               $"Produkt: {(Produkt != null ? Produkt.Produktnamn : "Ingen Produkt")}, " +
               $"Order: {(Order != null ? $"OrderNr: {Order.Ordernr}" : "Ingen Order")}";
    }
}
