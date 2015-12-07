using Ordersystem.Model;
using SQLite;

namespace Ordersystem.Utilities
{
    /// <summary>
    /// Concatination of the information in the database.
    /// Includes primary key property.
    /// </summary>
    public class Session
    {
        /// <summary>
        /// Empty constructor, needed for SQLite.
        /// </summary>
        public Session()
        {
            
        }

        /// <summary>
        /// Creates SQLItem with all relevant information.
        /// </summary>
        /// <param name="personNumber">The personNumber of the customer whom the order belongs to.</param>
        /// <param name="order">The Order in the database.</param>
        /// <param name="orderlist">The Orderlist relevant to the database.</param>
        public Session(string personNumber, Order order, Orderlist orderlist)
        {
            PersonNumber = personNumber;
            Order = order;
            Orderlist = orderlist;
        }

        /// <summary>
        /// The personNumber of the customer whom the order belongs to.
        /// </summary>
        public string PersonNumber { get; private set; }

        /// <summary>
        /// The Order in the database.
        /// </summary>
        [Ignore]
        public Order Order { get; private set; }

        /// <summary>
        /// The Orderlist relevant to the database.
        /// </summary>
        [Ignore]
        public Orderlist Orderlist { get; private set; }

        /// <summary>
        /// The primary key ID needed to navigate the database.
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
    }
}
