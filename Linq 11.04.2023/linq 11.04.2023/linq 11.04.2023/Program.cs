namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            string line;
            var lstVehicles = new List<TwoWheelVehicle>();
            string path1 = "D:\\Programming\\C#\\Univer\\Linq 11.04.2023\\linq 11.04.2023\\linq 11.04.2023\\1.csv";
            string path2 = "D:\\Programming\\C#\\Univer\\Linq 11.04.2023\\linq 11.04.2023\\linq 11.04.2023\\2.csv";
            string path3 = "D:\\Programming\\C#\\Univer\\Linq 11.04.2023\\linq 11.04.2023\\linq 11.04.2023\\3.csv";

            using (FileStream stream = File.OpenRead(path1))
            {
                System.IO.StreamReader file = new System.IO.StreamReader(@path1);
                while ((line = file.ReadLine()) != null)
                {
                    string[] arrtibutes = line.Split(',');
                    lstVehicles.Add(new TwoWheelVehicle(Convert.ToString(arrtibutes[0]),
                    Convert.ToDouble(arrtibutes[1]),
                    Convert.ToUInt32(arrtibutes[2]),
                    Convert.ToUInt32(arrtibutes[3])));
                }
            }

            using (FileStream stream = File.OpenRead(path2))
            {
                System.IO.StreamReader file = new System.IO.StreamReader(@path2);
                while ((line = file.ReadLine()) != null)
                {
                    string[] arrtibutes = line.Split(',');
                    lstVehicles.Add(new WithDieselEngine(
                        Convert.ToString(arrtibutes[0]),
                        Convert.ToDouble(arrtibutes[1]),
                        Convert.ToUInt32(arrtibutes[2]),
                        Convert.ToUInt32(arrtibutes[3]),
                        Convert.ToUInt32(arrtibutes[4])));
                }
            }

            using (FileStream stream = File.OpenRead(path3))
            {
                System.IO.StreamReader file = new System.IO.StreamReader(@path3);
                while ((line = file.ReadLine()) != null)
                {
                    string[] attributes = line.Split(',');
                    lstVehicles.Add(new WithElectroEngine(
                        Convert.ToString(attributes[0]),
                        Convert.ToDouble(attributes[1]),
                        Convert.ToUInt32(attributes[2]),
                        Convert.ToUInt32(attributes[3]),
                        Convert.ToUInt32(attributes[4])
                        ));
                }
            }


            Console.WriteLine("Task A\nUnsorted: ");
            foreach (var i in lstVehicles) { Console.WriteLine(i); }

            Console.WriteLine("\nSorted: \n");

            var sortedVehicles = from v in lstVehicles
                                 orderby v.Mark
                                 select v;
            foreach (var i in sortedVehicles) { Console.WriteLine(i); }


            Console.WriteLine("Task B");
            var powers = from v in lstVehicles
                         select v.EnginePower;

            double maxPow = powers.Max();

            var mostPower = from v in lstVehicles
                            where v.EnginePower == maxPow
                            select v;

            foreach (var item in mostPower) { Console.WriteLine(item); }





            Console.WriteLine("\nTask D");

            var weight = 80;

            var electroVehicles = from v in lstVehicles
                                  where v.MaxWeight <= weight
                                  orderby v.MaxWeight
                                  select v;

            foreach (var item in electroVehicles) { Console.WriteLine(item); }
        }
    }
    public class TwoWheelVehicle
    {
        public string Mark { get; set; }
        public double EnginePower { get; set; }
        public uint MaxSpeed { get; set; }
        public uint MaxWeight { get; set; }
        public TwoWheelVehicle(string mark, double ngnpwr, uint whlnmbr, uint wght)
        {
            Mark = mark;
            EnginePower = ngnpwr;
            MaxSpeed = whlnmbr;
            MaxWeight = wght;
        }
        public override string ToString()
        {
            return "Mark: " + Mark + " EnginePower: " + EnginePower + " MaxSpeed: " + MaxSpeed + " MaxWeight: " + MaxWeight;
        }
    }
    public class WithDieselEngine : TwoWheelVehicle
    {
        public uint EngineCapacity { get; set; }
        public WithDieselEngine(string mark, double ngnpwr, uint whlnmbr, uint wght, uint v) : base(mark, ngnpwr, whlnmbr, wght)
        {
            EngineCapacity = v;
        }
        public override string ToString()
        {
            return "Mark: " + Mark + " EnginePower: " + EnginePower + " MaxSpeed: " + MaxSpeed + " MaxWeight: " + MaxWeight + " EngineCapacity: " + EngineCapacity;
        }
    }
    public class WithElectroEngine : TwoWheelVehicle
    {
        public uint EngineVolume { get; set; }
        public WithElectroEngine(string mark, double ngnpwr, uint whlnmbr, uint wght, uint ngnvlm) : base(mark, ngnpwr, whlnmbr, wght)
        {
            EngineVolume = ngnvlm;
        }
        public override string ToString()
        {
            return "Mark: " + Mark + " EnginePower: " + EnginePower + " MaxSpeed: " + MaxSpeed + " MaxWeight: " + MaxWeight + " EngineVolume: " + EngineVolume;
        }
    }
    public class VehicleComparer<T> : IComparer<T> where T : TwoWheelVehicle
    {
        public int Compare(T x, T y)
        {
            return x.Mark.CompareTo(y.Mark);
        }
    }
    public class TruckComparer<T> : IComparer<T> where T : WithDieselEngine
    {
        public int Compare(T x, T y)
        {
            return x.MaxSpeed.CompareTo(y.MaxSpeed);
        }
    }
}