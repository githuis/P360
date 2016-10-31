using System;
using Ordersystem.Exceptions;
using Ordersystem.Model;

namespace Ordersystem.Utilities
{
    /// <summary>
    ///     Connection to the local database.
    /// </summary>
    public class LocalDatabase
    {
        /// <summary>
        ///     Gets the connection and creates the table.
        /// </summary>
        public LocalDatabase(string filename)
        {
            _database = DependencyService.Get<ISQLite>().GetConnection(filename);
            _database.CreateTable<SQLItem>();
        }

        private readonly SQLiteConnection _database;

        /* Predicate methods */

        /// <summary>
        ///     Gets the first Order from the database matching the predicate.
        /// </summary>
        /// <param name="predicate">The predicate used to fetch the Order.</param>
        /// <returns>The Order if found, else it returns null.</returns>
        public Order GetOrder(Func<SQLItem, bool> predicate)
        {
            return _database.Table<SQLItem>().FirstOrDefault(predicate).Order;
        }

        /// <summary>
        ///     Gets the first Orderlist from the database matching the predicate.
        /// </summary>
        /// <param name="predicate">The predicate used to fetch the Orderlsit.</param>
        /// <returns>The Orderlist if found, else returns null.</returns>
        public Orderlist GetOrderlist(Func<SQLItem, bool> predicate)
        {
            return _database.Table<SQLItem>().FirstOrDefault(predicate).Orderlist;
        }

        /// <summary>
        ///     Deletes the first entry in the database matching the predicate.
        ///     Throws an ItemNotFoundException if an entry is not found.
        /// </summary>
        /// <param name="predicate">The predicate used to delete the entry.</param>
        public void DeleteOrder(Func<SQLItem, bool> predicate)
        {
            SQLItem item = _database.Table<SQLItem>().FirstOrDefault(predicate);
            if (item == null)
                throw new ItemNotFoundException("item", "Saved session not found");
            _database.Delete<SQLItem>(item.ID);
        }

        /* Regular methods */

        /// <summary>
        ///     Gets the first Order from the database matching the personNumber.
        /// </summary>
        /// <param name="personNumber">The personNumber used to fetch the Order.</param>
        /// <returns>The Order if found, else returns null.</returns>
        public Order GetOrder(int personNumber)
        {
            return _database.Table<SQLItem>().FirstOrDefault(x => x.PersonNumber == personNumber).Order;
        }

        /// <summary>
        ///     Gets the first Orderlist from the database matching the personNumber.
        /// </summary>
        /// <param name="personNumber">The personNumber used to fetch the Orderlist.</param>
        /// <returns>The Orderlist if found, else returns null.</returns>
        public Orderlist GetOrderlist(int personNumber)
        {
            return _database.Table<SQLItem>().FirstOrDefault(x => x.PersonNumber == personNumber).Orderlist;
        }

        /// <summary>
        ///     Deletes the first entry in the database matching the personNumber.
        ///     Throws an ItemNotFoundException if an entry is not found.
        /// </summary>
        /// <param name="personNumber">The personNumber used to find the entry.</param>
        public void DeleteOrder(int personNumber)
        {
            SQLItem item = _database.Table<SQLItem>().FirstOrDefault(x => x.PersonNumber == personNumber);
            if (item == null)
                throw new ItemNotFoundException("item", "Saved session not found");
            _database.Delete<SQLItem>(item.ID);
        }

        /* Other methods */

        /// <summary>
        ///     Saves an order and relevant information to the database.
        /// </summary>
        /// <param name="order">The Order to be saved in the database.</param>
        /// <param name="orderlist">The Orderlist related to the Order.</param>
        /// <param name="customer">The Customer, whose personNumber is used as reference in the database.</param>
        public void SaveOrder(Orderlist orderlist, Customer customer)
        {
            _database.Insert(new SQLItem(customer.PersonNumber, customer.Order, orderlist));
        }

        /// <summary>
        ///     Clears the database.
        /// </summary>
        public void ClearDatabase()
        {
            _database.DeleteAll<SQLItem>();
        }
    }
}