using Microsoft.Data.SqlClient;
using Models;
using System.Data;

namespace Repositories;
/// <summary>
/// Klass som innehåller service logiken(CRUD) specifikt för Kunder
/// </summary>
public class KundRepository
{
    private readonly string _connectionString;
    private readonly GenericRepository<Kund> _genericRepo;
    /// <summary>
    /// Constructorn tar in connectionstring och instansierar en ny GenericRepository för Kund klassen.
    /// </summary>
    /// <param name="connectionString"></param>
    public KundRepository(string connectionString)
    {
        _connectionString = connectionString;
        _genericRepo = new GenericRepository<Kund>(_connectionString);
    }
    #region CREATE Metoder
    /// <summary>
    /// Tar in parametrar för Add(CREATE) Metoden
    /// </summary>
    /// <param name="command"></param>
    /// <param name="kund"></param>
    public void AddKundParameters(SqlCommand command, Kund kund)
    {

        // In-parameters
        command.Parameters.Add("@Personnr", SqlDbType.NVarChar, 13).Value = kund.Personnr;
        command.Parameters.Add("@Förnamn", SqlDbType.NVarChar, 32).Value = kund.Förnamn;
        command.Parameters.Add("@Efternamn", SqlDbType.NVarChar, 32).Value = kund.Efternamn;

        // Out-parameter
        command.Parameters.Add("@ID", SqlDbType.Int).Direction = ParameterDirection.Output;

    }
    /// <summary>
    /// Hanterar output värdet för "ID" från SQL och tilldelar till Kund instansen
    /// </summary>
    /// <param name="command"></param>
    /// <param name="kund"></param>
    public void HandleOutPutKund(SqlCommand command, Kund kund)
    {
        kund.ID = (int)command.Parameters["@ID"].Value;

    }
    /// <summary>
    /// Lägger till en ny Kund i Tellus DB via GenericRepository och Stored Procedure "AddKund"
    /// </summary>
    /// <param name="kund"></param>
    public void AddKund(Kund kund)
    {
        _genericRepo.AddEntity(kund, AddKundParameters, HandleOutPutKund, "AddKund");

    }
    #endregion
    #region READ Metoder
    /// <summary>
    /// Tar in parametrar (ID) för Get(READ) metoden
    /// </summary>
    /// <param name="command"></param>
    /// <param name="id"></param>
    public static void GetKundParameters(SqlCommand command, int id)
    {
        command.Parameters.Add("@ID", SqlDbType.Int).Value = id;
    }
    /// <summary>
    /// Läser och Mappar konverterade värden från SQL till en ny instans av entityn
    /// </summary>
    /// <param name="reader"></param>
    /// <returns></returns>
    public static Kund MapToKund(SqlDataReader reader)
    {
        return new Kund
               (
                   reader.GetString(reader.GetOrdinal("Personnr")),
                   reader.GetString(reader.GetOrdinal("Förnamn")),
                   reader.GetString(reader.GetOrdinal("Efternamn")),
                   (int)reader["ID"]
               );
    }
    /// <summary>
    /// Hämtar en Kund post från Tellus DB genom GenericRepository, utifrån angivet ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Kund? GetKundByID(int id)
    {
        return _genericRepo.GetByID(id, GetKundParameters, MapToKund, "select * from Kunder where ID = @ID");

    }
    /// <summary>
    /// Hämtar samtliga Kund poster från Tellus DB genom GenericRepository
    /// </summary>
    /// <returns>en lista av Kunder</returns>
    public List<Kund>? GetKunder()
    {

        return _genericRepo.GetEntities(MapToKund, "select * from Kunder");

    }
    #endregion

    #region UPDATE Metoder
    /// <summary>
    /// Tar in parametrar som skall uppdateras i UpdateKund metoden.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="kund"></param>
    public static void UpdateKundParameters(SqlCommand command, Kund kund)
    {
        command.Parameters.Add("@ID", SqlDbType.Int).Value = kund.ID;
        command.Parameters.Add("@Personnr", SqlDbType.NVarChar, 13).Value = kund.Personnr;
        command.Parameters.Add("@Förnamn", SqlDbType.NVarChar, 32).Value = kund.Förnamn;
        command.Parameters.Add("@Efternamn", SqlDbType.NVarChar, 32).Value = kund.Efternamn;
    }
    /// <summary>
    /// Updaterar en specifik Kund post i Tellus DB utifrån giltigt ID, genom GenericRepository och via Stored Procedure "UpdateKund"
    /// </summary>
    /// <param name="kund"></param>
    /// <exception cref="ArgumentException"></exception>
    public void UpdateKund(Kund kund)
    {
        if (kund.ID <= 0)
        {
            throw new ArgumentException("Ogiltigt eller tomt ID, fyll i korrekt ID\n\n");
        }

        if (GetKundByID(kund.ID) == null)
        {
            throw new ArgumentException($"Ingen kund med ID {kund.ID} hittades i databasen. \n\n");
        }

        _genericRepo.Update(kund, UpdateKundParameters, null, "UpdateKund");

    }
    #endregion

    #region DELETE Metod
    /// <summary>
    /// Deletar en specifik Adress post i Tellus DB utifrån ID, genom GenericRepository och via Stored Procedure "CascadeDeleteKund"
    /// </summary>
    /// <param name="id"></param>
    public void DeleteKundByID(int id)
    {
        _genericRepo.DeleteEntity(id, GetKundParameters, "CascadeDeleteKund");

    }
    #endregion

}
