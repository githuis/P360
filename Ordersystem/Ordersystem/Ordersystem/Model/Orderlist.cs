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
