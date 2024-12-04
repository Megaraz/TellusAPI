using Microsoft.Data.SqlClient;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories;
/// <summary>
/// Generisk Repo som sköter huvudlogik(CRUD) för klasser oavsett vilken det är
/// </summary>
/// <typeparam name="T"></typeparam>
public class GenericRepository<T> where T : class, new()
{
    private readonly string _connectionString;

    public GenericRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
    /// <summary>
    /// Öppnar connection till Tellus DB och lägger till en ny post via Stored Procedure
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="getInParameters"></param>
    /// <param name="handleOutPut"></param>
    /// <param name="procedureName"></param>
    public void AddEntity(T entity, Action<SqlCommand, T> getInParameters, Action<SqlCommand, T> handleOutPut, string procedureName)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        using SqlCommand command = new SqlCommand(procedureName, connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        getInParameters.Invoke(command, entity);

        command.ExecuteNonQuery();

        handleOutPut.Invoke(command, entity);

    }

    /// <summary>
    /// Öppnar connection till Tellus DB och hämtar samtliga poster för angiven klass(entity)
    /// </summary>
    /// <param name="mapToEntity"></param>
    /// <param name="query"></param>
    /// <returns>en lista av angiven klass(entity)</returns>
    public List<T> GetEntities(Func<SqlDataReader, T> mapToEntity, string query)
    {
        List<T> entities = new List<T>();
        T? result = null;

        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        using SqlCommand command = new SqlCommand(query, connection)
        {
            CommandType = CommandType.Text
        };

        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            entities.Add(result = mapToEntity.Invoke(reader));
        }

        return entities;

    }


    /// <summary>
    /// Öppnar connection till Tellus DB och hämtar en specifik post utifrån angiven ID och Klass(Entity)
    /// </summary>
    /// <param name="id"></param>
    /// <param name="getEntityParameters"></param>
    /// <param name="mapToEntity"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public T? GetByID(int id, Action<SqlCommand, int> getEntityParameters, Func<SqlDataReader, T> mapToEntity, string query)
    {
        T? result = null;

        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();


        using SqlCommand command = new SqlCommand(query, connection)
        {
            CommandType = CommandType.Text,
        };

        getEntityParameters.Invoke(command, id);

        using SqlDataReader reader = command.ExecuteReader();

        if (reader.Read())
        {
            result = mapToEntity.Invoke(reader);
        }

        return result;

    }


    /// <summary>
    /// Öppnar connection till Tellus DB och uppdaterar en specifik post utifrån angiven Klass(entity) via Stored Procedure
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="updateParameters"></param>
    /// <param name="procedureName"></param>
    public void Update(T entity, Action<SqlCommand, T> updateParameters, Action<SqlCommand, T>? handleOutPut, string procedureName)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        using SqlCommand command = new SqlCommand(procedureName, connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        updateParameters.Invoke(command, entity);

        command.ExecuteNonQuery();

        handleOutPut?.Invoke(command, entity);

    }

    /// <summary>
    /// Öppnar connection till Tellus DB och deletar en specifik post utifrån angiven Klass(entity) och ID, via Stored Procedure.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="getEntityParameters"></param>
    /// <param name="procedureName"></param>
    public void DeleteEntity(int id, Action<SqlCommand, int> getEntityParameters, string procedureName)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        using SqlCommand command = new SqlCommand(procedureName, connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        getEntityParameters.Invoke(command, id);

        command.ExecuteNonQuery();
    }

}
