using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordersystem.Model
{
    public enum DayMenuChoice { Dish1, Dish2, NoDish }
    public class DayMenuSelection
    {
        public DayMenuSelection(DayMenu dayMenu)
        {
            dayMenu = DayMenu;
        }

        public DayMenu DayMenu {get; private set;}
        public DayMenuChoice Choice {get; set;}
        public bool SideDish
        {
            get{ return SideDish; }
            set
            {
                if (Choice != DayMenuChoice.NoDish)
                    SideDish = true;
            }
        }

        public List<Dish> GetDishes()
        {
            List<Dish> Dishes = new List<Dish>();
            Dishes.Add(DayMenu.Dish1);
            Dishes.Add(DayMenu.Dish2);
            Dishes.Add(DayMenu.SideDish);
            return Dishes;
        }
    }
}
