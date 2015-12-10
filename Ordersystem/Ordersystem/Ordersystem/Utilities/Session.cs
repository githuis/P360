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
        /// Empty constructor, should only be used by SQLite.
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
            SerializedOrder = _orderSerializer.Serialize(Order);
            Orderlist = orderlist;
            SerializedOrderlist = _orderlistSerializer.Serialize(Orderlist);
        }
        /// <summary>
        /// The primary key ID needed to navigate the database.
        /// </summary>
        [PrimaryKey]
        public int ID { get; set; }

        /// <summary>
        /// The personNumber of the customer whom the order belongs to.
        /// </summary>
        //[MaxLength(10)]
        public string PersonNumber { get; set; }

        /// <summary>
        /// The serialized Order in the database.
        /// </summary>
        //[MaxLength(9001)]
        //[Column(nameof(Order))]
        public string SerializedOrder { get; set; }

        /// <summary>
        /// The serialized Orderlist in the database.
        /// </summary>
        //[MaxLength(9001)]
        //[Column(nameof(Orderlist))]
        public string SerializedOrderlist { get; set; }

        /// <summary>
        /// The serialized Order in the database.
        /// </summary>
        [Ignore]
        public Order Order
        {
            get
            {
                if (Order == null) Order = _orderSerializer.Deserialize(SerializedOrder);
                return Order;
            }
            private set { Order = value; }
        }

        /// <summary>
        /// The serialized Orderlist in the database.
        /// </summary>
        [Ignore]
        public Orderlist Orderlist
        {
            get
            {
                if (Orderlist == null) Orderlist = _orderlistSerializer.Deserialize(SerializedOrderlist);
                return Orderlist;
            }
            private set { Orderlist = value; }
        }

        private static XmlStringSerializer<Order> _orderSerializer = new XmlStringSerializer<Order>();
        private static XmlStringSerializer<Orderlist> _orderlistSerializer = new XmlStringSerializer<Orderlist>();
    }
}
