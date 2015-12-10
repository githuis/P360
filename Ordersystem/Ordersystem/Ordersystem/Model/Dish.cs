using Xamarin.Forms;

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
            Name = name;
            Description = description;
            ImageUrl = imageSource;
            //Image = new Image { Source = imageSource };
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
        public Image Image { get; private set; }

        public int Number { get; set; }

        public string ImageUrl { get; set; }

        public static Dish[] SelectedDishes;
    }
}
