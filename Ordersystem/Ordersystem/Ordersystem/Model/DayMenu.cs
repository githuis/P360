using System;

namespace Ordersystem.Model
{
    public class DayMenu
    {
        /// <summary>
        /// Default constructor for serialization.
        /// </summary>
        public DayMenu()
        {

        }

        /// <summary>
        /// The menu of a single day on an orderlist.
        /// </summary>
        /// <param name="dish1">The primary dish.</param>
        /// <param name="dish2">The secondary dish.</param>
        /// <param name="sideDish">The side dish.</param>
        /// <param name="date">The date of the menu.</param>
        public DayMenu(Dish dish1, Dish dish2, Dish sideDish, DateTime date)
        {
            if (dish1 == null)
            {
                throw new ArgumentNullException("dish1", "Dish 1 is null.");
            }

            if (dish2 == null)
            {
                throw new ArgumentNullException("dish2", "Dish 2 is null.");
            }

            if (sideDish == null)
            {
                throw new ArgumentNullException("sideDish", "Side dish is null.");
            }

            Dish1 = dish1;
            Dish2 = dish2;
            SideDish = sideDish;
            Date = date;
        }

        /// <summary>
        /// The primary dish.
        /// </summary>
        public Dish Dish1 { get; private set; }

        /// <summary>
        /// The secondary dish.
        /// </summary>
        public Dish Dish2 { get; private set; }

        /// <summary>
        /// The side dish.
        /// </summary>
        public Dish SideDish { get; private set; }

        /// <summary>
        /// The date of the menu.
        /// </summary>
        public DateTime Date { get; private set; }
    }
}