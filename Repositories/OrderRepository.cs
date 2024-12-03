using Microsoft.Data.SqlClient;
using Models;
using System.Data;

namespace Repositories;

public class OrderRepository
{
    private readonly string _connectionString;
    private GenericRepository<Order> _genericRepo;

    public OrderRepository(string connectionString)
    {
        _connectionString = connectionString;
        _genericRepo = new GenericRepository<Order>(_connectionString);
    }

    #region CREATE Metoder
    public void AddOrderParameters(SqlCommand command, Order order)
    {
        // In-parameters
        command.Parameters.Add("@Ordernr", SqlDbType.Int).Value = order.Ordernr;
        command.Parameters.Add("@ÄrSkickad", SqlDbType.Bit).Value = order.ÄrSkickad;
        command.Parameters.Add("@ÄrLevererad", SqlDbType.Bit).Value = order.ÄrLevererad;
        command.Parameters.Add("@ÄrBetald", SqlDbType.Bit).Value = order.ÄrBetald;
        command.Parameters.Add("@Betalsystem", SqlDbType.NVarChar, 32).Value = order.Betalsystem ?? (object)DBNull.Value;
        command.Parameters.Add("@TidVidBeställning", SqlDbType.DateTime).Value = order.TidVidBeställning;
        command.Parameters.Add("@BeräknadLeverans", SqlDbType.DateTime).Value = order.BeräknadLeverans;
        command.Parameters.Add("@Kund2KontaktID", SqlDbType.Int).Value = order.Kund2KontaktID;

        // Out-parameter
        command.Parameters.Add("@ID", SqlDbType.Int).Direction = ParameterDirection.Output;
    }

    public void HandleOutPutOrder(SqlCommand command, Order order)
    {
        order.ID = (int)command.Parameters["@ID"].Value;
    }

    public void AddOrder(Order order)
    {
        _genericRepo.AddEntity(order, AddOrderParameters, HandleOutPutOrder, "AddOrder");
    }
    #endregion

    #region READ Metoder
    public static void GetOrderParameters(SqlCommand command, int id)
    {
        command.Parameters.Add("@ID", SqlDbType.Int).Value = id;
    }

    public static Order MapToOrder(SqlDataReader reader)
    {
        return new Order(
            reader.GetInt32(reader.GetOrdinal("Ordernr")),
            reader.GetBoolean(reader.GetOrdinal("ÄrSkickad")),
            reader.GetBoolean(reader.GetOrdinal("ÄrLevererad")),
            reader.GetBoolean(reader.GetOrdinal("ÄrBetald")),
            reader.GetDateTime(reader.GetOrdinal("TidVidBeställning")),
            reader.GetDateTime(reader.GetOrdinal("BeräknadLeverans")),
            reader.GetInt32(reader.GetOrdinal("Kund2KontaktID")),
            reader.IsDBNull("Betalsystem") ? null : reader.GetString(reader.GetOrdinal("Betalsystem")),
            reader.GetInt32(reader.GetOrdinal("ID"))
        );
    }

    public Order? GetOrderByID(int id)
    {
        return _genericRepo.GetByID(id, GetOrderParameters, MapToOrder, "select * from [Order] where ID = @ID");
    }

    public List<Order>? GetOrders()
    {
        return _genericRepo.GetEntities(MapToOrder, "select * from [Order]");
    }
    #endregion

    #region UPDATE Metoder
    public static void UpdateOrderParameters(SqlCommand command, Order order)
    {
        command.Parameters.Add("@ID", SqlDbType.Int).Value = order.ID;
        command.Parameters.Add("@Ordernr", SqlDbType.Int).Value = order.Ordernr;
        command.Parameters.Add("@ÄrSkickad", SqlDbType.Bit).Value = order.ÄrSkickad;
        command.Parameters.Add("@ÄrLevererad", SqlDbType.Bit).Value = order.ÄrLevererad;
        command.Parameters.Add("@ÄrBetald", SqlDbType.Bit).Value = order.ÄrBetald;
        command.Parameters.Add("@Betalsystem", SqlDbType.NVarChar, 32).Value = order.Betalsystem ?? (object)DBNull.Value;
        command.Parameters.Add("@TidVidBeställning", SqlDbType.DateTime).Value = order.TidVidBeställning;
        command.Parameters.Add("@BeräknadLeverans", SqlDbType.DateTime).Value = order.BeräknadLeverans;
        command.Parameters.Add("@Kund2KontaktID", SqlDbType.Int).Value = order.Kund2KontaktID;
    }

    public void UpdateOrder(Order order)
    {
        if (order.ID <= 0)
        {
            throw new ArgumentException("Ogiltigt eller tomt ID, fyll i korrekt ID\n\n");
        }
        if (GetOrderByID(order.ID) == null)
        {
            throw new ArgumentException($"Ingen order med ID {order.ID} hittades i databasen.\n\n");
        }


        _genericRepo.Update(order, UpdateOrderParameters, "UpdateOrder");
    }
    #endregion

    #region DELETE Metod
    public void DeleteOrderByID(int id)
    {
        _genericRepo.DeleteEntity(id, GetOrderParameters, "CascadeDeleteOrder");
    }
    #endregion
}
