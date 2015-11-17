using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Order
    {
        public List<Serving> Servings;

        public void AddServing(Dish dish)
        {
            Servings.Add(new Serving(dish));
        }
        public void RemoveServing(Dish dish)
        {
            Servings.Remove(Servings.First((serving) => serving.Dish == dish));
        }
    }
}
