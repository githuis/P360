using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Dish
    {
        public Dish(string name, List<string> ingredients)
        {
            Name = name;
            Ingredients = ingredients;
        }

        public string Name { get; set; }
        public List<string> Ingredients { get; set; }
        //public Image image { get; set; }
        // TODO: Find ud af hvordan vi inkluderer billedet.

        // TODO: Integrer med databaser.
    }
}
