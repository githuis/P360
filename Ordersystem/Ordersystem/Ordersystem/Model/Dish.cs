using System.Collections.Generic;

namespace Ordersystem.Model
{
    public class Dish
    {
        /// <summary>
        /// A dish.
        /// </summary>
        /// <param name="name">The name of the dish.</param>
        /// <param name="description">A description of the dish.</param>
        public Dish(string name, string description)
        {
            Name = name;
            Description = description;
        }

        /// <summary>
        /// The name of the dish.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// A description of the dish.
        /// </summary>
        public string Description { get; private set; }

        //public Image image { get; set; }
        // TODO: Find ud af hvordan vi inkluderer billedet.
    }
}
