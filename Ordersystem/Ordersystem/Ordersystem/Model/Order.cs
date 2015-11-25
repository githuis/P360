using System.Collections.Generic;
using System.Linq;

namespace Ordersystem.Model
{
    public class Order
    {
        public Order()
        {
            DayMenuSelections = new List<DayMenuSelection>();
        }
        
        public List<DayMenuSelection> DayMenuSelections { get; private set; }

        public void AddDayMenuSelection(DayMenu dayMenu)
        {
            DayMenuSelections.Add(new DayMenuSelection(dayMenu));
        }

        public void ChangeDayMenuSelection(DayMenu dayMenu, DayMenuChoice dishChoice, bool sideDishChoice)
        {
            DayMenuSelections.First((DayMenuSelection) => DayMenuSelection.DayMenu == dayMenu).Choice = dishChoice;
            DayMenuSelections.First((DayMenuSelection) => DayMenuSelection.DayMenu == dayMenu).SideDish = sideDishChoice;
        }
        
    }
}
