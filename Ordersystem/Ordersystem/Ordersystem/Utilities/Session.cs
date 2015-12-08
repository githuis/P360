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
        /// Creates a storable Session with all relevant information.
        /// </summary>
        /// <param name="personNumber">The personNumber of the customer whom the order belongs to.</param>
        /// <param name="order">The Order in the database.</param>
        /// <param name="orderlist">The Orderlist relevant to the database.</param>
        public Session(string personNumber, Order order, Orderlist orderlist)
        {
            PersonNumber = personNumber;
            Order = order;
            _order = _orderSerializer.Serialize(Order);
            Orderlist = orderlist;
            _orderlist = _orderlistSerializer.Serialize(Orderlist);

        }

        /// <summary>
        /// The personNumber of the customer whom the order belongs to.
        /// </summary>
        [MaxLength(10)]
        public string PersonNumber { get; private set; }

        /// <summary>
        /// The serialized Order in the database.
        /// </summary>
        [Ignore]
        public Order Order {
            get { if (Order == null) { DeserializeOrder(); } return Order; }
            private set { Order = value; }
        }

        /// <summary>
        /// The serialized Orderlist in the database.
        /// </summary>
        [Ignore]
        public Orderlist Orderlist
        {
            get { if (Orderlist == null) { DeserializeOrderlist(); } return Orderlist; }
            private set { Orderlist = value; }
        }

        /// <summary>
        /// The primary key ID needed to navigate the database.
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        /// <summary>
        /// The serialized Order in the database.
        /// </summary>
        [MaxLength(9001)]
        private string _order { get; set; }

        /// <summary>
        /// The serialized Orderlist in the database.
        /// </summary>
        [MaxLength(9001)]
        private string _orderlist { get; set; }

        /// <summary>
        /// Returns the deserialized Order
        /// </summary>
        /// <returns>The deserialized Order</returns>
        private void DeserializeOrder()
        {
            Order = _orderSerializer.Deserialize(_order);
        }

        /// <summary>
        /// Returns the deserialized Orderlist
        /// </summary>
        /// <returns>The deserialized Order</returns>
        private void DeserializeOrderlist()
        {
            Orderlist = _orderlistSerializer.Deserialize(_orderlist);
        }

        private static XmlStringSerializer<Order> _orderSerializer = new XmlStringSerializer<Order>();
        private static XmlStringSerializer<Orderlist> _orderlistSerializer = new XmlStringSerializer<Orderlist>();
    }
}
