using System.Collections.Generic;
using System.Linq;

namespace Ordersystem.Model
{
    public class Order
    {
        public Order()
        {
            Servings = new List<Serving>();
        }

        public List<Serving> Servings { get; }

        public void AddServing(Dish dish)
        {
            Servings.Add(new Serving(dish));
        }

        public void RemoveServing(Dish dish)
        {
            Servings.Remove(Servings.First(serving => serving.Dish == dish));
        }
    }
}