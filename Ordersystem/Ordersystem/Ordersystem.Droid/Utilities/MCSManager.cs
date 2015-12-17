using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Ordersystem.Model;
using Ordersystem.Enums;
using Ordersystem.Droid.Utilities;
using Ordersystem.Functions;
using BCrypt.Net;

[assembly: Xamarin.Forms.Dependency(typeof(MCSManager))]
namespace Ordersystem.Droid.Utilities
{

    public class MCSManager : IMCSManager
    {
		private readonly string salt = "$2a$10$4/VAEZ3aRcan1gYAuxQ1me";
		private const string ConnectionString = "server=eu-cdbr-azure-north-d.cloudapp.net;port=3306;user id=ba3af1f8d328b9;pwd=650e758f;database=P360;allowuservariables=True;";

		/// <summary>
		/// Sends an order to the database.
		/// </summary>
		/// <param name="order">The order to be send.</param>
		/// <param name="personNumber">The personnumber of the customer sending the order.</param>
		public void SendOrder(Order order, string personNumber)
		{
			if (order.Sent)
				return;
			//Initalize the connection.
			using (MySqlConnection connection = new MySqlConnection (ConnectionString))
			{
				var pass = BCrypt.Net.BCrypt.HashPassword (personNumber, salt);
				var intPass = pass.GetHashCode ();

				connection.Open ();

				string Query = "INSERT INTO orders (CustomerPersonNumber)" +
				                    "VALUES (" + intPass + ")";

				//Execute query on database.
				MySqlCommand command = new MySqlCommand (Query, connection);
				command.ExecuteNonQuery ();

				List<Tuple<Dish,Dish>> dishes = GetDishesFromOrder (order);

				//Execute queries on database for each choice.
				foreach (Tuple<Dish,Dish> dishTuple in dishes)
				{
					if (dishTuple.Item2 != null) {
						Query = "INSERT INTO orderdays (DishKey, SideDishKey, OrderKey) " +
						"SELECT d1.DishKey, d2.DishKey, o.OrderKey " +
						"FROM dishes d1 " +
						"JOIN dishes d2 " +
						"JOIN orders o " +
						"WHERE d1.Name = '" + dishTuple.Item1.Name + "' " +
						"AND d1.Description = '" + dishTuple.Item1.Description + "' " +
						"AND d2.Name = '" + dishTuple.Item2.Name + "' " +
						"AND d2.Description = '" + dishTuple.Item2.Description + "' " +
						"AND o.CustomerPersonNumber = " + personNumber;
					} else {
						Query ="INSERT INTO orderdays (DishKey, OrderKey) " +
						"SELECT d1.DishKey, o.OrderKey " +
						"FROM dishes d1 " +
						"JOIN orders o " +
						"WHERE d1.Name = '" + dishTuple.Item1.Name + "' " +
						"AND d1.Description = '" + dishTuple.Item1.Description + "' " +
						"AND o.CustomerPersonNumber = " + personNumber;
					}
					command.CommandText = Query;
					command.ExecuteNonQuery ();
					order.Sent = true;
				}
					
				connection.Close ();
			}
		}

		/// <summary>
		/// Gets a customer from the database by a personnumber.
		/// </summary>
		/// <returns>The customer.</returns>
		/// <param name="personNumber">The personnumber of the customer.</param>
		/// <exception cref="Ordersystem.Exceptions.InvalidCustomerException">Throws this exception when customer is invalid.</exception>
		/// <exception cref="Ordersystem.Exceptions.CustomerNotFoundException">Throws this exceoption when no customer was found.</exception>
		public Customer GetCustomerByPersonNumber(string personNumber)
        {
			//Initialize connection to database.
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
				var pass = BCrypt.Net.BCrypt.HashPassword (personNumber, salt);
				var intPass = pass.GetHashCode ();

                connection.Open();

				string query = "SELECT * FROM customers WHERE customers.PersonNumber = " + intPass;

				//Execute query on database.
                MySqlCommand Command = new MySqlCommand(query, connection);
                MySqlDataReader reader = Command.ExecuteReader();

				//Parse output from database to usable types.
				Customer customer;
				if(reader.HasRows)
				{
					reader.Read();

					if(!reader.IsDBNull(reader.GetOrdinal("Name")) && !reader.IsDBNull(reader.GetOrdinal("Diet")))
					{
						customer = new Customer (personNumber, 
												 reader.GetString(reader.GetOrdinal("Name")), 
												 ParseDietFromString(reader.GetString(reader.GetOrdinal("Diet"))));
					}
					else
					{
						connection.Close ();
						throw new InvalidCustomerException ("Database contains null value.");
					}
						
					connection.Close ();
					return customer;
				}
				else
				{
					connection.Close ();
					throw new CustomerNotFoundException("No customer with that personnumber exists.");
				}
            }
        }

