using System;
using System.Collections.Generic;
using System.Linq;
using Ordersystem.Enums;
using System.Xml.Serialization;
namespace Ordersystem.Model
{
    [XmlType]
    public class Orderlist
    {
        /// <summary>
        /// Default constructor for serialization.
        /// </summary>
        public Orderlist()
        {

        }

        /// <summary>
        /// The Orderlist recieved from the Master Cater System.
        /// </summary>
        /// <param name="dayMenus">The list containing all the DayMenus.</param>
        /// <param name="startDate">The starting date of the Orderlist.</param>
        /// <param name="endDate">The end date of the Orderlist.</param>
        /// <param name="diet">The diet of the Orderlist.</param>
		/// <exception cref="System.ArgumentNullException">Thrown when an argument is null.</exception>
        public Orderlist(List<DayMenu> dayMenus, DateTime startDate, DateTime endDate, Diet diet)
        {
            if (dayMenus == null)
            {
                throw new ArgumentNullException("dayMenus", "List of dayMenu is null.");
            }

            DayMenus = dayMenus;
            StartDate = startDate;
            EndDate = endDate;
            Diet = diet;
        }

        /// <summary>
        /// The list containing all the DayMenus.
        /// </summary>
        [XmlIgnore]
        public List<DayMenu> DayMenus { get; private set; }

		public bool Active { get { return DateTime.Today >= StartDate && DateTime.Today <= EndDate; } }

        /// <summary>
        /// The starting date of the Orderlist.
        /// </summary>
        public DateTime StartDate { get; private set; }

        /// <summary>
        /// The end date of the Orderlist.
        /// </summary>
        public DateTime EndDate { get; private set; }

        /// <summary>
        /// The diet of the Orderlist.
        /// </summary>
        public Diet Diet { get; private set; }

        /// <summary>
        /// Adds a DayMenu to the Orderlist.
        /// </summary>
        /// <param name="dayMenu">The DayMenu to be added.</param>
        public void AddDayMenu(DayMenu dayMenu)
        {
            if (dayMenu == null)
            {
                throw new ArgumentNullException("dayMenu", "dayMenu is null.");
            }

            DayMenus.Add(dayMenu);
        }

        /// <summary>
        /// Gets a DayMenu based on a date.
        /// </summary>
        /// <param name="date">The date of the DayMenu to be fetched.</param>
        /// <returns>The DayMenu with that date, if found.</returns>
        public DayMenu GetDayMenuByDate(DateTime date)
        {
            DayMenu dayMenu = DayMenus.FirstOrDefault(d => d.Date == date);

            if (dayMenu == null)
            {
                throw new NullReferenceException("No DayMenu with that date was found.");
            }

            return dayMenu;
        }
    }
}
