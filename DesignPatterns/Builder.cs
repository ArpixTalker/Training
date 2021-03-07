using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{

    public interface ICarFactory {

        Car BuildCar(string type);
        string ToString();
    }

    public class CarProvider {

        public ICarFactory Factory { get; private set; }

        public CarProvider(ICarFactory factory) {

            this.Factory = factory;
        }

        public CarProvider() {

            this.Factory = null;
        }

        public void ChangeManufacturer(ICarFactory factory) {

            this.Factory = factory;
        }

        public Car ProvideCar(string type) {

            return this.Factory.BuildCar(type);
        }
    }

    public class ToyotaFactory : ICarFactory {

        private Car car;

        public ToyotaFactory() { }

        public Car BuildCar(string type) {

            switch (type) {

                case "Corrola": return this.BuildCorrola();
                case "Pickup": return this.BuildPickup();
                default: throw new NotImplementedException("This type is not provided");

            }
        }

        private Car BuildCorrola() {

            this.car = new Car("Toyota", "Corrola");
            this.car.Engine = "TSI 1.1 8V";
            this.car.Brakes = "Profi 156-X";
            this.car.Wheels = "Promo 24'";
            this.car.Transmission = "T-Semi-automatic";
            this.car.Price = 250000;
            this.car.ManufacturingDate = DateTime.UtcNow.ToString();
            return this.car;
        }

        private Car BuildPickup()
        {

            this.car = new Car("Toyota", "Pickup");
            this.car.Engine = "TDI 1.6 8V";
            this.car.Brakes = "Crafts 3-SD";
            this.car.Wheels = "Promo 28'";
            this.car.Transmission = "T-Manual";
            this.car.Price = 325000;
            this.car.ManufacturingDate = DateTime.UtcNow.ToString();
            return this.car;
        }
    }

    public class SkodaFactory : ICarFactory
    {

        private Car car;

        public SkodaFactory() { }

        public Car BuildCar(string type)
        {

            switch (type)
            {

                case "Octavia": return this.BuildOctavia();
                case "Yetti": return this.BuildYetti();
                default: throw new NotImplementedException("This type is not provided");

            }
        }

        private Car BuildOctavia()
        {

            this.car = new Car("Skoda", "Octavia");
            this.car.Engine = "TSI 1.2 6V";
            this.car.Brakes = "Skoda Brakes";
            this.car.Wheels = "S-23'";
            this.car.Transmission = "Automat-S15";
            this.car.Price = 234000;
            this.car.ManufacturingDate = DateTime.UtcNow.ToString();
            return this.car;
        }

        private Car BuildYetti()
        {

            this.car = new Car("Skoda", "Yetti");
            this.car.Engine = "TDI 1.8 8V";
            this.car.Brakes = "Skoda Brakes";
            this.car.Wheels = "S-25'";
            this.car.Transmission = "T-Manual";
            this.car.Price = 425000;
            this.car.ManufacturingDate = DateTime.UtcNow.ToString();
            return this.car;
        }
    }

    public class Car
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Price { get; set; }
        public string ManufacturingDate { get;  set; }
        public string Engine { get; set; }
        public string Wheels { get; set; }
        public string Transmission { get;  set; }
        public string Brakes{ get; set; }
        public string Equipment { get; set; }

        public Car(string brand, string model) {

            this.Brand = brand;
            this.Model = model;
        }

        public override string ToString()
        {
            return $"{this.Brand} {this.Model} Manuf: {this.ManufacturingDate} >> {this.Engine},{this.Wheels},{this.Brakes},{this.Transmission} Price: {this.Price}";
        }
    }

    /* -- MAIN -- 
     
            Car newCar;
            var provider = new CarProvider();

            provider.ChangeManufacturer(new ToyotaFactory());
            newCar = provider.ProvideCar("Corrola");
            Console.WriteLine(newCar.ToString());

            provider.ChangeManufacturer(new SkodaFactory());
            newCar = provider.ProvideCar("Octavia");
            Console.WriteLine(newCar.ToString());

            provider.ChangeManufacturer(new ToyotaFactory());
            newCar = provider.ProvideCar("Pickup");
            Console.WriteLine(newCar.ToString());

            provider.ChangeManufacturer(new SkodaFactory());
            newCar = provider.ProvideCar("Yetti");
            Console.WriteLine(newCar.ToString());

            Console.ReadLine();
     
     */
}
