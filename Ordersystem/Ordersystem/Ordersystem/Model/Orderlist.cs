using System;
using System.Collections.Generic;
using Ordersystem.Enums;

namespace Ordersystem.Model
{
    public class Orderlist
    {
        public Orderlist(List<DayMenu> dayMenus, DateTime startDate, DateTime endDate, Diet diet)
        {
            DayMenus = dayMenus;
            StartDate = startDate;
            EndDate = endDate;
            Diet = diet;
        }

        public List<DayMenu> DayMenus { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public Diet Diet { get; private set; }

        public void AddDayMenu(DayMenu dayMenu)
        {
            DayMenus.Add(dayMenu);
        }

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
