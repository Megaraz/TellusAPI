using Microsoft.Data.SqlClient;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories;
/// <summary>
/// Generell Repo med generiska metoder som skall funka för varje Model/Table
/// </summary>
/// <typeparam name="T"></typeparam>
public class GenericRepository<T> where T : class, new()
{
    private readonly string _connectionString;

    public GenericRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

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



    public void Update(T entity, Action<SqlCommand, T> updateParameters, string procedureName)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        using SqlCommand command = new SqlCommand(procedureName, connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        updateParameters.Invoke(command, entity);
        command.ExecuteNonQuery();
    }


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


    ///// <summary>
    ///// Creates a command of a StoredProcedure type
    ///// </summary>
    ///// <param name="connection"></param>
    ///// <param name="procedureName"></param>
    ///// <returns>a SqlCommand instance</returns>
    //public static SqlCommand CreateCommandProcedure(SqlConnection connection, string procedureName)
    //{
    //    SqlCommand command = connection.CreateCommand();

    //    command.CommandType = CommandType.StoredProcedure;
    //    command.CommandText = procedureName;

    //    return command;
    //}
    ///// <summary>
    ///// Creates a command of a Text(Query) type
    ///// </summary>
    ///// <param name="connection"></param>
    ///// <param name="query"></param>
    ///// <returns>a SqlCommand instance</returns>
    //public static SqlCommand CreateCommandText(SqlConnection connection, string query)
    //{
    //    SqlCommand command = connection.CreateCommand();

    //    command.CommandType = CommandType.Text;
    //    command.CommandText = query;

    //    return command;
    //}
}
