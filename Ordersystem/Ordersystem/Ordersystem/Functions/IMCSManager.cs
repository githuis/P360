﻿using System;
using Ordersystem.Model;
using Ordersystem.Enums;

namespace Ordersystem.Functions
{
	public interface IMCSManager
	{
		/// <summary>
		/// Sends an order to the database.
		/// </summary>
		/// <param name="order">The order to be send.</param>
		/// <param name="personNumber">The personnumber of the customer sending the order.</param>
		void SendOrder (Order order, string personNumber);

		/// <summary>
		/// Gets a customer from the database by a personnumber.
		/// </summary>
		/// <returns>The customer.</returns>
		/// <param name="personNumber">The personnumber of the customer.</param>
		/// <exception cref="Ordersystem.Exceptions.InvalidCustomerException">Throws this exception when customer is invalid.</exception>
		/// <exception cref="Ordersystem.Exceptions.CustomerNotFoundException">Throws this exceoption when no customer was found.</exception>
		Customer GetCustomerByPersonNumber (string personNumber);

		/// <summary>
		/// Gets the next orderlist from the database based on a diet and end date.
		/// </summary>
		/// <returns>The orderlist.</returns>
		/// <param name="diet">The diet of the orderlist.</param>
		/// <param name="endDate">The end date og the orderlist.</param>
		/// <exception cref="Ordersystem.Exceptions.InvalidOrderlistException">Throws this exception when orderlist is invalid.</exception>
		/// <exception cref="Ordersystem.Exceptions.OrderlistNotFoundException">Thows this exception when no orderlist is found.</exception>
		Orderlist GetOrderlistByDiet (Diet diet, DateTime endDate);

		/// <summary>
		/// Gets the next orderlist from the database based on the diet.
		/// </summary>
		/// <returns>The orderlist.</returns>
		/// <param name="diet">The diet of the orderlist.</param>
		/// <exception cref="Ordersystem.Exceptions.InvalidOrderlistException">Throws this exception when orderlist is invalid.</exception>
		/// <exception cref="Ordersystem.Exceptions.OrderlistNotFoundException">Thows this exception when no orderlist is found.</exception>
		Orderlist GetOrderlistByDiet (Diet diet);
	}
}

