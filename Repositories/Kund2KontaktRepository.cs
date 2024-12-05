using System;
using System.Data;
using Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace Repositories;
/// <summary>
/// Klass som innehåller service logiken(CRUD) specifikt för Kund2Kontakt
/// </summary>
public class Kund2KontaktRepository
{
    private readonly string _connectionString;

    public Kund2KontaktRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    #region CREATE
    /// <summary>
    /// Lägger till en post i tabellen Kund2Kontakt via StoredProcedure och därmed kopplar ihop en Kund med en Kontaktuppgift
    /// </summary>
    /// <param name="kund2Kontakt"></param>
    public void AddKund2Kontakt(Kund2Kontakt kund2Kontakt)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        using SqlCommand command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "AddKund2Kontakt";

        // In-parameters
        command.Parameters.Add("@KundID", SqlDbType.Int).Value = kund2Kontakt.KundID;
        command.Parameters.Add("@KontaktuppgiftID", SqlDbType.Int).Value = kund2Kontakt.KontaktuppgiftID;

        // Out-parameter
        command.Parameters.Add("@ID", SqlDbType.Int).Direction = ParameterDirection.Output;

        command.ExecuteNonQuery();

        // Tilldela ID till relationen
        kund2Kontakt.ID = (int)command.Parameters["@ID"].Value;

        KundRepository kundRepo = new KundRepository(_connectionString);
        KontaktuppgiftRepository kontaktuppgiftRepo = new KontaktuppgiftRepository(_connectionString);

        // Hämta relaterade data och tilldela
        kund2Kontakt.Kund = kundRepo.GetKundByID(kund2Kontakt.KundID);
        kund2Kontakt.Kontaktuppgift = kontaktuppgiftRepo.GetKontaktuppgiftByID(kund2Kontakt.KontaktuppgiftID);

    }
    #endregion

    #region READ
    /// <summary>
    /// Hämtar alla kontaktuppgifter kopplade till angivet KundID
    /// </summary>
    /// <param name="kundID"></param>
    /// <returns>En lista av Kund2Kontakt</returns>
    public List<Kund2Kontakt> GetKontaktuppgifterByKundID(int kundID)
    {
        List<Kund2Kontakt> kund2Kontakter = new List<Kund2Kontakt>();

        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        using SqlCommand command = connection.CreateCommand();

        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "GetKontaktuppgifterByKundID";

        command.Parameters.Add("@KundID", SqlDbType.Int).Value = kundID;

        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            Kund kund = new Kund
            {
                ID = reader.GetInt32(reader.GetOrdinal("KundID")),
                Personnr = reader.GetString(reader.GetOrdinal("Personnr")),
                Förnamn = reader.GetString(reader.GetOrdinal("Förnamn")),
                Efternamn = reader.GetString(reader.GetOrdinal("Efternamn"))
            };

            Kontaktuppgift kontaktuppgift = new Kontaktuppgift
            {
                ID = reader.GetInt32(reader.GetOrdinal("KontaktuppgiftID")),
                Kontakttyp = reader.GetString(reader.GetOrdinal("Kontakttyp")),
                Kontaktvärde = reader.GetString(reader.GetOrdinal("Kontaktvärde"))
            };

            kund2Kontakter.Add(
                new Kund2Kontakt
                {
                    ID = reader.GetInt32(reader.GetOrdinal("ID")),
                    KundID = kund.ID,
                    KontaktuppgiftID = kontaktuppgift.ID,
                    Kund = kund,
                    Kontaktuppgift = kontaktuppgift
                });
        }

        return kund2Kontakter;
    }

    /// <summary>
    /// Hämtar alla kunder kopplade till angivet KontaktuppgiftID
    /// </summary>
    /// <param name="kontaktuppgiftID"></param>
    /// <returns>En lista av Kund2Kontakt</returns>
    public List<Kund2Kontakt> GetKunderByKontaktuppgiftID(int kontaktuppgiftID)
    {
        List<Kund2Kontakt> kund2Kontakter = new List<Kund2Kontakt>();

        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        using SqlCommand command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "GetKunderByKontaktuppgiftID";
        command.Parameters.Add("@KontaktuppgiftID", SqlDbType.Int).Value = kontaktuppgiftID;

        using SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Kund kund = new Kund
            {
                ID = reader.GetInt32(reader.GetOrdinal("KundID")),
                Personnr = reader.GetString(reader.GetOrdinal("Personnr")),
                Förnamn = reader.GetString(reader.GetOrdinal("Förnamn")),
                Efternamn = reader.GetString(reader.GetOrdinal("Efternamn"))
            };

            Kontaktuppgift kontaktuppgift = new Kontaktuppgift
            {
                ID = reader.GetInt32(reader.GetOrdinal("KontaktuppgiftID")),
                Kontakttyp = reader.GetString(reader.GetOrdinal("Kontakttyp")),
                Kontaktvärde = reader.GetString(reader.GetOrdinal("Kontaktvärde"))
            };

            kund2Kontakter.Add(
                new Kund2Kontakt
                {
                    ID = reader.GetInt32(reader.GetOrdinal("ID")),
                    KundID = kund.ID,
                    KontaktuppgiftID = kontaktuppgift.ID,
                    Kund = kund,
                    Kontaktuppgift = kontaktuppgift
                });
        }

        return kund2Kontakter;
    }
    #endregion

    #region UPDATE
    /// <summary>
    /// Uppdaterar en specifik relation mellan Kund och Kontaktuppgift i Kund2Kontakt-tabellen.
    /// </summary>
    /// <param name="kund2Kontakt"></param>
    public void UpdateKund2Kontakt(Kund2Kontakt kund2Kontakt)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        using SqlCommand command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "UpdateKund2Kontakt";

        command.Parameters.Add("@ID", SqlDbType.Int).Value = kund2Kontakt.ID;
        command.Parameters.Add("@KundID", SqlDbType.Int).Value = kund2Kontakt.KundID;
        command.Parameters.Add("@KontaktuppgiftID", SqlDbType.Int).Value = kund2Kontakt.KontaktuppgiftID;

        command.ExecuteNonQuery();

        KundRepository kundRepo = new KundRepository(_connectionString);
        KontaktuppgiftRepository kontaktuppgiftRepo = new KontaktuppgiftRepository(_connectionString);

        // Hämta relaterade data och tilldela
        kund2Kontakt.Kund = kundRepo.GetKundByID(kund2Kontakt.KundID);
        kund2Kontakt.Kontaktuppgift = kontaktuppgiftRepo.GetKontaktuppgiftByID(kund2Kontakt.KontaktuppgiftID);
    }
    #endregion

    #region DELETE
    /// <summary>
    /// Tar bort en specifik relation mellan Kund och Kontaktuppgift baserat på ID
    /// </summary>
    /// <param name="id"></param>
    public void DeleteKund2Kontakt(int id)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        using SqlCommand command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "CascadeDeleteKund2Kontakt";

        command.Parameters.Add("@ID", SqlDbType.Int).Value = id;

        command.ExecuteNonQuery();


    }
    #endregion
}
