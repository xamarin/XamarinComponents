namespace SortableTableViewSample.Data
{
    public class Car : Java.Lang.Object, IChargable
    {
        public Car(CarProducer producer, string name, int ps, double price)
        {
            Producer = producer;
            Name = name;
            Ps = ps;
            Price = price;
        }

        public CarProducer Producer { get; private set; }

        public string Name { get; private set; }

        public int Ps { get; private set; }

        public int Kw
        {
            get { return (int)(Ps / 1.36); }
        }

        public double Price { get; private set; }
    }
}
