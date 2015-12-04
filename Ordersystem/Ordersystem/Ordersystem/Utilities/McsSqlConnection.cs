using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Ordersystem.Utilities
{
    public class McsSqlConnection
    {
        private SqlConnection _connection;

        public string ReturnCustomersConnection()
        {
            using (_connection = new SqlConnection("server=188.166.27.155;uid=root;pwd=dankmeme;database=Mcs;"))
            {
                var command = new SqlCommand();
                SqlDataReader reader;

                command.CommandText = "SELECT * FROM Customers";
                command.CommandType = CommandType.Text;
                command.Connection = _connection;

                _connection.Open();

                reader = command.ExecuteReader();

                _connection.Close();

                return "potato";
            }
        }
        
    }
}
