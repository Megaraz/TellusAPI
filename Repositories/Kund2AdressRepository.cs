using System;
using System.Data;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Repositories;
/// <summary>
/// Klass som innehåller service logiken(CRUD) specifikt för Kund2Adresser
/// </summary>
public class Kund2AdressRepository
{
    private readonly string _connectionString;


    public Kund2AdressRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
    #region CREATE
    /// <summary>
    /// Lägger till en post i tabellen Kund2Adress via StoredProcedure och därmed kopplar ihop en Kund med en Adress
    /// </summary>
    /// <param name="kund2Adress"></param>
    public void AddKund2Adress(Kund2Adress kund2Adress)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        using SqlCommand command = connection.CreateCommand();

        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "AddKund2Adress";

        // In-parameters
        command.Parameters.Add("@KundID", SqlDbType.Int).Value = kund2Adress.KundID;
        command.Parameters.Add("@AdressID", SqlDbType.Int).Value = kund2Adress.AdressID;

        // Out-parameters
        command.Parameters.Add("@ID", SqlDbType.Int).Direction = ParameterDirection.Output;
        


        command.ExecuteNonQuery();

        // Tilldelar ID värdet
        kund2Adress.ID = (int)command.Parameters["@ID"].Value;

        KundRepository kundRepo = new KundRepository(_connectionString);
        AdressRepository adressRepo = new AdressRepository(_connectionString);

        // Hämta relaterade data och tilldela
        kund2Adress.Kund = kundRepo.GetKundByID(kund2Adress.KundID);
        kund2Adress.Adress = adressRepo.GetAdressByID(kund2Adress.AdressID);

    }
    #endregion

    #region READ
    /// <summary>
    /// Hämtar alla adresser kopplade till angivet KundID
    /// </summary>
    /// <param name="kunderID"></param>
    /// <returns></returns>
    public List<Kund2Adress> GetAdresserByKundID(int kunderID)
    {

        List<Kund2Adress> kund2Adresser = new List<Kund2Adress>();

        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        using SqlCommand command = connection.CreateCommand();

        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "GetAdresserByKundID";

        command.Parameters.Add("@KundID", SqlDbType.Int).Value = kunderID;

        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {

            Kund kund = new Kund()
            {
                ID = reader.GetInt32(reader.GetOrdinal("KundID")),
                Personnr = reader.GetString(reader.GetOrdinal("Personnr")),
                Förnamn = reader.GetString(reader.GetOrdinal("Förnamn")),
                Efternamn = reader.GetString(reader.GetOrdinal("Efternamn"))
            };

            Adress adress = new Adress()
            {
                ID = reader.GetInt32(reader.GetOrdinal("AdressID")),
                Gatuadress = reader.GetString(reader.GetOrdinal("Gatuadress")),
                Ort = reader.GetString(reader.GetOrdinal("Ort")),
                Postnr = reader.GetString(reader.GetOrdinal("Postnr")),
                LghNummer = reader.IsDBNull(reader.GetOrdinal("Lgh nummer")) ? null : reader.GetString(reader.GetOrdinal("Lgh nummer"))
            };

            kund2Adresser.Add
                (
                    new Kund2Adress()
                        {
                            ID = reader.GetInt32(reader.GetOrdinal("ID")),
                            KundID = kund.ID,
                            AdressID = adress.ID,
                            Kund = kund,
                            Adress = adress
                        }
                );
        }

        return kund2Adresser;

    }
    /// <summary>
    /// Hämtar alla kunder kopplade till angivet AdressID
    /// </summary>
    /// <param name="adressID"></param>
    /// <returns></returns>
    public List<Kund2Adress> GetKunderByAdressID(int adressID)
    {
        List<Kund2Adress> kund2Adresser = new List<Kund2Adress>();

        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        using SqlCommand command = connection.CreateCommand();

        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "GetKunderByAdressID";
        command.Parameters.Add("@AdressID", SqlDbType.Int).Value = adressID;

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

            Adress adress = new Adress
            {
                ID = reader.GetInt32(reader.GetOrdinal("AdressID")),
                Gatuadress = reader.GetString(reader.GetOrdinal("Gatuadress")),
                Ort = reader.GetString(reader.GetOrdinal("Ort")),
                Postnr = reader.GetString(reader.GetOrdinal("Postnr")),
                LghNummer = reader.IsDBNull(reader.GetOrdinal("Lgh nummer"))
                            ? null
                            : reader.GetString(reader.GetOrdinal("Lgh nummer"))
            };

            kund2Adresser.Add
                (
                    new Kund2Adress
                        {
                            ID = reader.GetInt32(reader.GetOrdinal("ID")),
                            KundID = kund.ID,
                            AdressID = adress.ID,
                            Kund = kund,
                            Adress = adress
                        }
                );
        }

        return kund2Adresser;
    }


    #endregion

    #region UPDATE
    /// <summary>
    /// Uppdaterar en specifik post i Kund2Adress via Stored Procedure
    /// </summary>
    /// <param name="kund2Adress"></param>
    public void UpdateKund2Adress(Kund2Adress kund2Adress)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        using SqlCommand command = connection.CreateCommand();

        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "UpdateKund2Adress";

        command.Parameters.Add("@ID", SqlDbType.Int).Value = kund2Adress.ID;
        command.Parameters.Add("@KundID", SqlDbType.Int).Value = kund2Adress.KundID;
        command.Parameters.Add("@AdressID", SqlDbType.Int).Value = kund2Adress.AdressID;

        command.ExecuteNonQuery();

        KundRepository kundRepo = new KundRepository(_connectionString);
        AdressRepository adressRepo = new AdressRepository(_connectionString);

        // Hämta relaterade data och tilldela
        kund2Adress.Kund = kundRepo.GetKundByID(kund2Adress.KundID);
        kund2Adress.Adress = adressRepo.GetAdressByID(kund2Adress.AdressID);
    }
    #endregion

    #region DELETE
    /// <summary>
    /// Tar bort en specifik relation mellan Kund och Adress baserat på ID.
    /// </summary>
    /// <param name="id"></param>
    public void DeleteKund2Adress(int id)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        using SqlCommand command = connection.CreateCommand();

        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "CascadeDeleteKund2Adress";

        command.Parameters.Add("@ID", SqlDbType.Int).Value = id;

        command.ExecuteNonQuery();
    }
    #endregion
}
