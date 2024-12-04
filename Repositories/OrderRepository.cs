using Microsoft.Data.SqlClient;
using Models;
using System.Data;

namespace Repositories;
/// <summary>
/// Klass som innehåller service logiken(CRUD) specifikt för Order
/// </summary>
public class OrderRepository
{
    private readonly string _connectionString;
    private GenericRepository<Order> _genericRepo;
    /// <summary>
    /// Constructorn tar in connectionstring och instansierar en ny GenericRepository för Order klassen.
    /// </summary>
    /// <param name="connectionString"></param>
    public OrderRepository(string connectionString)
    {
        _connectionString = connectionString;
        _genericRepo = new GenericRepository<Order>(_connectionString);
    }

    #region CREATE Metoder
    /// <summary>
    /// Tar in parametrar för Add(CREATE) Metoden
    /// </summary>
    /// <param name="command"></param>
    /// <param name="order"></param>
    public void AddOrderParameters(SqlCommand command, Order order)
    {
        // In-parameters
        command.Parameters.Add("@ÄrSkickad", SqlDbType.Bit).Value = order.ÄrSkickad;
        command.Parameters.Add("@ÄrLevererad", SqlDbType.Bit).Value = order.ÄrLevererad;
        command.Parameters.Add("@ÄrBetald", SqlDbType.Bit).Value = order.ÄrBetald;
        command.Parameters.Add("@Betalsystem", SqlDbType.NVarChar, 32).Value = order.Betalsystem ?? (object)DBNull.Value;
        command.Parameters.Add("@TidVidBeställning", SqlDbType.DateTime).Value = order.TidVidBeställning;
        command.Parameters.Add("@BeräknadLeverans", SqlDbType.DateTime).Value = order.BeräknadLeverans;
        command.Parameters.Add("@KundID", SqlDbType.Int).Value = order.KundID;

        // Out-parameter
        command.Parameters.Add("@ID", SqlDbType.Int).Direction = ParameterDirection.Output;
        command.Parameters.Add("@Ordernr", SqlDbType.Int).Direction = ParameterDirection.Output;
    }
    /// <summary>
    /// Hanterar output värdet för "ID" från SQL och tilldelar till Order instansen.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="order"></param>
    public void HandleOutPutOrder(SqlCommand command, Order order)
    {
        order.Ordernr = (int)command.Parameters["@Ordernr"].Value;
        order.ID = (int)command.Parameters["@ID"].Value;
    }
    /// <summary>
    /// Lägger till en ny Order i Tellus DB via GenericRepository och Stored Procedure "AddOrder"
    /// </summary>
    /// <param name="order"></param>
    public void AddOrder(Order order)
    {
        _genericRepo.AddEntity(order, AddOrderParameters, HandleOutPutOrder, "AddOrder");
    }
    #endregion

    #region READ Metoder
    /// <summary>
    /// Tar in parametrar (ID) för Get(READ) metoden
    /// </summary>
    /// <param name="command"></param>
    /// <param name="id"></param>
    public static void GetOrderParameters(SqlCommand command, int id)
    {
        command.Parameters.Add("@ID", SqlDbType.Int).Value = id;
    }
    /// <summary>
    /// Läser och Mappar konverterade värden från SQL till en ny instans av entityn
    /// </summary>
    /// <param name="reader"></param>
    /// <returns></returns>
    public static Order MapToOrder(SqlDataReader reader)
    {
        return new Order(
            reader.GetBoolean(reader.GetOrdinal("ÄrSkickad")),
            reader.GetBoolean(reader.GetOrdinal("ÄrLevererad")),
            reader.GetBoolean(reader.GetOrdinal("ÄrBetald")),
            reader.GetDateTime(reader.GetOrdinal("TidVidBeställning")),
            reader.GetDateTime(reader.GetOrdinal("BeräknadLeverans")),
            reader.GetInt32(reader.GetOrdinal("KundID")),
            reader.IsDBNull("Betalsystem") ? null : reader.GetString(reader.GetOrdinal("Betalsystem")),
            reader.GetInt32(reader.GetOrdinal("ID")),
            reader.GetInt32(reader.GetOrdinal("Ordernr"))
        );
    }
    /// <summary>
    /// Hämtar en Order post från Tellus DB genom GenericRepository, utifrån angivet ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Order? GetOrderByID(int id)
    {
        return _genericRepo.GetByID(id, GetOrderParameters, MapToOrder, "select * from [Order] where ID = @ID");
    }
    /// <summary>
    /// Hämtar samtliga Order poster från Tellus DB genom GenericRepository
    /// </summary>
    /// <returns>en lista av Order</returns>
    public List<Order>? GetOrders()
    {
        return _genericRepo.GetEntities(MapToOrder, "select * from [Order]");
    }
    #endregion

    #region UPDATE Metoder
    /// <summary>
    /// Tar in parametrar som skall uppdateras i UpdateOrder metoden.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="order"></param>
    public static void UpdateOrderParameters(SqlCommand command, Order order)
    {
        // Input-parameter
        command.Parameters.Add("@ID", SqlDbType.Int).Value = order.ID;
        command.Parameters.Add("@ÄrSkickad", SqlDbType.Bit).Value = order.ÄrSkickad;
        command.Parameters.Add("@ÄrLevererad", SqlDbType.Bit).Value = order.ÄrLevererad;
        command.Parameters.Add("@ÄrBetald", SqlDbType.Bit).Value = order.ÄrBetald;
        command.Parameters.Add("@Betalsystem", SqlDbType.NVarChar, 32).Value = order.Betalsystem ?? (object)DBNull.Value;
        command.Parameters.Add("@TidVidBeställning", SqlDbType.DateTime).Value = order.TidVidBeställning;
        command.Parameters.Add("@BeräknadLeverans", SqlDbType.DateTime).Value = order.BeräknadLeverans;
        command.Parameters.Add("@Kund2KontaktID", SqlDbType.Int).Value = order.Kund2KontaktID;

        // Output-parameter
        command.Parameters.Add("@Ordernr", SqlDbType.Int).Direction = ParameterDirection.Output;
    }
    /// <summary>
    /// Updaterar en specifik Order post i Tellus DB utifrån giltigt ID, genom GenericRepository och via Stored Procedure "UpdateOrder"
    /// </summary>
    /// <param name="order"></param>
    /// <exception cref="ArgumentException"></exception>
    public void UpdateOrder(Order order)
    {
        _genericRepo.Update(order, UpdateOrderParameters, HandleOutPutOrder, "UpdateOrder");
    }
    #endregion

    #region DELETE Metod
    /// <summary>
    /// Deletar en specifik Order post i Tellus DB utifrån ID, genom GenericRepository och via Stored Procedure "CascadeDeleteOrder"
    /// </summary>
    /// <param name="id"></param>
    public void DeleteOrderByID(int id)
    {
        _genericRepo.DeleteEntity(id, GetOrderParameters, "CascadeDeleteOrder");
    }
    #endregion
}
