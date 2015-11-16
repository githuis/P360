using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
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

        public List<DayMenu> DayMenus { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Diet { get; set; }

        // TODO: Integrer med databaser.
    }
}