		/// <summary>
		/// Gets the next orderlist from the database based on a diet and end date.
		/// </summary>
		/// <returns>The orderlist.</returns>
		/// <param name="diet">The diet of the orderlist.</param>
		/// <param name="endDate">The end date og the orderlist.</param>
		/// <exception cref="Ordersystem.Exceptions.InvalidOrderlistException">Throws this exception when orderlist is invalid.</exception>
		/// <exception cref="Ordersystem.Exceptions.OrderlistNotFoundException">Thows this exception when no orderlist is found.</exception> 
		public Orderlist GetOrderlistByDiet(Diet diet, DateTime endDate)
		{
			string dietString = ParseStringFromDiet (diet);

			//Initialize connection to database.
			using (MySqlConnection connection = new MySqlConnection(ConnectionString))
			{
				connection.Open();

				string query = "SELECT * FROM orderlists_daymenus oldm " +
					"JOIN orderlists ol ON ol.OrderlistKey=oldm.orderlistKey " +
					"JOIN daymenus dm ON dm.DayMenuKey=oldm.DayMenuKey " +
					"JOIN dishes d1 ON d1.DishKey=dm.Dish1 " +
					"JOIN dishes d2 ON d2.DishKey=dm.Dish2 " +
					"JOIN dishes sd ON sd.DishKey=dm.SideDish " +
					"WHERE ol.Diet = '" + dietString + "' " +
					"AND ol.EndDate = '" + endDate.ToString ("yyyy-MM-dd") + "'";

				//Execute query on database.
				MySqlCommand Command = new MySqlCommand(query, connection);
				MySqlDataReader reader = Command.ExecuteReader();

				Orderlist orderlist;
				DateTime StartDate, EndDate;
				string Diet;
				int count;
				List<DayMenu> DayMenus = new List<DayMenu>();

				//Parse output from database to usable types.
				if (reader.HasRows)
				{
					reader.Read ();

					if (!reader.IsDBNull (reader.GetOrdinal ("StartDate")) &&
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
						connection.Close ();
						throw new InvalidOrderlistException ("Database contains null values.");
					}

					//Errorcheck.
					if (count < 28 || count > 31)
					{
						connection.Close ();
						throw new InvalidOrderlistException ("Invalid amount of days in orderlist.");
					}

					//Read all daymenus from database output and make sure to close connection if there is an error.
					try
					{
						DayMenus.Add (ReadDayMenu (reader));
						for (int i = 2; i <= count; i++)
						{
							reader.Read ();
							DayMenus.Add (ReadDayMenu (reader));
						}
					}
					catch(NullReferenceException e)
					{
						connection.Close ();
						throw;
					}

					//Errorcheck.
					if (IllegalDates (DayMenus))
					{
						connection.Close ();
						throw new InvalidOrderlistException ("Duplicate dates detected.");
					}
				}
				else
				{
					connection.Close ();
					throw new OrderlistNotFoundException ("No Ordelist for the given Diet found.");
				}

				connection.Close();
				return new Orderlist (DayMenus, StartDate, EndDate, ParseDietFromString(Diet));
			}
		}

		/// <summary>
		/// Gets the next orderlist from the database based on the diet.
		/// </summary>
		/// <returns>The orderlist.</returns>
		/// <param name="diet">The diet of the orderlist.</param>
		/// <exception cref="Ordersystem.Exceptions.InvalidOrderlistException">Throws this exception when orderlist is invalid.</exception>
		/// <exception cref="Ordersystem.Exceptions.OrderlistNotFoundException">Thows this exception when no orderlist is found.</exception> 
		public Orderlist GetOrderlistByDiet(Diet diet)
		{
			string dietString = ParseStringFromDiet (diet);

			//Initialize database connection.
			using (MySqlConnection connection = new MySqlConnection(ConnectionString))
			{
				connection.Open();

				string query = "SELECT * FROM orderlists_daymenus oldm " +
				               "JOIN orderlists ol ON ol.OrderlistKey=oldm.orderlistKey " +
				               "JOIN daymenus dm ON dm.DayMenuKey=oldm.DayMenuKey " +
				               "JOIN dishes d1 ON d1.DishKey=dm.Dish1 " +
				               "JOIN dishes d2 ON d2.DishKey=dm.Dish2 " +
				               "JOIN dishes sd ON sd.DishKey=dm.SideDish " +
				               "WHERE ol.Diet = '" + dietString + "' " +
				               "AND ol.EndDate > '" + DateTime.Today.ToString ("yyyy-MM-dd") + "' " +
				               "AND ol.EndDate < '" + DateTime.Today.AddMonths (1).ToString ("yyyy-MM-dd") + "'";

				//Execute command on database.
				MySqlCommand Command = new MySqlCommand(query, connection);
				MySqlDataReader reader = Command.ExecuteReader();

				Orderlist orderlist;
				DateTime StartDate, EndDate;
				string Diet;
				int count;
				List<DayMenu> DayMenus = new List<DayMenu>();

				//Parse output to usable types.
				if (reader.HasRows)
				{
					reader.Read ();

					if (!reader.IsDBNull (reader.GetOrdinal ("StartDate")) &&
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
						connection.Close ();
						throw new InvalidOrderlistException ("Database contains null values.");
					}

					//Errorcheck
					if (count < 28 || count > 31)
					{
						connection.Close ();
						throw new InvalidOrderlistException ("Invalid amount of days in orderlist.");
					}

					//Read all daymenus from database output and make sure to close connection if error is found.
					try
					{
						DayMenus.Add (ReadDayMenu (reader));
						for (int i = 2; i <= count; i++)
						{
							reader.Read ();
							DayMenus.Add (ReadDayMenu (reader));
						}
					}
					catch(InvalidOrderlistException e)
					{
						connection.Close ();
						throw;
					}

					//Errorcheck
					if (IllegalDates (DayMenus))
					{
						connection.Close ();
						throw new InvalidOrderlistException ("Duplicate dates detected.");
					}
				}
				else
				{
					connection.Close ();
					throw new OrderlistNotFoundException ("No Ordelist for the given Diet found.");
				}

				connection.Close();
				return new Orderlist (DayMenus, StartDate, EndDate, ParseDietFromString(Diet));
			}
		}

