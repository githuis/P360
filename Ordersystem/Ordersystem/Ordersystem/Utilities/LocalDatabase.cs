using System;
using System.Linq;
using Ordersystem.Exceptions;
using Xamarin.Forms;
using SQLite;
using Ordersystem.Model;

namespace Ordersystem.Utilities
{
    /// <summary>
    /// Connection to the local database.
    /// </summary>
    public class LocalDatabase
    {
        private SQLiteConnection _database;

        /// <summary>
        /// Gets the connection and creates the table.
        /// </summary>
        public LocalDatabase(string filename)
        {
            _database = DependencyService.Get<ISQLite>().GetConnection(filename);
            _database.CreateTable<Session>();
        }

        /* Predicate methods */
        /// <summary>
        /// Gets the first Order from the database matching the predicate.
        /// </summary>
        /// <param name="predicate">The predicate used to fetch the Order.</param>
        /// <returns>The Order if found, else it returns null.</returns>
        public Order GetOrder(Func<Session, bool> predicate)
        {
            return _database.Table<Session>().FirstOrDefault(predicate).Order;
        }

        /// <summary>
        /// Gets the first Orderlist from the database matching the predicate.
        /// </summary>
        /// <param name="predicate">The predicate used to fetch the Orderlsit.</param>
        /// <returns>The Orderlist if found, else returns null.</returns>
        public Orderlist GetOrderlist(Func<Session, bool> predicate)
        {
            return _database.Table<Session>().FirstOrDefault(predicate).Orderlist;
        }

        /// <summary>
        /// Deletes the first entry in the database matching the predicate.
        /// Throws an ItemNotFoundException if an entry is not found.
        /// </summary>
        /// <param name="predicate">The predicate used to delete the entry.</param>
        public void DeleteSession(Func<Session, bool> predicate)
        {
            Session session = _database.Table<Session>().FirstOrDefault(predicate);
            if (session == null)
            {
                throw new ItemNotFoundException("item", "Saved session not found");
            }
            _database.Delete<Session>(session.ID);
        }

        /* Regular methods */
        /// <summary>
        /// Gets the first Order from the database matching the personNumber.
        /// </summary>
        /// <param name="personNumber">The personNumber used to fetch the Order.</param>
        /// <returns>The Order if found, else returns null.</returns>
        public Order GetOrder(string personNumber)
        {
            return _database.Table<Session>().FirstOrDefault(x => x.PersonNumber == personNumber).Order;
        }

        /// <summary>
        /// Gets the first Orderlist from the database matching the personNumber.
        /// </summary>
        /// <param name="personNumber">The personNumber used to fetch the Orderlist.</param>
        /// <returns>The Orderlist if found, else returns null.</returns>
        public Orderlist GetOrderlist(string personNumber)
        {
            return _database.Table<Session>().FirstOrDefault(x => x.PersonNumber == personNumber).Orderlist;
        }

        /// <summary>
        /// Deletes the first session in the database matching the personNumber.
        /// Throws an ItemNotFoundException if an entry is not found.
        /// </summary>
        /// <param name="personNumber">The personNumber used to find the entry.</param>
        public void DeleteSession(string personNumber)
        {
            Session session = _database.Table<Session>().FirstOrDefault(x => x.PersonNumber == personNumber);
            if (session == null)
            {
                throw new ItemNotFoundException("item", "Saved session not found");
            }
            _database.Delete<Session>(session.ID);
        }

        /* Other methods */
        /// <summary>
        /// Saves a session and relevant information to the database.
        /// </summary>
        /// <param name="orderlist">The Orderlist related to the Order.</param>
        /// <param name="customer">The Customer, whose personNumber is used as reference in the database.</param>
        public void SaveSession(Orderlist orderlist, Customer customer)
        {
            _database.Insert(new Session(customer.PersonNumber, customer.Order, orderlist));
        }

        /// <summary>
        /// Clears the database.
        /// </summary>
        public void ClearDatabase()
        {
            _database.DeleteAll<Session>();
        }
    }
}