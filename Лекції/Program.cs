using System;
using System.Globalization;

namespace HelloWorld
{
    class Program
    {
        
        static void add(out int sum, params int[] val)
        {
            sum = 0;
            foreach (var i in val) sum += i;
        }
        static void Main(string[] args)
        {
            //0
            Console.WriteLine("Hello, World !");

            //1
            //var name = Console.ReadLine();            
            //Console.WriteLine($"Hello, {name} !");

            //2
            //char c = (char)Console.Read();
            //var b = (char)Console.Read();
            //var d = char.Parse(Console.ReadLine());
            //Console.WriteLine("c={0},  b={1}", c, b);
            //Console.WriteLine($"c={c},  b={b},  d={d}");

            //3
            //var i = int.Parse(Console.ReadLine());
            //Console.WriteLine($"i={i}");

            //4
            //var s = Console.ReadLine();
            //double x = double.Parse(s);
            //Console.WriteLine($"x={x}");
            //double y;
            //if (0 <= x && x < 2)
            //    y = Math.Cos(x) * Math.Cos(x);
            //else if (2 <= x && x < 10) y = 1 - Math.Sin(x * x); else y = 0;
            //Console.WriteLine($"x={x},  y={y.ToString("f5", NumberFormatInfo.InvariantInfo)}");
            //Console.WriteLine($"x={x},  y={y:f5}");

            //5
            //var date = DateTime.Now;
            //Console.WriteLine($"date={date}");
            //Console.WriteLine($"date={date:dd.MM, HH:MM}");

            //6
            //int[] a = { 1, 2, 3, 4, 5 };
            //var a = new int[]{ 1, 2, 3, 4, 5 };
            //foreach (var e in a)
            //    Console.WriteLine($"e={e}");
            //for (var i = 0; i < a.Length; ++i)
            //    Console.WriteLine($"a[{i}]={a[i]}");

            //7
            //int[,] b = { { 1, 2 }, { 3, 4 }, { 5, 6 } };
            //var bn = 2;
            //var bm = 3;
            //for (var i = 0; i < bm; ++i)
            //    for (var j = 0; j < bn; ++j)
            //        Console.WriteLine($"b[{i},{j}]={b[i, j]}");

            //8
            //var multiDim = new int[2, 3, 3];
            //for (uint i = 0; i < multiDim.GetLength(0); ++i)
            //    for (uint i = 0; i <= multiDim.GetUpperBound(0); ++i)
            //        for (uint j = 0; j <= multiDim.GetUpperBound(1); ++j)
            //            for (uint k = 0; k <= multiDim.GetUpperBound(2); ++k)
            //                multiDim[i, j, k] = (int)(i + j + k);
            //for (uint i = 0; i <= multiDim.GetUpperBound(0); ++i)
            //{
            //    Console.WriteLine($"i= {i}");
            //    for (uint j = 0; j <= multiDim.GetUpperBound(1); ++j)
            //    {
            //        Console.WriteLine($"j= {j}");
            //        for (uint k = 0; k <= multiDim.GetUpperBound(2); ++k)
            //        {
            //            Console.WriteLine($"k= {k}");
            //            Console.WriteLine("elements: {0}", multiDim[i, j, k]);
            //        }
            //    }
            //}
            //foreach (var e in multiDim)
            //    Console.WriteLine($"elements: {e}");

            //9
            //int s;
            //add(out s, 3, 5, 2, 9);
            //Console.WriteLine($"s : {s}");

            //10
            //var a = new int[] { 3, 5, 2, 9 };
            //add(out s, a);
            //Console.WriteLine($"s : {s}");
            //Console.WriteLine($"a : {a}");
        }
    }
}
