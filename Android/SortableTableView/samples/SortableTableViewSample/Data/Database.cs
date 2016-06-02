using System.Collections.Generic;

namespace SortableTableViewSample.Data
{
    public class Database
    {
        public Database()
        {
            var audi = new CarProducer(Resource.Drawable.audi, "Audi");
            var audiA1 = new Car(audi, "A1", 150, 250000);
            var audiA3 = new Car(audi, "A3", 120, 350000);
            var audiA4 = new Car(audi, "A4", 210, 420000);
            var audiA5 = new Car(audi, "S5", 333, 600000);
            var audiA6 = new Car(audi, "A6", 250, 550000);
            var audiA7 = new Car(audi, "A7", 420, 870000);
            var audiA8 = new Car(audi, "A8", 320, 1100000);

            var bmw = new CarProducer(Resource.Drawable.bmw, "BMW");
            var bmw1 = new Car(bmw, "1er", 170, 250000);
            var bmw3 = new Car(bmw, "3er", 230, 420000);
            var bmwX3 = new Car(bmw, "X3", 230, 450000);
            var bmw4 = new Car(bmw, "4er", 250, 390000);
            var bmwM4 = new Car(bmw, "M4", 350, 600000);
            var bmw5 = new Car(bmw, "5er", 230, 460000);

            var porsche = new CarProducer(Resource.Drawable.porsche, "Porsche");
            var porsche911 = new Car(porsche, "911", 280, 450000);
            var porscheCayman = new Car(porsche, "Cayman", 330, 520000);
            var porscheCaymanGT4 = new Car(porsche, "Cayman GT4", 385, 860000);

            Producers = new List<CarProducer>
            {
                audi,
                bmw,
                porsche
            };

            Cars = new List<Car>
            {
                audiA3,
                audiA1,
                porscheCayman,
                audiA7,
                audiA8,
                audiA4,
                bmwX3,
                porsche911,
                bmw1,
                audiA6,
                audiA5,
                bmwM4,
                bmw5,
                porscheCaymanGT4,
                bmw3,
                bmw4
            };
        }

        public List<Car> Cars { get; private set; }

        public List<CarProducer> Producers { get; private set; }

        public Java.Util.IComparator GetCarProducerComparator()
        {
            return new CarProducerComparator();
        }

        public Java.Util.IComparator GetCarPowerComparator()
        {
            return new CarPowerComparator();
        }

        public Java.Util.IComparator GetCarNameComparator()
        {
            return new CarNameComparator();
        }

        public Java.Util.IComparator GetCarPriceComparator()
        {
            return new CarPriceComparator();
        }

        private class CarProducerComparator : Java.Lang.Object, Java.Util.IComparator
        {
            public int Compare(Java.Lang.Object lhs, Java.Lang.Object rhs)
            {
                var car1 = (Car)lhs;
                var car2 = (Car)rhs;
                return car1.Producer.Name.CompareTo(car2.Producer.Name);
            }
        }

        private class CarPowerComparator : Java.Lang.Object, Java.Util.IComparator
        {
            public int Compare(Java.Lang.Object lhs, Java.Lang.Object rhs)
            {
                var car1 = (Car)lhs;
                var car2 = (Car)rhs;
                return car1.Ps - car2.Ps;
            }
        }

        private class CarNameComparator : Java.Lang.Object, Java.Util.IComparator
        {
            public int Compare(Java.Lang.Object lhs, Java.Lang.Object rhs)
            {
                var car1 = (Car)lhs;
                var car2 = (Car)rhs;
                return car1.Name.CompareTo(car2.Name);
            }
        }

        private class CarPriceComparator : Java.Lang.Object, Java.Util.IComparator
        {
            public int Compare(Java.Lang.Object lhs, Java.Lang.Object rhs)
            {
                var car1 = (Car)lhs;
                var car2 = (Car)rhs;
                if (car1.Price < car2.Price)
                {
                    return -1;
                }
                if (car1.Price > car2.Price)
                {
                    return 1;
                }
                return 0;
            }
        }
    }
}
