using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ordersystem.Enums;

namespace Ordersystem.Model
{
    public class DayMenuSelection
    {
        private bool _sideDish;

        public DayMenuSelection(DayMenu dayMenu, DayMenuChoice choice, bool sideDish)
        {
            DayMenu = dayMenu;
            Choice = choice;
            SideDish = sideDish;
        }

        public DayMenu DayMenu {get; private set;}
        public DayMenuChoice Choice {get; set;}
        public bool SideDish
        {
            get { return _sideDish; }
            set { _sideDish = (Choice != DayMenuChoice.NoDish && Choice != DayMenuChoice.NoChoice) && value; }
        }
        public DateTime Date {get {return DayMenu.Date;} }

        public List<Dish> GetDishes()
        {
            return new List<Dish> {DayMenu.Dish1, DayMenu.Dish2, DayMenu.SideDish};
        }
    }
}
