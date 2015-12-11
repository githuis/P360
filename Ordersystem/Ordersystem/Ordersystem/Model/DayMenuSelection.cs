using System;
using System.Collections.Generic;
using Ordersystem.Enums;
using System.Xml.Serialization;

namespace Ordersystem.Model
{
    public class DayMenuSelection
    {
        private bool _sideDish;

        /// <summary>
        /// Default constructor for serialization.
        /// </summary>
        public DayMenuSelection()
        {

        }

        /// <summary>
        /// The selection from a DayMenu to be added to an order.
        /// </summary>
        /// <param name="dayMenu">The DayMenu this is a selection from.</param>
        /// <param name="choice">The choice of dish.</param>
        /// <param name="sideDish">Whether to save a side dish or not.</param>
        public DayMenuSelection(DayMenu dayMenu, DayMenuChoice choice, bool sideDish = false)
        {
            if (dayMenu == null)
            {
                throw new ArgumentNullException("dayMenu", "dayMenu is null.");
            }

            DayMenu = dayMenu;
            Choice = choice;
            SideDish = sideDish;
        }

        /// <summary>
        /// The DayMenu this is a selection from.
        /// </summary>
        [XmlIgnore]
        public DayMenu DayMenu {get; private set;}

        /// <summary>
        /// The choice of dish.
        /// </summary>
        public DayMenuChoice Choice {get; set;}

        /// <summary>
        /// Whether to save a side dish or not.
        /// Can not be true if Choice is NoDish or NoChoise.
        /// </summary>
        public bool SideDish
        {
            get { return _sideDish; }
            set { _sideDish = (Choice != DayMenuChoice.NoDish && Choice != DayMenuChoice.NoChoice) && value; }
        }

        /// <summary>
        /// The date of the DayMenu.
        /// </summary>
        public DateTime Date {get {return DayMenu.Date;} }

        /// <summary>
        /// Gets the dishes from the DayMenu.
        /// </summary>
        /// <returns>A List of the dishes.</returns>
        public List<Dish> GetDishes()
        {
            return new List<Dish> {DayMenu.Dish1, DayMenu.Dish2, DayMenu.SideDish};
        }
    }
}
