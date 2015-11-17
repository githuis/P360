namespace Ordersystem.Model
{
    public class Serving
    {
        public Serving(Dish dish)
        {
            Dish = dish;
        }

        public Dish Dish { get; private set; }
    }
}
