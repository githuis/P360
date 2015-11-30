using System;
using System.Collections.Generic;
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

        public DayMenuSelection GetDayMenuSelection(DayMenu dayMenu)
        {
            DayMenuSelection dayMenuSelection = DayMenuSelections.FirstOrDefault(dms => dms.DayMenu == dayMenu);

            if (dayMenuSelection == null) throw new NullReferenceException("No selection for given dayMenu found.");

            return dayMenuSelection;
        }

        public void AddDayMenuSelection(DayMenu dayMenu, DayMenuChoice choice, bool sideDish)
        {
            DayMenuSelections.Add(new DayMenuSelection(dayMenu, choice, sideDish));
        }

        public void RemoveDayMenuSelection(DayMenu dayMenu)
        {
            DayMenuSelection selectionToRemove = DayMenuSelections.FirstOrDefault(dms => dms.DayMenu == dayMenu);

            if(selectionToRemove == null) throw new NullReferenceException("No selection for given dayMenu found.");

            DayMenuSelections.Remove(selectionToRemove);
        }

        public void ChangeDayMenuSelection(DayMenu dayMenu, DayMenuChoice dishChoice, bool sideDishChoice)
        {
            DayMenuSelection dayMenuSelection = DayMenuSelections.FirstOrDefault(dms => dms.DayMenu == dayMenu);

            if (dayMenuSelection == null) throw new NullReferenceException("No selection for given dayMenu found.");

            dayMenuSelection.Choice = dishChoice;
            dayMenuSelection.SideDish = sideDishChoice;
        }
        
    }
}
