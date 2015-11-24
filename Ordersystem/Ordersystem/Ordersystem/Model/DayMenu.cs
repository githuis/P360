namespace Ordersystem.Model
{
    public class DayMenu
    {
        public DayMenu(Dish dish1, Dish dish2, Dish sideDish)
        {
            Dish1 = dish1;
            Dish2 = dish2;
            SideDish = sideDish;
        }

        public Dish Dish1 { get; private set; }
        public Dish Dish2 { get; private set; }
        public Dish SideDish { get; private set; }

        // TODO: Integrer med databaser.
    }
}