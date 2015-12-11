using System;

namespace Ordersystem.Model
{
    public class Dish
    {
        /// <summary>
        /// A dish.
        /// </summary>
        /// <param name="name">The name of the dish.</param>
        /// <param name="description">A description of the dish.</param>
        /// <param name="imageSource">The source path for the image.</param>
        public Dish(string name, string description, string imageSource)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("name", "No name found.");
            }

            if (String.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentNullException("description", "No description found.");
            }

            if (imageSource == null)
            {
                throw new ArgumentNullException("imageSource", "No image source found.");
            }

            Name = name;
            Description = description;
            ImageSource = imageSource;
        }

        /// <summary>
        /// The name of the dish.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// A description of the dish.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// An image of the dish.
        /// </summary>
        public string ImageSource { get; private set; }

		public static Dish[] SelectedDishes;
		public static Dish[] SelectedSideDishes;
    }
}
