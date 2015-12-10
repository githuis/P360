using System;
using Ordersystem.Model;

namespace Ordersystem
{
	public interface IMCSManager
	{
		void SendOrder (Order order, string personNumber);

		Customer GetCustomerByPersonNumber (string personNumber);

		Orderlist GetOrderlistByDiet (string diet);
	}
}