		//Reads a daymenu from from the database reader.
		private DayMenu ReadDayMenu(MySqlDataReader reader)
		{
			//Find column indexes, ordinals, for daymenu keys.
			List<int> KeyOrdinals = new List<int>();
			KeyOrdinals.Add (reader.GetOrdinal ("SideDish") + 1);
			KeyOrdinals.Add (reader.GetOrdinal ("SideDish") + 5);
			KeyOrdinals.Add (reader.GetOrdinal ("SideDish") + 9);

			//Find dishes from these ordinals.
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
					throw new InvalidOrderlistException ("Database contains illegal null values.");
				}
			}

			//Find the date.
			DateTime Date;
			if (!reader.IsDBNull (reader.GetOrdinal ("Date")))
			{
				Date = reader.GetDateTime (reader.GetOrdinal ("Date"));
			}
			else
			{
				throw new InvalidOrderlistException ("Database contains illegal null values.");
			}

			return new DayMenu (Dishes [0], Dishes [1], Dishes [2], Date);
		}

		//Check if there are duplicate dates in the daymenus.
		private bool IllegalDates(List<DayMenu> dayMenus)
		{
			List<DayMenu> dayMenusClone = dayMenus.ToList ();

			foreach (var day in dayMenus)
			{
				foreach (var dayClone in dayMenusClone)
				{
					if (day.Date == dayClone.Date
						&& day.Dish1 != dayClone.Dish1
						&& day.Dish2 != dayClone.Dish2
						&& day.SideDish != dayClone.SideDish)
					{
						return true;
					}
				}
			}

			return false;
		}

		//Fetches the dishes chosen from the current order.
		private List<Tuple<Dish,Dish>> GetDishesFromOrder(Order order)
		{
			var dishes = new List<Tuple<Dish,Dish>> ();

			foreach (DayMenuSelection selection in order.DayMenuSelections)
			{
				var selectionDishes = selection.GetDishes ();

				if (selection.Choice == DayMenuChoice.Dish1)
				{
					dishes.Add (new Tuple<Dish,Dish> (selectionDishes [0], selection.SideDish ? selectionDishes [2] : null));
				}
				else if(selection.Choice == DayMenuChoice.Dish2)
				{
					dishes.Add (new Tuple<Dish,Dish> (selectionDishes [1], selection.SideDish ? selectionDishes [2] : null));
				}
			}

			return dishes;
		}

		private Diet ParseDietFromString(string diet)
		{
			switch(diet)
			{
			case "v-full":
				return Diet.Full;
			case "v-lowFat":
				return Diet.LowFat;
			case "v-energyDense":
				return Diet.EnergyDense;
			case "v-softFoodsWPotatoes":
				return Diet.SoftFoodsWPotatoes;
			case "v-softFoodsWMash":
				return Diet.SoftFoodsWMash;
			default:
				throw new ArgumentException ("Invalid diet type");
			}
		}

		private string ParseStringFromDiet(Diet diet)
		{
			switch(diet)
			{
			case Diet.Full:
				return "v-full";
			case Diet.LowFat:
				return"v-lowFat" ;
			case Diet.EnergyDense:
				return "v-energyDense";
			case Diet.SoftFoodsWPotatoes:
				return "v-softFoodsWPotatoes";
			case Diet.SoftFoodsWMash:
				return "v-softFoodsWMash";
			default:
				throw new ArgumentException ("Invalid diet type");
			}
		}
    }
}
