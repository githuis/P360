using System;
using System.Linq;
using System.Collections.Generic;
using Ordersystem.Exceptions;
using Xamarin.Forms;
using Ordersystem.Model;
using System.IO;

namespace Ordersystem.Utilities
{
    /// <summary>
    /// Connection to the local database.
    /// </summary>
    public class LocalDatabase
    {
        public string Filename { get; set; }
        private ILocalFile _database;
        private List<Session> _sessions;
        /// <summary>
        /// Gets the connection and creates the table.
        /// </summary>
        public LocalDatabase(string filename)
        {
            Open(filename);
        }

		public void CleanOldSessions()
		{
            foreach(var session in _sessions.ToList())
            {
                if (session.Orderlist.EndDate < DateTime.Today || session.Order.Sent)
                {
                    _sessions.Remove(session);
                }
            }
		}

        /* Predicate methods */
        /// <summary>
        /// Gets the first Order from the database matching the predicate.
        /// </summary>
        /// <param name="predicate">The predicate used to fetch the Order.</param>
        /// <returns>The Order if found, else it returns null.</returns>
        public Order GetOrder(Func<Session, bool> predicate)
        {
			var session = _sessions.FirstOrDefault(predicate);
			if (session != null)
				return session.Order;
			else 
				return null;
        }

        /// <summary>
        /// Gets the first Orderlist from the database matching the predicate.
        /// </summary>
        /// <param name="predicate">The predicate used to fetch the Orderlsit.</param>
        /// <returns>The Orderlist if found, else returns null.</returns>
        public Orderlist GetOrderlist(Func<Session, bool> predicate)
        {
			var session = _sessions.FirstOrDefault(predicate);
			if (session != null)
				return session.Orderlist;
			else 
				return null;
        }

        /// <summary>
        /// Deletes the first entry in the database matching the predicate.
        /// Throws an ItemNotFoundException if an entry is not found.
        /// </summary>
        /// <param name="predicate">The predicate used to delete the entry.</param>
        public void DeleteSession(Func<Session, bool> predicate)
        {
            Session session = _sessions.FirstOrDefault(predicate);
            if (session == null)
            {
                throw new ItemNotFoundException("item", "Saved session not found");
            }
            _sessions.Remove(session);
        }

        /* Regular methods */
        /// <summary>
        /// Gets the first Order from the database matching the personNumber.
        /// </summary>
        /// <param name="personNumber">The personNumber used to fetch the Order.</param>
        /// <returns>The Order if found, else returns null.</returns>
        public Order GetOrder(string personNumber)
        {
			var session = _sessions.FirstOrDefault(x => x.PersonNumber == personNumber);
			if (session != null)
				return session.Order;
			else
				return null;
        }

        /// <summary>
        /// Gets the first Orderlist from the database matching the personNumber.
        /// </summary>
        /// <param name="personNumber">The personNumber used to fetch the Orderlist.</param>
        /// <returns>The Orderlist if found, else returns null.</returns>
        public Orderlist GetOrderlist(string personNumber)
        {
			var session = _sessions.FirstOrDefault(x => x.PersonNumber == personNumber);
			if (session != null)
				return session.Orderlist;
			else
				return null;
        }

        /// <summary>
        /// Deletes the first session in the database matching the personNumber.
        /// Throws an ItemNotFoundException if an entry is not found.
        /// </summary>
        /// <param name="personNumber">The personNumber used to find the entry.</param>
        public void DeleteSession(string personNumber)
        {
            Session session = _sessions.FirstOrDefault(x => x.PersonNumber == personNumber);
            if (session == null)
            {
                throw new ItemNotFoundException("item", "Saved session not found");
            }
            _sessions.Remove(session);
        }

        /* Other methods */
        /// <summary>
        /// Saves a session and relevant information to the database.
        /// </summary>
        /// <param name="orderlist">The Orderlist related to the Order.</param>
        /// <param name="customer">The Customer, whose personNumber is used as reference in the database.</param>
        public void SaveSession(Orderlist orderlist, Customer customer)
        {
			try
			{
				DeleteSession(customer.PersonNumber);
			}
			catch (ItemNotFoundException)
			{
			}
			_sessions.Add(new Session(customer.PersonNumber, customer.Order, orderlist));
        }

        /// <summary>
        /// Clears the database.
        /// </summary>
        public void ClearDatabase()
        {
            _sessions.Clear();
        }

        /// <summary>
        /// Opens the database with the given filename.
        /// Is called automatically at database creation.
        /// </summary>
        /// <param name="filename">The name of the file.</param>
        public void Open(string filename)
        {
            Filename = filename;
            _database = DependencyService.Get<ILocalFile>();
            _database.UseFilePath(Filename);
            List<string> serializedSessions = _database.ReadFile();
            _sessions = serializedSessions.Select(client => Session.Serializer.Deserialize(client)).ToList();
        }

        /// <summary>
        /// Saves and overwrites the currently opened database file.
        /// </summary>
        public void Close()
        {
            List<string> serializedSessions = _sessions.Select(client => Session.Serializer.Serialize(client)).ToList();
            _database.WriteSeveralLinesToFile(serializedSessions, false);
        }
    }
}
