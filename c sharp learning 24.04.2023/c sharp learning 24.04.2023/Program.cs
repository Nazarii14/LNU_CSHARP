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
using static Program.Program;

namespace Program
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            string line;

            var path1 = "computers.csv";
            var path2 = "os.csv";
            var path3 = "checks.csv";

            var allComputers = new List<Computer>();
            var allOS = new List<OS>();
            var allChecks = new List<Check>();

            using (File.OpenRead(path1))
            {
                var file = new System.IO.StreamReader(path1);
                while ((line = file.ReadLine()) != null)
                {
                    string[] arrtibutes = line.Split(',');
                    allComputers.Add(new Computer(
                    Convert.ToUInt32(arrtibutes[0]),
                    Convert.ToString(arrtibutes[1]),
                    Convert.ToDouble(arrtibutes[2])
                    ));
                }
            }


            using (File.OpenRead(path2))
            {
                var file = new System.IO.StreamReader(path2);
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


            using (File.OpenRead(path3))
            {
                var file = new System.IO.StreamReader(path3);
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


            Console.WriteLine("Task A");

            var genSum = (from ch in allChecks
                          join c in allComputers on ch.CompId equals c.Id
                          join s in allOS on ch.OsId equals s.IdOs
                          select (int)(c.Price + s.OSPrice) * ch.Sold).Sum();

            Console.WriteLine($"General price = {genSum}");


            Console.WriteLine("Task B");

            var soldComputersByMark = from computer in allComputers
                                      join check in allChecks on computer.Id equals check.CompId
                                      group check by computer.Mark into markGroup
                                      select new { Mark = markGroup.Key, SoldQuantity = markGroup.Sum(c => c.Sold) };

            foreach (var soldcomps in soldComputersByMark) { Console.WriteLine(soldcomps); }


            Console.WriteLine("Task C");

            var soldPerEachDay = from ch in allChecks
                                 join comp in allComputers on ch.CompId equals comp.Id
                                 join os in allOS on ch.OsId equals os.IdOs
                                 group new { comp.Price, ch.Sold, os.OSPrice } by ch.Date into dayGroup
                                 select new { Date = dayGroup.Key, TotalPrice = dayGroup.Sum(c => (c.Price + c.OSPrice) * c.Sold) };

            foreach (var i in soldPerEachDay) { Console.WriteLine(i); }


            Console.WriteLine("Task D");

            var soldByOS = from os in allOS
                           join ch in allChecks on os.IdOs equals ch.OsId
                           join comp in allComputers on ch.CompId equals comp.Id
                           group new { os.IdOs, comp.Price, ch.Sold, os.OSPrice } by os.IdOs into groupedOS
                           select new { OsID = groupedOS.Key, TotalPrice = groupedOS.Sum(c => (c.Price + c.OSPrice) * c.Sold) };
                           
            foreach(var i in soldByOS) { Console.WriteLine(i); }
        }

        public class Computer
        {
            public uint Id { get; set; }
            public string Mark { get; set; }
            public double Price { get; set; }
            public bool IdOs { get; set; }
            public Computer(uint d, string mrk, double prc)
            {
                Id = d;
                Mark = mrk;
                Price = prc;
            }
            public override string ToString()
            {
                return "Id: " + Id + " Mark: " + Mark + " Price: " + Price + " IdOs: " + IdOs;
            }
        }
        public class OS
        {
            public uint IdOs { get; set; }
            public string Name { get; set; }
            public double OSPrice { get; set; }
            public OS(uint d, string nm, double prc)
            {
                IdOs = d;
                Name = nm;
                OSPrice = prc;
            }
            public override string ToString()
            {
                return "Id: " + IdOs + " Name: " + Name + " Price: " + OSPrice;
            }
        }
        public class Check
        {
            public DateTime Date { get; set; }
            public uint CompId { get; set; }
            public uint OsId { get; set; }
            public uint Sold { get; set; }
            public Check(DateTime dttm, uint cmpd, uint sd, uint sld)
            {
                Date = dttm;
                CompId = cmpd;
                OsId = sd;
                Sold = sld;
            }
            public override string ToString()
            {
                return "Date: " + Date + " CompId: " + CompId + " OsId: " + OsId + " Sold: " + Sold;
            }
        }
    }
}


