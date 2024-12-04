using System.Data;
using Models;
using Microsoft.Data.SqlClient;

namespace Repositories;
public class Produkt2OrderRepository
{
    private readonly string _connectionString;

    public Produkt2OrderRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    #region CREATE
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

        produkt2Order.ID = (int)command.Parameters["@ID"].Value;
    }
    #endregion

    #region READ
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
    /// Uppdaterar en specifik relation mellan Produkt och Order i Produkt2Order-tabellen.
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
    }
    #endregion


    #region DELETE
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
