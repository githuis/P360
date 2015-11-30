using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using Ordersystem.Enums;

namespace Ordersystem.Model
{
    public class Order
    {
        public Order()
        {
            DayMenuSelections = new List<DayMenuSelection>();
        }

        public List<DayMenuSelection> DayMenuSelections { get; private set; }

        public void AddDayMenuSelection(DayMenu dayMenu, DayMenuChoice choice, bool sideDish)
        {
            DayMenuSelections.Add(new DayMenuSelection(dayMenu, choice, sideDish));

            DayMenuSelections.Sort((x, y) => x.Date.CompareTo(y.Date));
        }

        public void ChangeDayMenuSelection(DayMenu dayMenu, DayMenuChoice dishChoice, bool sideDishChoice)
        {
            DayMenuSelection dayMenuSelection = DayMenuSelections.FirstOrDefault(dms => dms.DayMenu == dayMenu);

            if (dayMenuSelection == null) throw new NullReferenceException("No selection for given dayMenu found.");

            dayMenuSelection.Choice = dishChoice;
            dayMenuSelection.SideDish = sideDishChoice;

            DayMenuSelections.Sort((x, y) => x.Date.CompareTo(y.Date));
        }
        
    }
}
