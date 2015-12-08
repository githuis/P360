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

				string query = "SELECT * FROM customers WHERE customers.PersonNumber = " + personNumber;

                MySqlCommand Command = new MySqlCommand(query, connection);
                MySqlDataReader reader = Command.ExecuteReader();

				Customer customer;

				if(reader.HasRows)
				{
					reader.Read();

					if(!reader.IsDBNull(reader.GetOrdinal("Name")) && !reader.IsDBNull(reader.GetOrdinal("Diet")))
					{
						customer = new Customer (reader.GetInt32(reader.GetOrdinal("PersonNumber")).ToString("D10"), 
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

				string query = "SELECT * FROM orderlists_daymenus oldm " +
				               "JOIN orderlists ol ON ol.OrderlistKey=oldm.orderlistKey " +
				               "JOIN daymenus dm ON dm.DayMenuKey=oldm.DayMenuKey " +
				               "JOIN dishes d1 ON d1.DishKey=dm.Dish1 " +
				               "JOIN dishes d2 ON d2.DishKey=dm.Dish2 " +
				               "JOIN dishes sd ON sd.DishKey=dm.SideDish " +
				               "WHERE ol.Diet = '" + diet + "'";

				MySqlCommand Command = new MySqlCommand(query, connection);
				MySqlDataReader reader = Command.ExecuteReader();

				Orderlist orderlist;
				DateTime StartDate, EndDate;
				string Diet;
				int count;
				List<DayMenu> DayMenus = new List<DayMenu>();

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

					DayMenus.Add (ReadDayMenu (reader));
					for (int i = 2; i <= count; i++)
					{
						reader.Read ();
						DayMenus.Add (ReadDayMenu (reader));
					}

				}
				else
				{
					throw new ArgumentException ("No Ordelist for the given Diet found.");
				}

				connection.Close();
				orderlist = new Orderlist (DayMenus, StartDate, EndDate, Diet);
				return orderlist;
			}
		}

		private DayMenu ReadDayMenu(MySqlDataReader reader)
		{
			List<int> KeyOrdinals = new List<int>();
			KeyOrdinals.Add (reader.GetOrdinal ("SideDish") + 1);
			KeyOrdinals.Add (reader.GetOrdinal ("SideDish") + 5);
			KeyOrdinals.Add (reader.GetOrdinal ("SideDish") + 9);

			List<Dish> Dishes = new List<Dish>();
			foreach (int ordinal in KeyOrdinals)
			{
				if (!reader.IsDBNull (ordinal + 1) && !reader.IsDBNull (ordinal + 2))
				{
					Dishes.Add (new Dish (reader.GetString (ordinal + 1),
						reader.GetString (ordinal + 2),
						reader.IsDBNull (ordinal + 3) ? "" : reader.GetString (ordinal + 3)));
				}
				else
				{
					throw new NullReferenceException ("Database contains illegal null values.");
				}
			}

			DateTime Date;
			if (!reader.IsDBNull (reader.GetOrdinal ("Date")))
			{
				Date = reader.GetDateTime (reader.GetOrdinal ("Date"));
			}
			else
			{
				throw new NullReferenceException ("Database contains illegal null values.");
			}

			return new DayMenu (Dishes [0], Dishes [1], Dishes [2], Date);
		}
    }
}
