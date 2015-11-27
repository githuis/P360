using System;

namespace Ordersystem.Model
{
    public class DayMenu
    {
        public DayMenu(Dish dish1, Dish dish2, Dish sideDish, DateTime date)
        {
            Dish1 = dish1;
            Dish2 = dish2;
            SideDish = sideDish;
            Date = date;
        }

        public Dish Dish1 { get; private set; }
        public Dish Dish2 { get; private set; }
        public Dish SideDish { get; private set; }
        public DateTime Date { get; private set; }

        // TODO: Integrer med databaser.
    }
}