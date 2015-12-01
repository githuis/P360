using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;

namespace Ordersystem.Utilities
{
    public class McsSqlConnection
    {
        MySql.Data.MySqlClient.MySqlConnection conn;
        string myConnectionString = "server=188.166.27.155;uid=root;pwd=dankmeme;database=Mcs;";

        /*public McsSqlConnection()
        {
            conn = new MySql.Data.MySqlClient.MySqlConnection();
            conn.ConnectionString = myConnectionString;
            conn.Open();

            // I have no idea, sorry. Den påstår at Open kan kaste en System.Data.Common.DbException, men System.Data findes ikke i den nyeste version af .NET frameworket.
        }*/
    }
}
