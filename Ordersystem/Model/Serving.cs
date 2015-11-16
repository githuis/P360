using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Serving
    {
        public Serving(Dish dish)
        {
            Dish = dish;
        }
        public Dish Dish { get; private set; }
    }
}
