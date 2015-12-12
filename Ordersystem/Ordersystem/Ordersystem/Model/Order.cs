using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Ordersystem.Enums;

namespace Ordersystem.Model
{
    [XmlType]
    public class Order
    {

        /// <summary>
        /// An order for Master Cater System.
        /// </summary>
	public Order()
        {
		//DayMenuSelections = DayMenuSelection [daysInMonth];
            	Sent = false;
        }

        /// <summary>
        /// A list of the selections in this order.
        /// </summary>
		public DayMenuSelection[]  DayMenuSelections { get; private set; }
        //public List<DayMenuSelection> DayMenuSelections { get; private set; }

		public void SetSelectionLength(int daysInMonth, Orderlist orderlist)
		{
			DayMenuSelections = new DayMenuSelection[daysInMonth];
			for (int i = 0; i < daysInMonth; i++) 
			{
				DayMenuSelections [i] = new DayMenuSelection (orderlist.DayMenus[i], DayMenuChoice.NoChoice, false);
			}
		}

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
		public void AddDayMenuSelection(DayMenu dayMenu, DayMenuChoice choice = DayMenuChoice.NoChoice, bool sideDish = false)
        {
            if (dayMenu == null)
            {
                throw new ArgumentNullException("dayMenu", "dayMenu is null.");
            }

            //DayMenuSelections.Add(new DayMenuSelection(dayMenu, choice, sideDish));
			DayMenuSelections[dayMenu.Date.Day-1] = new DayMenuSelection(dayMenu, choice, sideDish); //Add selection at day in month

            //DayMenuSelections.Sort((x, y) => x.Date.CompareTo(y.Date));
        }

        /// <summary>
        /// Changes a selection in the order.
        /// </summary>
        /// <param name="dayMenu">The DayMenu of the selection to be changed.</param>
        /// <param name="dishChoice">The choice to change the selection to.</param>
        /// <param name="sideDishChoice">Whether a side dish is to be included. False by default.</param>
        public void ChangeDayMenuSelection(DateTime date, DayMenuChoice dishChoice, bool sideDishChoice)
        {
			DayMenuSelections[date.Day-1].Choice = dishChoice;
			DayMenuSelections[date.Day-1].SideDish = sideDishChoice;

            //DayMenuSelections.Sort((x, y) => x.Date.CompareTo(y.Date));
        }
    }
}
