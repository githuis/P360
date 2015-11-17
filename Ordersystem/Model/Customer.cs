using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Customer
    {
        // Danish translation: Fuldkost, Hjertesund, Energitæt, Blødkost m/kartofler, Blødkost m/kartoffelmos
        public enum Diet { Full, LowFat, EnergyDense, SoftFoodsWPotatoes, SoftFoodsWMash} 
        public int PersonNumber { get; private set; }
        public string Name { get; private set; }
        public Order Order;
    }
}
