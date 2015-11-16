using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class DayMenu
    {
        public DayMenu(Dish dish1, Dish dish2, Dish sideDish)
        {
            Dish1 = dish1;
            Dish2 = dish2;
            SideDish = sideDish;
        }

        public Dish Dish1 { get; set; }
        public Dish Dish2 { get; set; }
        public Dish SideDish { get; set; }

        public List<Dish> ReadDayMenu()
        {
            return new List<Dish> {Dish1, Dish2, SideDish};
        }

        // TODO: Integrer med databaser.
    }
}
