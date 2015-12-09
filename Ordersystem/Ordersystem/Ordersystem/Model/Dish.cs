using System.Collections.Generic;

namespace Ordersystem.Model
{
    public class Dish
    {
        public Dish(string name, string description)
        {
            Name = name;
            Description = description;
			Number = 1;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
		public int Number { get; set; }

        public static Dish[] SelectedDishes;
        //public Image image { get; set; }
        // TODO: Find ud af hvordan vi inkluderer billedet.

        // TODO: Integrer med databaser.
    }
}
