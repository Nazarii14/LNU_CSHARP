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
        public static int SumOfTwoMaxElements(int[] array)
        {
            array = array.OrderBy(x => x).ToArray();
            return array[array.Length - 1] + array[array.Length - 2];
        }
        public static void Main(string[] args)
        {
            int[] arr = { 1, 5, 3 };
            Console.WriteLine(SumOfTwoMaxElements(arr));
        }


    }
}


