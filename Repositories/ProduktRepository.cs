using Microsoft.Data.SqlClient;
using Models;
using System.Data;

namespace Repositories;
/// <summary>
/// Klass som innehåller service logiken(CRUD) specifikt för Produkter
/// </summary>
public class ProduktRepository
{
    private readonly string _connectionString;
    private GenericRepository<Produkt> _genericRepo;
    /// <summary>
    /// Constructorn tar in connectionstring och instansierar en ny GenericRepository för Produkt klassen.
    /// </summary>
    /// <param name="connectionString"></param>
    public ProduktRepository(string connectionString)
    {
        _connectionString = connectionString;
        _genericRepo = new GenericRepository<Produkt>(_connectionString);
    }

    #region CREATE Metoder
    /// <summary>
    /// Tar in parametrar för Add(CREATE) Metoden
    /// </summary>
    /// <param name="command"></param>
    /// <param name="produkt"></param>
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
    /// <summary>
    /// Hanterar output värdet för "ID" från SQL och tilldelar till Produkt instansen.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="produkt"></param>
    public void HandleOutPutProdukt(SqlCommand command, Produkt produkt)
    {
        produkt.ID = (int)command.Parameters["@ID"].Value;
    }
    /// <summary>
    /// Lägger till en ny Produkt i Tellus DB via GenericRepository och Stored Procedure "AddProdukt"
    /// </summary>
    /// <param name="produkt"></param>
    public void AddProdukt(Produkt produkt)
    {
        _genericRepo.AddEntity(produkt, AddProduktParameters, HandleOutPutProdukt, "AddProdukt");
    }
    #endregion

    #region READ Metoder
    /// <summary>
    /// Tar in parametrar (ID) för Get(READ) metoden
    /// </summary>
    /// <param name="command"></param>
    /// <param name="id"></param>
    public static void GetProduktParameters(SqlCommand command, int id)
    {
        command.Parameters.Add("@ID", SqlDbType.Int).Value = id;
    }
    /// <summary>
    /// Läser och Mappar konverterade värden från SQL till en ny instans av entityn
    /// </summary>
    /// <param name="reader"></param>
    /// <returns></returns>
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
    /// <summary>
    /// Hämtar en Produkt post från Tellus DB genom GenericRepository, utifrån angivet ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Produkt? GetProduktByID(int id)
    {
        return _genericRepo.GetByID(id, GetProduktParameters, MapToProdukt, "select * from Produkter where ID = @ID");
    }
    /// <summary>
    /// Hämtar samtliga Produkt poster från Tellus DB genom GenericRepository
    /// </summary>
    /// <returns>en lista av Produkter</returns>
    public List<Produkt>? GetProdukter()
    {
        return _genericRepo.GetEntities(MapToProdukt, "select * from Produkter");
    }
    #endregion

    #region UPDATE Metoder
    /// <summary>
    /// Tar in parametrar som skall uppdateras i UpdateProdukt metoden.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="produkt"></param>
    public static void UpdateProduktParameters(SqlCommand command, Produkt produkt)
    {
        command.Parameters.Add("@ID", SqlDbType.Int).Value = produkt.ID;
        command.Parameters.Add("@ProduktTyp", SqlDbType.NVarChar, 16).Value = produkt.ProduktTyp ?? (object)DBNull.Value;
        command.Parameters.Add("@Produktnamn", SqlDbType.NVarChar, 64).Value = produkt.Produktnamn;
        command.Parameters.Add("@ProduktNummer", SqlDbType.NVarChar, 128).Value = produkt.ProduktNummer;
        command.Parameters.Add("@Pris", SqlDbType.Money).Value = produkt.Pris;
    }
    /// <summary>
    /// Updaterar en specifik Produkt post i Tellus DB utifrån giltigt ID, genom GenericRepository och via Stored Procedure "UpdateProdukt"
    /// </summary>
    /// <param name="produkt"></param>
    /// <exception cref="ArgumentException"></exception>
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

        _genericRepo.Update(produkt, UpdateProduktParameters, null, "UpdateProdukt");
    }
    #endregion

    #region DELETE Metod
    /// <summary>
    /// Deletar en specifik Produkt post i Tellus DB utifrån ID, genom GenericRepository och via Stored Procedure "CascadeDeleteProdukt"
    /// </summary>
    /// <param name="id"></param>
    public void DeleteProduktByID(int id)
    {
        _genericRepo.DeleteEntity(id, GetProduktParameters, "CascadeDeleteProdukt");
    }
    #endregion
}
