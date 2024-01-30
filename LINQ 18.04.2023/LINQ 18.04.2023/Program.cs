using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace Program
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            string line;

            var allDevices = new List<Computer>();
            var path1 = "D:\\Programming\\C#\\Univer\\LINQ 18.04.2023\\LINQ 18.04.2023\\computers.csv";

            using (FileStream stream = File.OpenRead(path1))
            {
                System.IO.StreamReader file = new System.IO.StreamReader(@path1);
                while ((line = file.ReadLine()) != null)
                {
                    string[] arrtibutes = line.Split(',');
                    allDevices.Add(new Computer(
                    Convert.ToUInt32(arrtibutes[0]),
                    Convert.ToString(arrtibutes[1]),
                    Convert.ToDouble(arrtibutes[2]),
                    Convert.ToBoolean(arrtibutes[3])
                    ));
                }
            }


            var path2 = "D:\\Programming\\C#\\Univer\\LINQ 18.04.2023\\LINQ 18.04.2023\\os.csv";
            var allOS = new List<OS>();

            using (FileStream stream = File.OpenRead(path2))
            {
                System.IO.StreamReader file = new System.IO.StreamReader(@path2);
                while ((line = file.ReadLine()) != null)
                {
                    string[] arrtibutes = line.Split(',');
                    allOS.Add(new OS(
                    Convert.ToUInt32(arrtibutes[0]),
                    Convert.ToString(arrtibutes[1]),
                    Convert.ToDouble(arrtibutes[2])
                    ));
                }
            }



            var path3 = "D:\\Programming\\C#\\Univer\\LINQ 18.04.2023\\LINQ 18.04.2023\\check.csv";
            var allChecks = new List<Check>();

            using (FileStream stream = File.OpenRead(path3))
            {
                System.IO.StreamReader file = new System.IO.StreamReader(@path3);
                while ((line = file.ReadLine()) != null)
                {
                    string[] attributes = line.Split(',');
                    allChecks.Add(new Check(
                    Convert.ToDateTime(attributes[0]),
                    Convert.ToUInt32(attributes[1]),
                    Convert.ToUInt32(attributes[2]),
                    Convert.ToUInt32(attributes[3])
                    ));
                }
            }

            var path4 = "D:\\Programming\\C#\\Univer\\LINQ 18.04.2023\\LINQ 18.04.2023\\result.csv";


            using (StreamWriter writer = new StreamWriter(path4))
            {
                foreach (var i in allDevices) { writer.WriteLine(i); };
                foreach (var i in allOS) { writer.WriteLine(i); };
                foreach (var i in allChecks) { writer.WriteLine(i); };
            }

            //Task A
            var summedPriceOfComputer = from check in allChecks
                                        let OsPrice = (from os in allOS
                                                       where os.Id == check.OsId
                                                       select os.Price).Sum()
                                        let ComputerPrice = (from comp in allDevices
                                                             where comp.Id == check.CompId
                                                             select comp.Price).Sum()
                                        select new
                                        {
                                            TotalPrice = check.Sold * (OsPrice + ComputerPrice)
                                        };
            var sum = summedPriceOfComputer;
            Console.WriteLine(sum);


            using (StreamWriter writer = new StreamWriter(path4))
            {
                foreach (var i in summedPriceOfComputer) { writer.WriteLine(i); };
            }


            ////Task B
            //var sumsOfMarks = from dev in allDevices
            //                  group dev by dev.Mark into g
            //                  select new
            //                  {
            //                      Mark = g.Key,
            //                      TotalPrice = g.Sum(dev => dev.price)
            //                  };

            //using (StreamWriter writer = new StreamWriter(path4))
            //{
            //    foreach (var i in sumsOfMarks) { writer.WriteLine($"Mark: {i.Mark} SummedPrice: {i.TotalPrice}"); };
            //}



            ////Task C
            //var servers = from dev in allDevices
            //              where dev.GetType() == typeof(Server)
            //              select (Server)dev;

            //var summedCapacity = from serv in servers
            //                     select new
            //                     {
            //                         Mark = serv.Mark,
            //                         TotalCapacity = serv.driveSize + serv.additionalDisk
            //                     };

            //using (StreamWriter writer = new StreamWriter(path4))
            //{
            //    foreach (var i in summedCapacity) { writer.WriteLine($"Mark: {i.Mark} SummedDrives: {i.TotalCapacity}"); };
            //}


            ////Task D
            //var workstations = from dev in allDevices
            //              where dev.GetType() == typeof(WorkStation)
            //              select (WorkStation)dev;

            //var theLongestDiagonal = from serv in servers
            //                     select new
            //                     {
            //                         Mark = serv.Mark,
            //                         TotalCapacity = serv.driveSize + serv.additionalDisk
            //                     };

            //using (StreamWriter writer = new StreamWriter(path4))
            //{
            //    foreach (var i in summedCapacity) { writer.WriteLine($"Mark: {i.Mark} SummedDrives: {i.TotalCapacity}"); };
            //}
        }

    }

    public class Computer
    {
        public uint Id { get; set; }
        public string Mark { get; set; }
        public double Price { get; set; }
        public bool HasOs { get; set; }
        public OS Os { get; set; }
        public Computer(uint d, string mrk, double prc, bool hss)
        {
            Id = d;
            Mark = mrk;
            Price = prc;
            HasOs = hss;
            if (!HasOs) { Os = new OS(0, "", 0.0); }
        }
        public override string ToString()
        {
            return "Id: " + Id + " Mark: " + Mark + " Price: " + Price + " HasOs: " + HasOs;
        }
    }
    public class OS
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public OS(uint d, string nm, double prc)
        {
            Id = d;
            Name = nm;
            Price = prc;
        }
        public override string ToString()
        {
            return "Id: " + Id + " Name: " + Name + " Price: " + Price;
        }
    }
    public class Check
    {
        public DateTime date { get; set; }
        public uint CompId { get; set; }
        public uint OsId { get; set; }
        public uint Sold { get; set; }
        public Check(DateTime dttm, uint cmpd, uint sd, uint sld)
        {
            date = dttm;
            CompId = cmpd;
            OsId = sd;
            Sold = sld;
        }

        public override string ToString()
        {
            return "Date: " + date + " CompId: " + CompId + " OsId: " + OsId + " Sold: " + Sold;
        }
    }
}

