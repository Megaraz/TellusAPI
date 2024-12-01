using Microsoft.Data.SqlClient;
using System.Data;
using System.IO.Pipes;
using TellusAPI;

namespace Models
{
    public class Tellus
    {
        private readonly string _connectionString;

        public Tellus(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddKund(Kund kund)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = connection.CreateCommand();

            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "AddKund";

            command.Parameters.Add("@Personnr", SqlDbType.NVarChar, 13).Value = kund.Personnr;
            command.Parameters.Add("@Förnamn", SqlDbType.NVarChar, 32).Value = kund.Förnamn;
            command.Parameters.Add("@Efternamn", SqlDbType.NVarChar, 32).Value = kund.Efternamn;

            command.Parameters.Add("@ID", SqlDbType.Int).Direction = ParameterDirection.Output;
            connection.Open();
            command.ExecuteNonQuery();

            kund.ID = (int)command.Parameters["@ID"].Value;

        }
    }


}
