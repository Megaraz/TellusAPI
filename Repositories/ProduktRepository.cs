using Microsoft.Data.SqlClient;
using Models;
using System.Data;

namespace Repositories;

public class ProduktRepository
{
    private readonly string _connectionString;
    private GenericRepository<Produkt> _genericRepo;

    public ProduktRepository(string connectionString)
    {
        _connectionString = connectionString;
        _genericRepo = new GenericRepository<Produkt>(_connectionString);
    }

    #region CREATE Metoder
    public void AddProduktParameters(SqlCommand command, Produkt produkt)
    {
        // In-parameters
        command.Parameters.Add("@ProduktTyp", SqlDbType.NVarChar, 16).Value = produkt.ProduktTyp ?? (object)DBNull.Value;
        command.Parameters.Add("@Produktnamn", SqlDbType.NVarChar, 64).Value = produkt.Produktnamn;
        command.Parameters.Add("@ProduktNummer", SqlDbType.NVarChar, 128).Value = produkt.ProduktNummer;
        command.Parameters.Add("@Pris", SqlDbType.Money).Value = produkt.Pris;

        // Out-parameter
        command.Parameters.Add("@ID", SqlDbType.Int).Direction = ParameterDirection.Output;
    }

    public void HandleOutPutProdukt(SqlCommand command, Produkt produkt)
    {
        produkt.ID = (int)command.Parameters["@ID"].Value;
    }

    public void AddProdukt(Produkt produkt)
    {
        _genericRepo.AddEntity(produkt, AddProduktParameters, HandleOutPutProdukt, "AddProdukt");
    }
    #endregion

    #region READ Metoder
    public static void GetProduktParameters(SqlCommand command, int id)
    {
        command.Parameters.Add("@ID", SqlDbType.Int).Value = id;
    }

    public static Produkt MapToProdukt(SqlDataReader reader)
    {
        return new Produkt(
            reader.GetString(reader.GetOrdinal("Produktnamn")),
            reader.GetString(reader.GetOrdinal("ProduktNummer")),
            reader.GetDecimal(reader.GetOrdinal("Pris")),
            reader.IsDBNull("ProduktTyp") ? null : reader.GetString(reader.GetOrdinal("ProduktTyp")),
            reader.GetInt32(reader.GetOrdinal("ID"))
        );
    }

    public Produkt? GetProduktByID(int id)
    {
        return _genericRepo.GetByID(id, GetProduktParameters, MapToProdukt, "select * from Produkter where ID = @ID");
    }

    public List<Produkt>? GetProdukter()
    {
        return _genericRepo.GetEntities(MapToProdukt, "select * from Produkter");
    }
    #endregion

    #region UPDATE Metoder
    public static void UpdateProduktParameters(SqlCommand command, Produkt produkt)
    {
        command.Parameters.Add("@ID", SqlDbType.Int).Value = produkt.ID;
        command.Parameters.Add("@ProduktTyp", SqlDbType.NVarChar, 16).Value = produkt.ProduktTyp ?? (object)DBNull.Value;
        command.Parameters.Add("@Produktnamn", SqlDbType.NVarChar, 64).Value = produkt.Produktnamn;
        command.Parameters.Add("@ProduktNummer", SqlDbType.NVarChar, 128).Value = produkt.ProduktNummer;
        command.Parameters.Add("@Pris", SqlDbType.Money).Value = produkt.Pris;
    }

    public void UpdateProdukt(Produkt produkt)
    {
        if (produkt.ID <= 0)
        {
            throw new ArgumentException("Ogiltigt eller tomt ID, fyll i korrekt ID\n\n");
        }
        if (GetProduktByID(produkt.ID) == null)
        {
            throw new ArgumentException($"Ingen produkt med ID {produkt.ID} hittades i databasen.\n\n");
        }

        _genericRepo.Update(produkt, UpdateProduktParameters, "UpdateProdukt");
    }
    #endregion

    #region DELETE Metod
    public void DeleteProduktByID(int id)
    {
        _genericRepo.DeleteEntity(id, GetProduktParameters, "CascadeDeleteProdukt");
    }
    #endregion
}
