using System.Data;
using Models;
using Microsoft.Data.SqlClient;

namespace Repositories;
/// <summary>
/// Klass som innehåller service logiken(CRUD) specifikt för Produkt2Order
/// </summary>
public class Produkt2OrderRepository
{
    private readonly string _connectionString;

    /// <summary>
    /// Construcorn tar in connectionstring och tilldelar det privata fältet.
    /// </summary>
    /// <param name="connectionString"></param>
    public Produkt2OrderRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    #region CREATE
    /// <summary>
    /// Lägger till en post i tabellen Produkt2Order via StoredProcedure och därmed kopplar ihop en Produkt med en Order
    /// </summary>
    /// <param name="produkt2Order"></param>
    public void AddProdukt2Order(Produkt2Order produkt2Order)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        using SqlCommand command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "AddProdukt2Order";

        command.Parameters.Add("@ProduktID", SqlDbType.Int).Value = produkt2Order.ProduktID;
        command.Parameters.Add("@OrderID", SqlDbType.Int).Value = produkt2Order.OrderID;
        command.Parameters.Add("@Antal", SqlDbType.Int).Value = produkt2Order.Antal;

        command.Parameters.Add("@ID", SqlDbType.Int).Direction = ParameterDirection.Output;

        command.ExecuteNonQuery();

        // Tilldela ID till relationen
        produkt2Order.ID = (int)command.Parameters["@ID"].Value;

        ProduktRepository produktRepo = new ProduktRepository(_connectionString);
        OrderRepository orderRepo = new OrderRepository(_connectionString);

        // Hämta relaterade data och tilldela
        produkt2Order.Produkt = produktRepo.GetProduktByID(produkt2Order.ProduktID);
        produkt2Order.Order = orderRepo.GetOrderByID(produkt2Order.OrderID);
    }
    #endregion

    #region READ
    /// <summary>
    /// Hämtar alla produkter kopplade till angivet OrderID via StoredProcedure
    /// </summary>
    /// <param name="orderID"></param>
    /// <returns></returns>
    public List<Produkt2Order> GetProdukterByOrderID(int orderID)
    {
        List<Produkt2Order> produkt2Orders = new List<Produkt2Order>();

        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        using SqlCommand command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "GetProdukterByOrderID";
        command.Parameters.Add("@OrderID", SqlDbType.Int).Value = orderID;

        using SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Produkt produkt = new Produkt
            {
                ID = reader.GetInt32(reader.GetOrdinal("ProduktID")),
                Produktnamn = reader.GetString(reader.GetOrdinal("Produktnamn")),
                Pris = reader.GetDecimal(reader.GetOrdinal("Pris"))
            };

            Order order = new Order
            {
                ID = reader.GetInt32(reader.GetOrdinal("OrderID")),
                Ordernr = reader.GetInt32(reader.GetOrdinal("Ordernr"))
            };

            produkt2Orders.Add
                (
                    new Produkt2Order
                        {
                            ID = reader.GetInt32(reader.GetOrdinal("ID")),
                            ProduktID = produkt.ID,
                            OrderID = order.ID,
                            Antal = reader.GetInt32(reader.GetOrdinal("Antal")),
                            Produkt = produkt,
                            Order = order
                        }
                );
        }

        return produkt2Orders;
    }
    #endregion

    #region UPDATE
    /// <summary>
    /// Uppdaterar en specifik relation mellan Produkt och Order i Produkt2Order-tabellen, via StoredProcedure
    /// </summary>
    /// <param name="produkt2Order"></param>
    public void UpdateProdukt2Order(Produkt2Order produkt2Order)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        using SqlCommand command = connection.CreateCommand();

        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "UpdateProdukt2Order";

        command.Parameters.Add("@ID", SqlDbType.Int).Value = produkt2Order.ID;
        command.Parameters.Add("@ProduktID", SqlDbType.Int).Value = produkt2Order.ProduktID;
        command.Parameters.Add("@OrderID", SqlDbType.Int).Value = produkt2Order.OrderID;
        command.Parameters.Add("@Antal", SqlDbType.Int).Value = produkt2Order.Antal;

        command.ExecuteNonQuery();

        ProduktRepository produktRepo = new ProduktRepository(_connectionString);
        OrderRepository orderRepo = new OrderRepository(_connectionString);

        // Hämta relaterade data och tilldela
        produkt2Order.Produkt = produktRepo.GetProduktByID(produkt2Order.ProduktID);
        produkt2Order.Order = orderRepo.GetOrderByID(produkt2Order.OrderID);
    }
    #endregion

    #region DELETE
    /// <summary>
    /// Tar bort en specifik relation mellan Produkt och Order baserat på ID via StoredProcedure
    /// </summary>
    /// <param name="id"></param>
    public void DeleteProdukt2Order(int id)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        using SqlCommand command = connection.CreateCommand();

        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "DeleteProdukt2Order";

        command.Parameters.Add("@ID", SqlDbType.Int).Value = id;

        command.ExecuteNonQuery();
    }
    #endregion
}
