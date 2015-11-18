using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ordersystem.Model;

namespace Ordersystem.Functions
{
    class OrderManager
    {
        private Order order;
        private Orderlist orderList;

        public OrderManager(Order order, Orderlist list)
        {
            this.order = order;
            orderList = list;
        }

        public void SelectDish(Dish dish)
        {
            if (!order.Servings.Contains(new Serving(dish)))
                order.AddServing(dish);
        }

        public void RemoveDish(Dish dish)
        {
            order.RemoveServing(dish);
        }
    }
}