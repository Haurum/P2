using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<MotorVehicle> vehicles = new List<MotorVehicle>()
            {
            new Car() { Make = "Opel", Model = "Zafira", Year = 2002,
            Fuel = Fuel.Octane95, Price = 112000 },
            new Car() { Make = "Ford", Model = "Fiesta", Year = 1994,
            Fuel = Fuel.Octane92, HasSunRoof = true, Price = 72000 },
            new Car() { Make = "Mazda", Model = "6", Year = 2007,
            Fuel = Fuel.Octane95, Price = 200000 },
            new Car() { Make = "Opel", Model = "Astra", Year = 1995,
            Fuel = Fuel.Octane92, HasSunRoof = true, Price = 45000 },
            new Car() { Make = "Opel", Model = "Astra", Year = 1997,
            Fuel = Fuel.Diesel, Price = 52000 },
            new Car() { Make = "Opel", Model = "Zafira", Year = 2001,
            Fuel = Fuel.Diesel, Price = 137000 },
            new Car() { Make = "Ford", Model = "Focus", Year = 2007,
            Fuel = Fuel.Octane92, HasSunRoof = true, Price = 199999 },
            new Car() { Make = "Opel", Model = "Astra", Year = 1996,
            Fuel = Fuel.Diesel, Price = 29000 },
            new Bus() { Make = "Scania", Model = "Buzz", Year = 1999,
            Price = 275000, NumSeats = 52},
            new Bus() { Make = "Scania", Model = "Fuzz", Year = 2000,
            Price = 225000, NumSeats = 12}
            };

            Console.WriteLine(vehicles.Average(vehicle => vehicle.Price));

            Console.WriteLine(vehicles.Where(vehicle => vehicle is Bus).Select(vehicle => vehicle as Bus).Average(vehicle => vehicle.NumSeats));

            Console.WriteLine(vehicles.Where(vehicle => vehicle is Car).Select(vehicle => vehicle as Car).Where(vehicle => vehicle.HasSunRoof == true).Count());

            Console.ReadKey();
        }
    }

    enum Fuel
    {
        Diesel, Octane92, Octane95
    }

    abstract class MotorVehicle
    {
        protected Fuel _fuel;
        public string Make { get; set; } //VW, Audi, Skoda...
        public string Model { get; set; } //Golf, Polo, A3, Fabia, etc.
        public int Year { get; set; }
        public decimal Price { get; set; }
        public virtual Fuel Fuel
        {
            get { return _fuel; }
            set { _fuel = value; }
        }
    }

    class Bus : MotorVehicle
    {
        public Bus()
        {
            _fuel = Fuel.Diesel;
        }
        public int NumSeats { get; set; }
        public override Fuel Fuel
        {
            set { } //do nothing - only diesel is allowed
        }
    }

    class Car : MotorVehicle
    {
        public bool HasSunRoof { get; set; }
    }

    
}
