using System;
using System.Collections.Generic;

namespace Ordersystem.Model
{
    public class Orderlist
    {
        public Orderlist(List<DayMenu> dayMenus, DateTime startDate, DateTime endDate, string diet)
        {
            DayMenus = dayMenus;
            StartDate = startDate;
            EndDate = endDate;
            Diet = diet;
        }

        public List<DayMenu> DayMenus { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public string Diet { get; private set; }

        public void AddDayMenu(DayMenu dayMenu)
        {
            DayMenus.Add(dayMenu);
        }

        public void RemoveDayMenu(DayMenu dayMenu)
        {
            DayMenus.Remove(dayMenu);
        }

        public DayMenu GetDayMenuByID(int id)
        {
            return DayMenus[id];
        }

        // TODO: Integrer med databaser.
    }
}
