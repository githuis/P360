using Ordersystem.Model;
using System.Xml.Serialization;

namespace Ordersystem.Utilities
{
    /// <summary>
    /// Concatination of the information in the database.
    /// Includes primary key property.
    /// </summary>
    [XmlRoot]
    public class Session
    {
        /// <summary>
        /// Empty constructor, should only be used by SQLite.
        /// </summary>
        public Session() : base()
        {
            
        }

        /// <summary>
        /// Creates a storable Session with all relevant information.
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
        //[MaxLength(10)]
        public string PersonNumber { get; set; }

        /// <summary>
        /// The serialized Order in the database.
        /// </summary>
        public Order Order { get; set; }

        /// <summary>
        /// The serialized Orderlist in the database.
        /// </summary>
        public Orderlist Orderlist { get; set; }

        public static XmlStringSerializer<Session> Serializer = new XmlStringSerializer<Session>();
    }
}
