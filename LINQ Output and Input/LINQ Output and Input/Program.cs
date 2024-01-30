using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;

namespace Program
{
    public static class Program
    {
        public static void WriteSimpleAndReverseTransformation(List<double> lst, string path)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.WriteLine("Write To File!");
                foreach (var i in lst) { writer.WriteLine(i); };
            }
        }
        public static void Main(string[] args)
        {
            string line;

            var allDevices = new List<Computer>();
            var path1 = "D:\\Programming\\C#\\Univer\\LINQ Output and Input\\LINQ Output and Input\\computers.csv";

            using (FileStream stream = File.OpenRead(path1))
            {
                System.IO.StreamReader file = new System.IO.StreamReader(@path1);
                while ((line = file.ReadLine()) != null)
                {
                    string[] arrtibutes = line.Split(',');
                    allDevices.Add(new Computer(Convert.ToString(arrtibutes[0]),
                    Convert.ToUInt32(arrtibutes[1]),
                    Convert.ToUInt32(arrtibutes[2]),
                    Convert.ToUInt32(arrtibutes[3]),
                    Convert.ToUInt32(arrtibutes[4])
                    ));
                }
            }


            var path2 = "D:\\Programming\\C#\\Univer\\LINQ Output and Input\\LINQ Output and Input\\servers.csv";

            using (FileStream stream = File.OpenRead(path2))
            {
                System.IO.StreamReader file = new System.IO.StreamReader(@path2);
                while ((line = file.ReadLine()) != null)
                {
                    string[] arrtibutes = line.Split(',');
                    allDevices.Add(new Server(Convert.ToString(arrtibutes[0]),
                    Convert.ToUInt32(arrtibutes[1]),
                    Convert.ToUInt32(arrtibutes[2]),
                    Convert.ToUInt32(arrtibutes[3]),
                    Convert.ToUInt32(arrtibutes[4]),
                    Convert.ToUInt32(arrtibutes[5])
                    ));
                }
            }



            var path3 = "D:\\Programming\\C#\\Univer\\LINQ Output and Input\\LINQ Output and Input\\workstations.csv";

            using (FileStream stream = File.OpenRead(path3))
            {
                System.IO.StreamReader file = new System.IO.StreamReader(@path3);
                while ((line = file.ReadLine()) != null)
                {
                    string[] arrtibutes = line.Split(',');
                    allDevices.Add(new WorkStation(Convert.ToString(arrtibutes[0]),
                    Convert.ToUInt32(arrtibutes[1]),
                    Convert.ToUInt32(arrtibutes[2]),
                    Convert.ToUInt32(arrtibutes[3]),
                    Convert.ToUInt32(arrtibutes[4]),
                    Convert.ToString(arrtibutes[5]),
                    Convert.ToUInt32(arrtibutes[6])
                    ));
                }
            }

            var path4 = "D:\\Programming\\C#\\Univer\\LINQ Output and Input\\LINQ Output and Input\\result.csv";



            //Task A
            var sortedDevices = from dev in allDevices
                                orderby dev.Mark
                                select dev;

            using (StreamWriter writer = new StreamWriter(path4))
            {
                foreach (var i in sortedDevices) { writer.WriteLine(i); };
            }


            //Task B
            var sumsOfMarks = from dev in allDevices
                              group dev by dev.Mark into g
                              select new
                              {
                                  Mark = g.Key,
                                  TotalPrice = g.Sum(dev => dev.price)
                              };

            using (StreamWriter writer = new StreamWriter(path4))
            {
                foreach (var i in sumsOfMarks) { writer.WriteLine($"Mark: {i.Mark} SummedPrice: {i.TotalPrice}"); };
            }



            //Task C
            var servers = from dev in allDevices
                          where dev.GetType() == typeof(Server)
                          select (Server)dev;

            var summedCapacity = from serv in servers
                                 select new
                                 {
                                     Mark = serv.Mark,
                                     TotalCapacity = serv.driveSize + serv.additionalDisk
                                 };
         
            using (StreamWriter writer = new StreamWriter(path4))
            {
                foreach (var i in summedCapacity) { writer.WriteLine($"Mark: {i.Mark} SummedDrives: {i.TotalCapacity}"); };
            }


            //Task D
            var workstations = from dev in allDevices
                          where dev.GetType() == typeof(WorkStation)
                          select (WorkStation)dev;

            var theLongestDiagonal = from serv in servers
                                 select new
                                 {
                                     Mark = serv.Mark,
                                     TotalCapacity = serv.driveSize + serv.additionalDisk
                                 };

            using (StreamWriter writer = new StreamWriter(path4))
            {
                foreach (var i in summedCapacity) { writer.WriteLine($"Mark: {i.Mark} SummedDrives: {i.TotalCapacity}"); };
            }
        }

    }

    public class Computer
    {
        public string Mark { get; set; }
        public uint ProcessorSpeed { get; set; }
        public uint ramSize { get; set; }
        public uint driveSize { get; set; }
        public double price { get; set; }
        public Computer(string mrk, uint prcsrSpd, uint rmSz, uint drvSz, double prc)
        {
            Mark = mrk;
            ProcessorSpeed = prcsrSpd;
            ramSize = rmSz;
            driveSize = drvSz;
            price = prc;
        }
        public override string ToString()
        {
            return "Mark: " + Mark + " ProcSpeed: " + ProcessorSpeed + " RamSize: " + ramSize + " DriveSize: " + driveSize + " Price: " + price;
        }
    }
    public class Server : Computer
    {
        public uint additionalDisk { get; set; }
        public Server(string mrk, uint prcsrSpd, uint rmSz, uint drvSz, double prc, uint addDisk) : base(mrk, prcsrSpd, rmSz, drvSz, prc)
        {
            additionalDisk = addDisk;
        }
        public override string ToString()
        {
            return "Mark: " + Mark + " ProcSpeed: " + ProcessorSpeed + " RamSize: " + ramSize + " DriveSize: " + driveSize + " Price: " + price + " AdditionalDisk: " + additionalDisk;
        }
    }
    public class WorkStation : Computer
    {
        public string workStationMark { get; set; }
        public uint diagonal { get; set; }
        public WorkStation(string mrk, uint prcsrSpd, uint rmSz, uint drvSz, double prc, string wrkStMrk, uint dgnl) : base(mrk, prcsrSpd, rmSz, drvSz, prc)
        {
            workStationMark = wrkStMrk;
            diagonal = dgnl;
        }
        public override string ToString()
        {
            return "Mark: " + Mark + " ProcSpeed: " + ProcessorSpeed + " RamSize: " + ramSize + " DriveSize: " + driveSize + " Price: " + price + " WorkStMark: " + workStationMark + " Diagonal: " + diagonal;
        }
    }
}

