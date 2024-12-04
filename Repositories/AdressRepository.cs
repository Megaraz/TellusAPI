using Microsoft.Data.SqlClient;
using Models;
using System.Data;

namespace Repositories;
/// <summary>
/// Klass som innehåller service logiken(CRUD) specifikt för Adresser
/// </summary>
public class AdressRepository
{
    private readonly string _connectionString;
    private readonly GenericRepository<Adress> _genericRepo;
    /// <summary>
    /// Constructorn tar in connectionstring och instansierar en ny GenericRepository för Adress klassen.
    /// </summary>
    /// <param name="connectionString"></param>
    public AdressRepository(string connectionString)
    {
        _connectionString = connectionString;
        _genericRepo = new GenericRepository<Adress>(_connectionString);
    }

    #region CREATE Metoder
    /// <summary>
    /// Tar in parametrar för Add(CREATE) Metoden
    /// </summary>
    /// <param name="command"></param>
    /// <param name="adress"></param>
    public void AddAdressParameters(SqlCommand command, Adress adress)
    {
        // In-parameters
        command.Parameters.Add("@Gatuadress", SqlDbType.NVarChar, 32).Value = adress.Gatuadress;
        command.Parameters.Add("@Ort", SqlDbType.NVarChar, 32).Value = adress.Ort;
        command.Parameters.Add("@Postnr", SqlDbType.NVarChar, 6).Value = adress.Postnr;
        command.Parameters.Add("@LghNummer", SqlDbType.NVarChar, 4).Value = adress.LghNummer ?? (object)DBNull.Value;

        // Out-parameter
        command.Parameters.Add("@ID", SqlDbType.Int).Direction = ParameterDirection.Output;
    }
    /// <summary>
    /// Hanterar output värdet för "ID" från SQL och tilldelar till Adress instansen.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="adress"></param>
    public void HandleOutPutAdress(SqlCommand command, Adress adress)
    {
        adress.ID = (int)command.Parameters["@ID"].Value;
    }
    /// <summary>
    /// Lägger till en ny Adress i Tellus DB via GenericRepository och Stored Procedure "AddAdress"
    /// </summary>
    /// <param name="adress"></param>
    public void AddAdress(Adress adress)
    {
        _genericRepo.AddEntity(adress, AddAdressParameters, HandleOutPutAdress, "AddAdress");
    }
    #endregion
    #region READ Metoder
    /// <summary>
    /// Tar in parametrar (ID) för Get(READ) metoden
    /// </summary>
    /// <param name="command"></param>
    /// <param name="id"></param>
    public static void GetAdressParameters(SqlCommand command, int id)
    {
        command.Parameters.Add("@ID", SqlDbType.Int).Value = id;
    }
    /// <summary>
    /// Läser och Mappar konverterade värden från SQL till en ny instans av entityn
    /// </summary>
    /// <param name="reader"></param>
    /// <returns></returns>
    public static Adress MapToAdress(SqlDataReader reader)
    {
        return new Adress(
            reader.GetString(reader.GetOrdinal("Gatuadress")),
            reader.GetString(reader.GetOrdinal("Ort")),
            reader.GetString(reader.GetOrdinal("Postnr")),
            reader.IsDBNull("Lgh nummer") ? null : reader.GetString(reader.GetOrdinal("Lgh nummer")),
            reader.GetInt32(reader.GetOrdinal("ID"))
        );
    }
    /// <summary>
    /// Hämtar en Adress post från Tellus DB genom GenericRepository, utifrån angivet ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>en Adress</returns>
    public Adress? GetAdressByID(int id)
    {
        return _genericRepo.GetByID(id, GetAdressParameters, MapToAdress, "select * from Adresser where ID = @ID");
    }
    /// <summary>
    /// Hämtar samtliga Adress poster från Tellus DB genom GenericRepository
    /// </summary>
    /// <returns>en lista av Adresser</returns>
    public List<Adress>? GetAdresser()
    {
        return _genericRepo.GetEntities(MapToAdress, "select * from Adresser");
    }
    #endregion

    #region UPDATE Metoder
    /// <summary>
    /// Tar in parametrar som skall uppdateras i UpdateAdress metoden.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="adress"></param>
    public static void UpdateAdressParameters(SqlCommand command, Adress adress)
    {
        command.Parameters.Add("@ID", SqlDbType.Int).Value = adress.ID;
        command.Parameters.Add("@Gatuadress", SqlDbType.NVarChar, 32).Value = adress.Gatuadress;
        command.Parameters.Add("@Ort", SqlDbType.NVarChar, 32).Value = adress.Ort;
        command.Parameters.Add("@Postnr", SqlDbType.NVarChar, 6).Value = adress.Postnr;
        command.Parameters.Add("@LghNummer", SqlDbType.NVarChar, 4).Value = adress.LghNummer ?? (object)DBNull.Value;
    }
    /// <summary>
    /// Updaterar en specifik Adress post i Tellus DB utifrån giltigt ID, genom GenericRepository och via Stored Procedure "UpdateAdress"
    /// </summary>
    /// <param name="adress"></param>
    /// <exception cref="ArgumentException"></exception>
    public void UpdateAdress(Adress adress)
    {
        if (adress.ID <= 0)
        {
            throw new ArgumentException("Ogiltigt eller tomt ID, fyll i korrekt ID\n\n");
        }

        if (GetAdressByID(adress.ID) == null)
        {
            throw new ArgumentException($"Ingen adress med ID {adress.ID} hittades i databasen.\n\n");
        }
        _genericRepo.Update(adress, UpdateAdressParameters, null, "UpdateAdress");
    }
    #endregion
    #region DELETE Metod
    /// <summary>
    /// Deletar en specifik Adress post i Tellus DB utifrån ID, genom GenericRepository och via Stored Procedure "CascadeDeleteAdress"
    /// </summary>
    /// <param name="id"></param>
    public void DeleteAdressByID(int id)
    {
        _genericRepo.DeleteEntity(id, GetAdressParameters, "CascadeDeleteAdress");
    }
    #endregion
}
