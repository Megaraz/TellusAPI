using Microsoft.Data.SqlClient;
using Models;
using System.Data;

namespace Repositories;

public class KundRepository
{
    private readonly string _connectionString;
    private GenericRepository<Kund> _genericRepo;

    public KundRepository(string connectionString)
    {
        _connectionString = connectionString;
        _genericRepo = new GenericRepository<Kund>(_connectionString);
    }

    #region CREATE Metoder
    public void AddKundParameters(SqlCommand command, Kund kund)
    {

        // In-parameters
        command.Parameters.Add("@Personnr", SqlDbType.NVarChar, 13).Value = kund.Personnr;
        command.Parameters.Add("@Förnamn", SqlDbType.NVarChar, 32).Value = kund.Förnamn;
        command.Parameters.Add("@Efternamn", SqlDbType.NVarChar, 32).Value = kund.Efternamn;

        // Out-parameter
        command.Parameters.Add("@ID", SqlDbType.Int).Direction = ParameterDirection.Output;

    }

    public void HandleOutPutKund(SqlCommand command, Kund kund)
    {
        kund.ID = (int)command.Parameters["@ID"].Value;

    }

    public void AddKund(Kund kund)
    {
        _genericRepo.AddEntity(kund, AddKundParameters, HandleOutPutKund, "AddKund");

    }
    #endregion

    #region READ Metoder
    public static void GetKundParameters(SqlCommand command, int id)
    {
        command.Parameters.Add("@ID", SqlDbType.Int).Value = id;
    }

    public static Kund MapToKund(SqlDataReader reader)
    {
        return new Kund
               (
                   reader.IsDBNull("Personnr") ? null : (string)reader["Personnr"],
                   reader.IsDBNull("Förnamn") ? null : (string)reader["Förnamn"],
                   reader.IsDBNull("Efternamn") ? null : (string)reader["Efternamn"],
                   (int)reader["ID"]
               );
    }

    public Kund? GetKundByID(int id)
    {
        return _genericRepo.GetByID(id, GetKundParameters, MapToKund, "select * from Kunder where ID = @ID");

    }
    public List<Kund>? GetKunder()
    {

        return _genericRepo.GetEntities(MapToKund, "select * from Kunder");

    }
    #endregion

    #region UPDATE Metoder
    public static void UpdateKundParameters(SqlCommand command, Kund kund)
    {
        command.Parameters.Add("@ID", SqlDbType.Int).Value = kund.ID;
        command.Parameters.Add("@Personnr", SqlDbType.NVarChar, 13).Value = kund.Personnr;
        command.Parameters.Add("@Förnamn", SqlDbType.NVarChar, 32).Value = kund.Förnamn;
        command.Parameters.Add("@Efternamn", SqlDbType.NVarChar, 32).Value = kund.Efternamn;
    }

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

        _genericRepo.Update(kund, UpdateKundParameters, "UpdateKund");

    }
    #endregion

    #region DELETE Metod
    /// <summary>
    /// Cascade delete görs automatiskt då jag i tabelldefinitionerna lagt till "on delete cascade"
    /// </summary>
    /// <param name="id"></param>
    public void DeleteKundByID(int id)
    {
        _genericRepo.DeleteEntity(id, GetKundParameters, "CascadeDeleteKund");

    }
    #endregion

}
