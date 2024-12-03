using Microsoft.Data.SqlClient;
using Models;
using System.Data;

namespace Repositories;

public class KontaktuppgiftRepository
{
    private readonly string _connectionString;
    private GenericRepository<Kontaktuppgift> _genericRepo;

    public KontaktuppgiftRepository(string connectionString)
    {
        _connectionString = connectionString;
        _genericRepo = new GenericRepository<Kontaktuppgift>(_connectionString);
    }

    #region CREATE Metoder
    public void AddKontaktuppgiftParameters(SqlCommand command, Kontaktuppgift kontaktuppgift)
    {
        // In-parameters
        command.Parameters.Add("@Kontakttyp", SqlDbType.NVarChar, 16).Value = kontaktuppgift.Kontakttyp;
        command.Parameters.Add("@Kontaktvärde", SqlDbType.NVarChar, 64).Value = kontaktuppgift.Kontaktvärde;

        // Out-parameter
        command.Parameters.Add("@ID", SqlDbType.Int).Direction = ParameterDirection.Output;
    }

    public void HandleOutPutKontaktuppgift(SqlCommand command, Kontaktuppgift kontaktuppgift)
    {
        kontaktuppgift.ID = (int)command.Parameters["@ID"].Value;
    }

    public void AddKontaktuppgift(Kontaktuppgift kontaktuppgift)
    {
        _genericRepo.AddEntity(kontaktuppgift, AddKontaktuppgiftParameters, HandleOutPutKontaktuppgift, "AddKontaktuppgift");
    }
    #endregion

    #region READ Metoder
    public static void GetKontaktuppgiftParameters(SqlCommand command, int id)
    {
        command.Parameters.Add("@ID", SqlDbType.Int).Value = id;
    }

    public static Kontaktuppgift MapToKontaktuppgift(SqlDataReader reader)
    {
        return new Kontaktuppgift(
            reader.GetString(reader.GetOrdinal("Kontakttyp")),
            reader.GetString(reader.GetOrdinal("Kontaktvärde")),
            reader.GetInt32(reader.GetOrdinal("ID"))
        );
    }

    public Kontaktuppgift? GetKontaktuppgiftByID(int id)
    {
        return _genericRepo.GetByID(id, GetKontaktuppgiftParameters, MapToKontaktuppgift, "select * from Kontaktuppgifter where ID = @ID");
    }

    public List<Kontaktuppgift>? GetKontaktuppgifter()
    {
        return _genericRepo.GetEntities(MapToKontaktuppgift, "select * from Kontaktuppgifter");
    }
    #endregion

    #region UPDATE Metoder
    public static void UpdateKontaktuppgiftParameters(SqlCommand command, Kontaktuppgift kontaktuppgift)
    {
        command.Parameters.Add("@ID", SqlDbType.Int).Value = kontaktuppgift.ID;
        command.Parameters.Add("@Kontakttyp", SqlDbType.NVarChar, 16).Value = kontaktuppgift.Kontakttyp;
        command.Parameters.Add("@Kontaktvärde", SqlDbType.NVarChar, 64).Value = kontaktuppgift.Kontaktvärde;
    }

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
    public void DeleteKontaktuppgiftByID(int id)
    {
        _genericRepo.DeleteEntity(id, GetKontaktuppgiftParameters, "CascadeDeleteKontaktuppgift");
    }
    #endregion
}
