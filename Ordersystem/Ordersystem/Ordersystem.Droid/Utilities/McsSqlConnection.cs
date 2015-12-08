using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Ordersystem.Model;

namespace Ordersystem.Utilities
{
    public class McsSqlConnection
    {
		private const string ConnectionString = "server=eu-cdbr-azure-north-d.cloudapp.net;port=3306;user id=ba3af1f8d328b9;pwd=650e758f;database=P360;allowuservariables=True;";

		public Customer GetCustomerByPersonNumber(string personNumber)
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

				string query = "SELECT TOP 1 * FROM customers WHERE PersonNumber = " + personNumber;

                MySqlCommand Command = new MySqlCommand(query, connection);
                MySqlDataReader reader = Command.ExecuteReader();

				Customer customer;

				if(reader.HasRows)
				{
					reader.Read();

					if(!reader.IsDBNull(reader.GetOrdinal("Name")) && !reader.IsDBNull(reader.GetOrdinal("Diet")))
					{
						customer = new Customer (reader.GetString(reader.GetOrdinal("PersonNumber")), 
												 reader.GetString(reader.GetOrdinal("Name")), 
												 reader.GetString(reader.GetOrdinal("Diet")));
					}
					else
					{
						throw new NullReferenceException("Database contains null value.");
					}

					connection.Close();

					return customer;
				}
				else
				{
					throw new ArgumentException ("No customer with that personnumber exists.");
				}
            }
        }

		public Orderlist GetOrderlistByDiet(string diet)
		{
			using (MySqlConnection connection = new MySqlConnection(ConnectionString))
			{
				connection.Open();

				string query = "SELECT * FROM ordelists_daymenus oldm " +
				               "JOIN ordelists ol ON ol.OrderlistKey=oldm.orderlistKey " +
				               "JOIN daymenus dm " +
				               "(JOIN dishes d1 ON dm.Dish1=d1.DishKey" +
				               " JOIN dishes d2 ON dm.Dish2=d2.DishKey" +
				               " JOIN dishes sd ON dm.SideDish=sd.DishKey) " +
				               "ON dm.DayMenuKey=oldm.daymenuKey " +
				               "WHERE orderlistsDiet = " + diet;

				MySqlCommand Command = new MySqlCommand(query, connection);
				MySqlDataReader reader = Command.ExecuteReader();

				Orderlist Orderlist;
				DateTime StartDate, EndDate;
				string Diet;
				int count;
				List<DayMenu> DayMenus = new List<DayMenu>();
				List<int> DayMenuKeys;

				if (reader.HasRows)
				{
					reader.Read ();

					if (!reader.IsDBNull (reader.GetOrdinal ("StartDate")) &&
					    !reader.IsDBNull (reader.GetOrdinal ("EndDate")) &&
					    !reader.IsDBNull (reader.GetOrdinal ("Diet")) &&
					    !reader.IsDBNull (reader.GetOrdinal ("Days")))
					{
						count = reader.GetInt32 (reader.GetOrdinal ("Days"));
						Diet = reader.GetString (reader.GetOrdinal ("Diet"));
						StartDate = reader.GetDateTime (reader.GetOrdinal ("StartDate"));
						EndDate = reader.GetDateTime (reader.GetOrdinal ("EndDate"));
					}
					else
					{
						throw new NullReferenceException ("Database contains null values.");
					}

					DayMenuKeys = GetDayMenuKeys (reader, count);
				}
				else
				{
					throw new ArgumentException ("No Ordelist for the given Diet found.");
				}

				DayMenus = GetDayMenusFromKeys (DayMenuKeys);

				connection.Close();

				return customer;
			}
		}
    }
}
