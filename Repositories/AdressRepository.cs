using Microsoft.Data.SqlClient;
using Models;
using System.Data;

namespace Repositories;

public class AdressRepository
{
    private readonly string _connectionString;
    private GenericRepository<Adress> _genericRepo;

    public AdressRepository(string connectionString)
    {
        _connectionString = connectionString;
        _genericRepo = new GenericRepository<Adress>(_connectionString);
    }

    #region CREATE Metoder
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

    public void HandleOutPutAdress(SqlCommand command, Adress adress)
    {
        adress.ID = (int)command.Parameters["@ID"].Value;
    }

    public void AddAdress(Adress adress)
    {
        _genericRepo.AddEntity(adress, AddAdressParameters, HandleOutPutAdress, "AddAdress");
    }
    #endregion

    #region READ Metoder
    public static void GetAdressParameters(SqlCommand command, int id)
    {
        command.Parameters.Add("@ID", SqlDbType.Int).Value = id;
    }

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

    public Adress? GetAdressByID(int id)
    {
        return _genericRepo.GetByID(id, GetAdressParameters, MapToAdress, "select * from Adresser where ID = @ID");
    }

    public List<Adress>? GetAdresser()
    {
        return _genericRepo.GetEntities(MapToAdress, "select * from Adresser");
    }
    #endregion

    #region UPDATE Metoder
    public static void UpdateAdressParameters(SqlCommand command, Adress adress)
    {
        command.Parameters.Add("@ID", SqlDbType.Int).Value = adress.ID;
        command.Parameters.Add("@Gatuadress", SqlDbType.NVarChar, 32).Value = adress.Gatuadress;
        command.Parameters.Add("@Ort", SqlDbType.NVarChar, 32).Value = adress.Ort;
        command.Parameters.Add("@Postnr", SqlDbType.NVarChar, 6).Value = adress.Postnr;
        command.Parameters.Add("@LghNummer", SqlDbType.NVarChar, 4).Value = adress.LghNummer ?? (object)DBNull.Value;
    }

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
        _genericRepo.Update(adress, UpdateAdressParameters, "UpdateAdress");
    }
    #endregion

    #region DELETE Metod
    public void DeleteAdressByID(int id)
    {
        _genericRepo.DeleteEntity(id, GetAdressParameters, "CascadeDeleteAdress");
    }
    #endregion
}
