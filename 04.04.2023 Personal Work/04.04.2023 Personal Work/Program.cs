using System;
using System.Collections;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace Program
{
    class Program
    {
        public static void ReadTextByStreamReader(string path)
        {
            using (var reader = new StreamReader(path))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
        }
        public static void addTextByStreamWriter(string path)
        {
            if (File.Exists(path)) File.Delete(path);
            var fs = new FileStream(path, FileMode.CreateNew);

            using (var writer = new StreamWriter(fs))
            {
                writer.WriteLine("Fuck");
                writer.Write("Fuck you");
            }
        }
        static void Main(string[] args)
        {
            string line;
            var lstVehicles = new List<Vehicle>();
            string path = "D:\\Programming\\C#\\Univer\\04.04.2023 Personal Work\\04.04.2023 Personal Work\\Data.csv";
            using (FileStream stream = File.OpenRead(path))
            {
                System.IO.StreamReader file = new System.IO.StreamReader(@path);
                while ((line = file.ReadLine()) != null)
                {
                    string[] arrtibutes = line.Split(',');
                    if (arrtibutes[0] == "vehicle")
                    {
                        lstVehicles.Add(new Vehicle(Convert.ToString(arrtibutes[1]),
                        Convert.ToDouble(arrtibutes[2]),
                        Convert.ToUInt32(arrtibutes[3]),
                        Convert.ToUInt32(arrtibutes[4])));
                    }
                    else if (arrtibutes[0] == "truck")
                    {
                        lstVehicles.Add(new Truck(
                        Convert.ToString(arrtibutes[1]),
                        Convert.ToDouble(arrtibutes[2]),
                        Convert.ToUInt32(arrtibutes[3]),
                        Convert.ToUInt32(arrtibutes[4]),
                        Convert.ToUInt32(arrtibutes[5])
                        ));
                    }
                    else if (arrtibutes[0] == "car")
                    {
                        lstVehicles.Add(new Car(
                        Convert.ToString(arrtibutes[1]),
                        Convert.ToDouble(arrtibutes[2]),
                        Convert.ToUInt32(arrtibutes[3]),
                        Convert.ToUInt32(arrtibutes[4]),
                        Convert.ToUInt32(arrtibutes[5])
                        ));
                    }
                }

                Console.WriteLine("Task A\nUnsorted: ");
                foreach (var i in lstVehicles) { Console.WriteLine(i); }
                Console.WriteLine("\nSorted: \n");
                //var sortedVehicles = lstVehicles.OrderBy(x => x, new VehicleComparer<Vehicle>());

                var sortedVehicles = from v in lstVehicles
                                     orderby v.Mark
                                     select v;

                foreach(var i in sortedVehicles) { Console.WriteLine(i); }

                Console.WriteLine("\nTask B\n");

                var grouped = from v in lstVehicles
                              group v by v.Mark into groups
                              select groups;

                foreach (var i in grouped)
                {
                    foreach (var j in i)
                    {
                        Console.WriteLine(i); 
                    }
                }




                Console.WriteLine("\nTask C");
                
                var trucks = new List<Truck>();

                foreach (var i in lstVehicles)
                {
                    if(i.GetType() == typeof(Truck))
                    {
                        trucks.Add((Truck)i);
                    }
                }

                var mostPowerful = trucks[0];
                foreach(var i in trucks)
                {
                    if (i.VantazhPid > mostPowerful.VantazhPid)
                        mostPowerful = i;
                }
                Console.WriteLine("Most Powerful Truck: ");
                Console.WriteLine(mostPowerful);

                var sortedtrucks = trucks.OrderBy(x => x, new TruckComparer<Truck>());
                Console.WriteLine("Sorted Trucks by wheelNumber: ");
                foreach(var i in sortedtrucks.Reverse()) { Console.WriteLine(i); }
                
                
                
                Console.WriteLine("\nTask D");

                var cars = new List<Car>();

                foreach (var i in lstVehicles)
                {
                    if (i.GetType() == typeof(Car))
                    {
                        cars.Add((Car)i);
                    }
                }

                var mostSeatable = cars[0];
                foreach (var i in cars)
                {
                    if (i.Seats > mostSeatable.Seats)
                    {
                        mostSeatable = i;
                    }
                }
                Console.WriteLine("Car with most seats: ");
                Console.WriteLine(mostSeatable);

                var sortedCars = cars.OrderBy(x => x, new CarComparer<Car>());
                Console.WriteLine("Sorted Cars by Seats: ");
                foreach (var i in sortedCars.Reverse()) { Console.WriteLine(i); }

            }


            //ReadTextByStreamReader("D:\\Programming\\C#\\Univer\\04.04.2023 Personal Work\\04.04.2023 Personal Work\\Data.csv");
        }
    }
    public class Vehicle
    {
        public string Mark { get; set; }
        public double EnginePower { get; set; }
        public uint WheelNumber { get; set; }
        public uint Weight { get; set; }
        public Vehicle(string mark, double ngnpwr, uint whlnmbr, uint wght)
        {
            Mark = mark;
            EnginePower = ngnpwr;
            WheelNumber = whlnmbr;
            Weight = wght;
        }
        public override string ToString()
        {
            return "Mark: " + Mark + " EnginePower: " + EnginePower + " WheelNumber: " + WheelNumber + " Weight: " + Weight;
        }
    }
    public class Truck : Vehicle
    {
        public uint VantazhPid { get; set; }
        public Truck(string mark, double ngnpwr, uint whlnmbr, uint wght, uint v) : base(mark, ngnpwr, whlnmbr, wght)
        {
            VantazhPid = v;
        }
        public override string ToString()
        {
            return "Mark: " + Mark + " EnginePower: " + EnginePower + " WheelNumber: " + WheelNumber + " Weight: " + Weight + " VantazhPid: " + VantazhPid;
        }
    }
    public class Car : Vehicle
    {
        public uint Seats { get; set; }
        public Car(string mark, double ngnpwr, uint whlnmbr, uint wght, uint seats) : base(mark, ngnpwr, whlnmbr, wght)
        {
            Seats = seats;
        }
        public override string ToString()
        {
            return "Mark: " + Mark + " EnginePower: " + EnginePower + " WheelNumber: " + WheelNumber + " Weight: " + Weight + " Seats: " + Seats;
        }
    }
    public class VehicleComparer<T> : IComparer<T> where T : Vehicle
    {
        public int Compare(T x, T y)
        {
            return x.Mark.CompareTo(y.Mark);
        }
    }
    public class TruckComparer<T> : IComparer<T> where T : Truck
    {
        public int Compare(T x, T y)
        {
            return x.WheelNumber.CompareTo(y.WheelNumber);
        }
    }
    public class CarComparer<T> : IComparer<T> where T : Car
    {
        public int Compare(T x, T y)
        {
            return x.Seats.CompareTo(y.Seats);
        }
    }
    public class VehicleEnum : IEnumerable
    {
        private Vehicle[] _v;
        public VehicleEnum(Vehicle[] pArray)
        {
            _v = new Vehicle[pArray.Length];

            for (int i = 0; i < pArray.Length; i++)
            {
                _v[i] = pArray[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }
        public VehicleEnum GetEnumerator()
        {
            return new VehicleEnum(_v);
        }
    }
}