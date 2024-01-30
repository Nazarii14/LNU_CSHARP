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

            var path1 = "D:\\Programming\\C#\\Univer\\25.04.2023 Practice\\25.04.2023 Practice\\patients.xml";
            var path2 = "D:\\Programming\\C#\\Univer\\25.04.2023 Practice\\25.04.2023 Practice\\favors.xml";
            var path3 = "D:\\Programming\\C#\\Univer\\25.04.2023 Practice\\25.04.2023 Practice\\checks.xml";


            using (var fs = new FileStream(path1, FileMode.Open))
            {
                var root = XElement.Load(fs);

                foreach (var element in root.Elements()) { Console.WriteLine(element); }
            }

            Console.WriteLine();
            using (var fs = new FileStream(path2, FileMode.Open))
            {
                var root = XElement.Load(fs);

                foreach (var element in root.Elements()) { Console.WriteLine(element); }
            }

            Console.WriteLine();
            using (var fs = new FileStream(path3, FileMode.Open))
            {
                var root = XElement.Load(fs);
                foreach (var element in root.Elements()) { Console.WriteLine(element); }


                var queryByDate = from serviceRecord in root.Elements()
                                  orderby (DateTime?)serviceRecord.Element("Date"), 
                                  (uint)serviceRecord.Element("PatId"), 
                                  (uint)serviceRecord.Element("FavId")
                                  select serviceRecord;

                foreach (var check in queryByDate) { Console.WriteLine(check); }
            }
        }

        public class Patient
        {
            public uint PatId { get; set; }
            public string Surname { get; set; }
            public string Name { get; set; }
            public uint BirthYear { get; set; }
            public Patient(uint patid, string srnm, string nm, uint brthyr)
            {
                PatId = patid;
                Surname = srnm;
                Name = nm;
                BirthYear = brthyr;
            }
            public override string ToString()
            {
                return "PatId: " + PatId + " Surname: " + Surname + " Name: " + Name + " BirthYear: " + BirthYear;
            }
        }
        public class Favor
        {
            public uint FavId { get; set; }
            public string Name { get; set; }
            public double FavPrice { get; set; }
            public Favor(uint d, string nm, double prc)
            {
                FavId = d;
                Name = nm;
                FavPrice = prc;
            }
            public override string ToString()
            {
                return "FavId: " + FavId + " Name: " + Name + " FavPrice: " + FavPrice;
            }
        }
        public class Check
        {
            public DateTime Date { get; set; }
            public uint IdPat { get; set; }
            public uint IdFav { get; set; }
            public Check(DateTime dttm, uint idpat, uint idfav)
            {
                Date = dttm;
                IdPat = idpat;
                IdFav = idfav;
            }
            public override string ToString()
            {
                return "Date: " + Date + " IdPat: " + IdPat + " IdFav: " + IdFav;
            }
        }
    }
}


