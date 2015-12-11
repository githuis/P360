using System;
using Ordersystem.Model;

namespace Ordersystem.Functions
{
	public interface IMCSManager
	{
		void SendOrder (Order order, string personNumber);

		Customer GetCustomerByPersonNumber (string personNumber);

		Orderlist GetOrderlistByDiet (Ordersystem.Enums.Diet diet);
	}
}

