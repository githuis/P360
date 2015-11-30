using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using Ordersystem.Enums;

namespace Ordersystem.Model
{
    public class Order
    {
        /// <summary>
        /// An order for Master Cater System.
        /// </summary>
        public Order()
        {
            DayMenuSelections = new List<DayMenuSelection>();
            Sent = false;
        }

        /// <summary>
        /// A list of the selections in this order.
        /// </summary>
        public List<DayMenuSelection> DayMenuSelections { get; private set; }

        /// <summary>
        /// Whether the order has already been sent.
        /// </summary>
        public bool Sent { get; set; }

        /// <summary>
        /// Add a selection of a DayMenu to the order.
        /// </summary>
        /// <param name="dayMenu">The menu chosen from.</param>
        /// <param name="choice">The choice of dish.</param>
        /// <param name="sideDish">Whether a side dish is included. False by default.</param>
        public void AddDayMenuSelection(DayMenu dayMenu, DayMenuChoice choice, bool sideDish = false)
        {
            DayMenuSelections.Add(new DayMenuSelection(dayMenu, choice, sideDish));

            DayMenuSelections.Sort((x, y) => x.Date.CompareTo(y.Date));
        }

        /// <summary>
        /// Changes a selection in the order.
        /// </summary>
        /// <param name="dayMenu">The DayMenu of the selection to be changed.</param>
        /// <param name="dishChoice">The choice to change the selection to.</param>
        /// <param name="sideDishChoice">Whether a side dish is to be included. False by default.</param>
        public void ChangeDayMenuSelection(DayMenu dayMenu, DayMenuChoice dishChoice, bool sideDishChoice = false)
        {
            DayMenuSelection dayMenuSelection = DayMenuSelections.FirstOrDefault(dms => dms.DayMenu == dayMenu);

            if (dayMenuSelection == null) throw new NullReferenceException("No selection for given dayMenu found.");

            dayMenuSelection.Choice = dishChoice;
            dayMenuSelection.SideDish = sideDishChoice;

            DayMenuSelections.Sort((x, y) => x.Date.CompareTo(y.Date));
        }
        
    }
}
