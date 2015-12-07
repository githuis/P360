using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Ordersystem.Utilities
{
    public class McsSqlConnection
    {
		private const string ConnectionString = "server=eu-cdbr-azure-north-d.cloudapp.net;port=3306;user id=ba3af1f8d328b9;pwd=650e758f;database=P360;allowuservariables=True;";

        public string ReturnCustomersConnection()
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "SELECT * FROM kunder";

                MySqlCommand Command = new MySqlCommand(query, connection);
                MySqlDataReader reader = Command.ExecuteReader();
                var result = reader.Read() ? reader.GetString(0) : "not found";

                connection.Close();

                return result;
            }
        }  
    }
}
