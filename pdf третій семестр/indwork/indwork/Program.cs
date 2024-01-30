using System;
using System.Runtime.InteropServices;
using System.Linq;
using System.Net.NetworkInformation;
using Microsoft.VisualBasic;

namespace Program
{
    class Program
    {
        public static void Main(string[] args)
        {
            var base1 = new TwoWheelTransport("base1", 100, 80);
            var base2 = new TwoWheelTransport("base2", 120, 120);
            var base3 = new TwoWheelTransport("base3", 60, 90);

            var twe1 = new TwoWheelWithEngine("twe1", 100, 80, 200);
            var twe2 = new TwoWheelWithEngine("twe2", 101, 100, 205);
            var twe3 = new TwoWheelWithEngine("twe3", 102, 90, 210);

            var twfe1 = new TwoWheelWithFuelEngine("twfe1", 100, 90, 20, 40);
            var twfe2 = new TwoWheelWithFuelEngine("twfe2", 100, 90, 20, 50);
            var twfe3 = new TwoWheelWithFuelEngine("twfe3", 100, 90, 20, 60);

            var twee1 = new TwoWheelWithElectroEngine("twee1", 100, 80, 20, 41);
            var twee2 = new TwoWheelWithElectroEngine("twee2", 100, 90, 20, 52);
            var twee3 = new TwoWheelWithElectroEngine("twee3", 100, 80, 20, 63);

            var autopark = new List<TwoWheelTransport> { base1, base2, base3, twe1, twe2, twe3, twfe1, twfe2, twfe3, twee1, twee2, twee3 };

            Console.WriteLine("TaskA");
            foreach (var i in autopark)
            {
                i.printTransport();
            }

            Console.WriteLine("\nTaskB");
            var TwoWheelsWithE = new List<TwoWheelWithEngine> { };
            foreach (var i in autopark)
            {
                if (i.GetType() == typeof(TwoWheelWithEngine))
                    TwoWheelsWithE.Add((TwoWheelWithEngine)i);
            }

            var OrderedTwoWheelsWithE = TwoWheelsWithE.OrderByDescending(i => i.EnginePower);

            Console.WriteLine("Most Powerful Engines: ");
            foreach (var i in OrderedTwoWheelsWithE)
            {
                i.printTransport();
            }

            Console.WriteLine("\nTaskC");
            var mass = 80;
            var toPrint = autopark.FindAll(i => i.GetType() == typeof(TwoWheelWithElectroEngine) && i.MaxMass == mass);
            foreach (var i in toPrint)
            {
                i.printTransport();
            }
        }
    }
    class TwoWheelTransport
    {
        private string mark;
        private int maxSpeed;
        private int maxMass;
        public TwoWheelTransport(string mrk, int mxSpeed, int mxMass)
        {
            mark = mrk;
            maxSpeed = mxSpeed;
            maxMass = mxMass;
        }
        public string Mark { get { return mark; } set { mark = value; } }
        public int MaxSpeed { get { return maxSpeed; } set { maxSpeed = value; } }
        public int MaxMass { get { return maxMass; } set { maxMass = value; } }
        public virtual void printTransport()
        {
            Console.WriteLine("Mark : " + Mark + " MaxSpeed: " + MaxSpeed + " MaxMass: " + MaxMass);
        }
    }
    class TwoWheelWithEngine : TwoWheelTransport
    {
        private int enginePower;

        public TwoWheelWithEngine(string mrk, int mxSpeed, int mxMass, int ngnPwr) : base(mrk, mxSpeed, mxMass)
        {
            enginePower = ngnPwr;
        }
        public int EnginePower { get { return enginePower; } set { enginePower = value; } }
        public override void printTransport()
        {
            Console.WriteLine("Mark : " + Mark + " MaxSpeed: " + MaxSpeed + " MaxMass: " + MaxMass + " Engine Power: " + EnginePower);
        }
    }
    class TwoWheelWithFuelEngine : TwoWheelWithEngine
    {
        private int capacity;

        public TwoWheelWithFuelEngine(string mrk, int mxSpeed, int mxMass, int ngnPwr, int cpct) : base(mrk, mxSpeed, mxMass, ngnPwr)
        {
            capacity = cpct;
        }
        public int Capacity { get { return capacity; } set { capacity = value; } }
        public override void printTransport()
        {
            Console.WriteLine("Mark : " + Mark + " MaxSpeed: " + MaxSpeed + " MaxMass: " + MaxMass + " Engine Power: " + EnginePower + " Capacity: " + Capacity);
        }
    }
    class TwoWheelWithElectroEngine : TwoWheelWithEngine
    {
        private int ecapacity;

        public TwoWheelWithElectroEngine(string mrk, int mxSpeed, int mxMass, int ngnPwr, int ecpct) : base(mrk, mxSpeed, mxMass, ngnPwr)
        {
            ecapacity = ecpct;
        }
        public int Ecapacity { get { return ecapacity; } set { ecapacity = value; } }
        public override void printTransport()
        {
            Console.WriteLine("Mark : " + Mark + " MaxSpeed: " + MaxSpeed + " MaxMass: " + MaxMass + " Engine Power: " + EnginePower + " ECapacity: " + Ecapacity);
        }
    }
}
