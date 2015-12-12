﻿using System;
using Ordersystem.Model;
using Ordersystem.Enums;

namespace Ordersystem.Functions
{
	public interface IMCSManager
	{
		void SendOrder (Order order, string personNumber);

		Customer GetCustomerByPersonNumber (string personNumber);

		Orderlist GetOrderlistByDiet (Diet diet, Nullable<DateTime> endDate = null);
	}
}

