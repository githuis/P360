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
        private const string ConnectionString = "server=188.166.27.155;port=22;uid=root;pwd=dankmeme;database=Mcs;";

        public string ReturnCustomersConnection()
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Customer";

                MySqlCommand Command = new MySqlCommand(query, connection);
                MySqlDataReader reader = Command.ExecuteReader();
                var result = reader.Read() ? reader.GetString(0) : "not found";

                connection.Close();

                return result;
            }
        }  
    }
}
