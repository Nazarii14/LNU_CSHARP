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
            var pgen1 = new PointsGenerator();
            Console.WriteLine(pgen1);


        }
        public class PointsGenerator
        {
            public int X { get; set; }
            public int Y { get; set; }
            public List<KeyValuePair<int, int>> Points { get; set; }

            public PointsGenerator()
            {
                Random rand = new Random();
             
                X = rand.Next(-100, 100);
                Y = rand.Next(-100, 100);
                


                Points.Add(KeyValuePair.Create(X, Y));
            }
            public void PrintList()
            {
                foreach (var p in Points) { Console.WriteLine(Convert.ToString(p.Key) + ", " + Convert.ToString(p.Value)); }
            }
        }

        //public class PointsEventArgs : EventArgs
        //{
        //    public PointsEventArgs()
        //}
        //public abstract class Point
        //{
        //    public int X { get; set; }
        //    public int Y { get; set; }
        //    public event EventHandler<PointsEventArgs> PointChanged;

        //    public Point(int x, int y)
        //    {
        //        X = x;
        //        Y = y;
        //    }

        //}
        //protected virtual void onPointChanged()
    }
}


