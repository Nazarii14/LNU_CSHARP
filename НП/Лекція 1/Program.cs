using System;
using System.Globalization;
using System.Linq;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] a = {0, 0, 0, 0, 0};
            for(int i = 0; i < 5; i++)
            {
                string s = Console.ReadLine();
                a[i] = Convert.ToInt32(s);
            }
            Console.WriteLine(a.Where(i => i % 2 == 0).Count());

            
        }
    }
}
