using Microsoft.Data.SqlClient;
using Models;
using System.Data;

namespace Repositories;
/// <summary>
/// Klass som innehåller servicelogiken(CRUD) specifikt för Kontaktuppgifter
/// </summary>
public class KontaktuppgiftRepository
{
    private readonly string _connectionString;
    private GenericRepository<Kontaktuppgift> _genericRepo;
    /// <summary>
    /// Constructorn tar in connectionstring och instansierar en ny GenericRepository för Kontaktuppgift klassen.
    /// </summary>
    /// <param name="connectionString"></param>
    public KontaktuppgiftRepository(string connectionString)
    {
        _connectionString = connectionString;
        _genericRepo = new GenericRepository<Kontaktuppgift>(_connectionString);
    }
    #region CREATE Metoder
    /// <summary>
    /// Tar in parametrar för Add(CREATE) Metoden
    /// </summary>
    /// <param name="command"></param>
    /// <param name="kontaktuppgift"></param>
    public void AddKontaktuppgiftParameters(SqlCommand command, Kontaktuppgift kontaktuppgift)
    {
        // In-parameters
        command.Parameters.Add("@Kontakttyp", SqlDbType.NVarChar, 16).Value = kontaktuppgift.Kontakttyp;
        command.Parameters.Add("@Kontaktvärde", SqlDbType.NVarChar, 64).Value = kontaktuppgift.Kontaktvärde;

        // Out-parameter
        command.Parameters.Add("@ID", SqlDbType.Int).Direction = ParameterDirection.Output;
    }
    /// <summary>
    /// Hanterar output värdet för "ID" från SQL och tilldelar till Kontaktuppgift instansen.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="kontaktuppgift"></param>
    public void HandleOutPutKontaktuppgift(SqlCommand command, Kontaktuppgift kontaktuppgift)
    {
        kontaktuppgift.ID = (int)command.Parameters["@ID"].Value;
    }
    /// <summary>
    /// Lägger till en ny Kontaktuppgift i Tellus DB via GenericRepository och Stored Procedure "AddKontaktuppgift"
    /// </summary>
    /// <param name="kontaktuppgift"></param>
    public void AddKontaktuppgift(Kontaktuppgift kontaktuppgift)
    {
        _genericRepo.AddEntity(kontaktuppgift, AddKontaktuppgiftParameters, HandleOutPutKontaktuppgift, "AddKontaktuppgift");
    }
    #endregion
    #region READ Metoder
    /// <summary>
    /// Tar in parametrar (ID) för Get(READ) metoden
    /// </summary>
    /// <param name="command"></param>
    /// <param name="id"></param>
    public static void GetKontaktuppgiftParameters(SqlCommand command, int id)
    {
        command.Parameters.Add("@ID", SqlDbType.Int).Value = id;
    }
    /// <summary>
    /// Läser och Mappar konverterade värden från SQL till en ny instans av entityn
    /// </summary>
    /// <param name="reader"></param>
    /// <returns></returns>
    public static Kontaktuppgift MapToKontaktuppgift(SqlDataReader reader)
    {
        return new Kontaktuppgift(
            reader.GetString(reader.GetOrdinal("Kontakttyp")),
            reader.GetString(reader.GetOrdinal("Kontaktvärde")),
            reader.GetInt32(reader.GetOrdinal("ID"))
        );
    }
    /// <summary>
    /// Hämtar en Kontaktuppgift post från Tellus DB genom GenericRepository, utifrån angivet ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>en Kontaktuppgift</returns>
    public Kontaktuppgift? GetKontaktuppgiftByID(int id)
    {
        return _genericRepo.GetByID(id, GetKontaktuppgiftParameters, MapToKontaktuppgift, "select * from Kontaktuppgifter where ID = @ID");
    }
    /// <summary>
    /// Hämtar samtldiga Kontaktuppgift poster från Tellus DB genom GenericRepository
    /// </summary>
    /// <returns>en lista av Kontaktuppgifter</returns>
    public List<Kontaktuppgift>? GetKontaktuppgifter()
    {
        return _genericRepo.GetEntities(MapToKontaktuppgift, "select * from Kontaktuppgifter");
    }
    #endregion

    #region UPDATE Metoder
    /// <summary>
    /// Tar in parametrar som skall uppdateras i UpdateKontaktuppgift metoden.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="kontaktuppgift"></param>
    public static void UpdateKontaktuppgiftParameters(SqlCommand command, Kontaktuppgift kontaktuppgift)
    {
        command.Parameters.Add("@ID", SqlDbType.Int).Value = kontaktuppgift.ID;
        command.Parameters.Add("@Kontakttyp", SqlDbType.NVarChar, 16).Value = kontaktuppgift.Kontakttyp;
        command.Parameters.Add("@Kontaktvärde", SqlDbType.NVarChar, 64).Value = kontaktuppgift.Kontaktvärde;
    }
    /// <summary>
    /// Updaterar en specifik Kontaktuppgift post i Tellus DB utifrån giltigt ID, genom GenericRepository och via Stored Procedure "UpdateKontaktuppgift"
    /// </summary>
    /// <param name="kontaktuppgift"></param>
    /// <exception cref="ArgumentException"></exception>
    public void UpdateKontaktuppgift(Kontaktuppgift kontaktuppgift)
    {
        if (kontaktuppgift.ID <= 0)
        {
            throw new ArgumentException("Ogiltigt eller tomt ID, fyll i korrekt ID\n\n");
        }
        if (GetKontaktuppgiftByID(kontaktuppgift.ID) == null)
        {
            throw new ArgumentException($"Ingen kontaktuppgift med ID {kontaktuppgift.ID} hittades i databasen\n\n");
        }

        _genericRepo.Update(kontaktuppgift, UpdateKontaktuppgiftParameters, "UpdateKontaktuppgift");
    }
    #endregion
    #region DELETE Metod
    /// <summary>
    /// Deletar en specifik Kontaktuppgift post i Tellus DB utifrån ID, genom GenericRepository och via Stored Procedure "CascadeDeleteKontaktuppgift"
    /// </summary>
    /// <param name="id"></param>
    public void DeleteKontaktuppgiftByID(int id)
    {
        _genericRepo.DeleteEntity(id, GetKontaktuppgiftParameters, "CascadeDeleteKontaktuppgift");
    }
    #endregion
}
