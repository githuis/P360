namespace Ordersystem.Model
{
    public enum Diet
    {
        Full,
        LowFat,
        EnergyDense,
        SoftFoodsWPotatoes,
        SoftFoodsWMash
    } // Danish translation: Fuldkost, Hjertesund, Energitæt, Blødkost m/kartofler, Blødkost m/kartoffelmos

    public class Customer
    {
        public Customer(int personNumber, string name, Diet diet)
        {
            PersonNumber = personNumber;
            Name = name;
            Order = new Order();
            Diet = diet;
        }

        public int PersonNumber { get; private set; }
        public string Name { get; private set; }
        public Order Order { get; set; }
        public Diet Diet { get; private set; }
    }
}