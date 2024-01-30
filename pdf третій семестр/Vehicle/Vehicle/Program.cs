using System;
using System.Runtime.InteropServices;
using System.Linq;
using System.Net.NetworkInformation;

namespace Program
{
    class Program
    {
        public static void Main(string[] args)
        {
            var v1 = new Vehicle("vehicle1", 10, 12, 14);
            var v2 = new Vehicle("vehicle2", 4, 2, 4);
            var v3 = new Vehicle("vehicle3", 5, 2, 4);

            var t1 = new Truck("truck1", 60, 2, 3, 500);
            var t2 = new Truck("truck2", 20, 3, 4, 1500);
            var t3 = new Truck("truck3", 16, 3, 4, 1500);
            var t4 = new Truck("truck3", 17, 3, 4, 2000);

            var c1 = new Car("car1", 11, 4, 1250, 4);
            var c2 = new Car("car2", 18, 4, 1000, 5);
            var c3 = new Car("car3", 20, 4, 1550, 6);
            var c4 = new Car("car4", 20, 4, 1000, 6);
            var c5 = new Car("car5", 20, 4, 700, 6);
            var c6 = new Car("car6", 18, 4, 2000, 6);

            var b1 = new Bus("bus1", 14, 2, 3, 40, 50);
            var b2 = new Bus("bus2", 15, 2, 3, 35, 35);
            var b3 = new Bus("bus3", 15, 2, 3, 30, 35);



            Console.WriteLine("Task A");
            var autopark = new Vehicle[] { v1, v2, v3, t1, t2, t3, t4, c1, c2, c3, c4, c5, c6, b1, b2, b3};
            foreach (var i in autopark) { Console.WriteLine(i.ToString()); }



            Console.WriteLine("\nTask B");
            int summedLoadCapacity = 0;
            foreach (var i in autopark)
            {
                if (i.GetType() == typeof(Truck))
                    summedLoadCapacity += ((Truck)i).LoadCapacity;
            }
            Console.WriteLine("Cумарна вантажопiдйомнiсть транспортних засобiв автопарку: " + summedLoadCapacity);



            Console.WriteLine("\nTask C");
            int max = 0;
            foreach (var i in autopark)
            {
                if (i.EnginePower > max && i.GetType() == typeof(Car))
                    max = i.EnginePower;
            }
            
            Console.WriteLine("Перелiк авто, якi мають найбiльшу потужнiсть:");
            foreach (var i in autopark)
            {
                if (i.EnginePower == max && i.GetType() == typeof(Car))
                    Console.WriteLine(i.ToString());
            }




            Console.WriteLine("\nTask D");
            int countOfPeople = 0;
            foreach (var i in autopark)
            {
                if (i.GetType() == typeof(Car) || i.GetType() == typeof(Bus))
                {
                    countOfPeople += ((Car)i).NumOfSeats;
                }
                if (i.GetType() == typeof(Bus))
                {
                    countOfPeople += ((Bus)i).NumOfStands;
                }
            }
            Console.WriteLine("Кiлькiсть пасажирiв, яку можна одночасно перевезти усiма транспортними засобами автопарку: " + countOfPeople);
        }

        class Vehicle
        {
            private string mark;
            private int enginePower;
            private int numOfWheels;
            private int mass;

            public string Mark { get => mark; set { mark = value; } }
            public int EnginePower { get => enginePower; set { enginePower = value; } }
            public int NumOfWheels { get => numOfWheels; set { numOfWheels = value; } }
            public int Mass { get => mass; set { mass = value; } }

            public Vehicle(string mrk, int ngnPwr, int nmfWhls, int mss)
            {
                mark = mrk;
                enginePower = ngnPwr;
                numOfWheels = nmfWhls;
                mass = mss;
            }
            public virtual string ToString()
            {
                return "Mark: " + Mark + " EnginePower: " + EnginePower + " NumOfWheels: " + NumOfWheels + " Mass: " + Mass;
            }
        }
        class Truck : Vehicle
        {
            private int loadCapacity;
            public int LoadCapacity { get => loadCapacity; set { loadCapacity = value; } }
            public Truck(string mrk, int ngnPwr, int nmfWhls, int mss, int strngth) : base(mrk, ngnPwr, nmfWhls, mss)
            {
                loadCapacity = strngth;
            }
            public override string ToString()
            {
                return "Mark: " + Mark + " EnginePower: " + EnginePower + " NumOfWheels: " + NumOfWheels + " Mass: " + Mass + " LoadCapacity: " + LoadCapacity;
            }
        }
        class Car : Vehicle
        {
            private int numOfSeats;
            public int NumOfSeats { get => numOfSeats; set { numOfSeats = value; } }
            public Car(string mrk, int ngnPwr, int nmfWhls, int mss, int nmfSts) : base(mrk, ngnPwr, nmfWhls, mss)
            {
                numOfSeats = nmfSts;
            }
            public override string ToString()
            {
                return "Mark: " + Mark + " EnginePower: " + EnginePower + " NumOfWheels: " + NumOfWheels + " Mass: " + Mass + " NumOfSeats: " + NumOfSeats;
            }
        }
        class Bus : Car
        {
            private int numofStands;
            public int NumOfStands { get => numofStands; set { numofStands = value; } }
            public Bus(string mrk, int ngnPwr, int nmfWhls, int mss, int nmfSts, int nmfStnds) : base(mrk, ngnPwr, nmfWhls, mss, nmfSts)
            {
                numofStands = nmfSts;
            }
            public override string ToString()
            {
                return "Mark: " + Mark + " EnginePower: " + EnginePower + " NumOfWheels: " + NumOfWheels + " Mass: " + Mass + " NumOfSeats: " + NumOfSeats + " NumOfStands: " + numofStands;
            }
        }
    }
}
