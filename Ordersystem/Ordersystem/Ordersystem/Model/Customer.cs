﻿using Ordersystem.Enums;

namespace Ordersystem.Model
{
    public class Customer
    {
        /// <summary>
        /// A customer of Aalborg Madservice.
        /// </summary>
        /// <param name="personNumber">The social security number of the customer.</param>
        /// <param name="name">The name of the customer.</param>
        /// <param name="diet">The diet, the customer is getting.</param>
        public Customer(string personNumber, string name, Diet diet)
        {
            PersonNumber = personNumber;
            Name = name;
            Order = new Order();
            Diet = diet;
        }
        
        /// <summary>
        /// The social security number of the customer.
        /// </summary>
        public string PersonNumber { get; private set; }

        /// <summary>
        /// The name of the customer.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The order of the customer for the next month.
        /// </summary>
        public Order Order { get; set; }

        /// <summary>
        /// The diet, the customer is getting.
        /// </summary>
        public Diet Diet { get; private set; }
    }
}
