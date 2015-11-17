using System.Collections.Generic;

namespace Ordersystem.Model
{
    public class Dish
    {
        public Dish(string name, List<string> ingredients)
        {
            Name = name;
            Ingredients = ingredients;
        }

        public string Name { get; private set; }
        public List<string> Ingredients { get; private set; }
        //public Image image { get; set; }
        // TODO: Find ud af hvordan vi inkluderer billedet.

        public void AddIngredient(string ingredient)
        {
            Ingredients.Add(ingredient);
        }

        public void RemoveIngredient(string ingredient)
        {
            Ingredients.Remove(ingredient);
        }

        // TODO: Integrer med databaser.
    }
}
