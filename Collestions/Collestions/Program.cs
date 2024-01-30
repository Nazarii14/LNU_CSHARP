using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Xml.Linq;
using System.ComponentModel.DataAnnotations;

namespace ConsoleApp1
{
    class Program
    {
        public static void Main(string[] args)
        {
            var dic = new Dictionary<string, string>();
            dic.Add("1str", "1vstr");
            dic.Add("2str", "2vstr");
            dic.Add("3str", "3vstr");
            dic.Add("4str", "4vstr");
            dic.Add("5str", "5vstr");
            dic.Add("6str", "6vstr");
            string str;
            dic.TryGetValue("6str", out str);
            //Console.WriteLine(str);

            dic.Remove("5str");
            //foreach (var item in dic) { Console.WriteLine(item); }
            var isTrue = dic.ContainsValue("6vstr");
            //foreach (var item in dic) { Console.WriteLine(item); }
            //Console.WriteLine(isTrue);

            var sortedDic = new SortedDictionary<int, string>();
            sortedDic.Add(1, "3");
            sortedDic.Add(15, "4");
            sortedDic.Add(5, "4");
           // sortedDic.Add(2, "4");
            sortedDic.Add(3, "4");
            sortedDic.Add(17, "8");
            sortedDic.Add(13, "4");

            //sortedDic.TryAdd(1, "14");
            //foreach(var item in sortedDic) { Console.WriteLine(item); }
            //Console.WriteLine(sortedDic.Count()); //number of elements
            //var uniqueValues = sortedDic.DistinctBy(x => x.Value);
            //Console.WriteLine(sortedDic.Max(x=>x.Key));
            //Console.WriteLine(sortedDic.Sum(x=>x.Key));


            var steak = new Stack<int>();
            steak.Push(1);
            steak.Push(7);
            steak.Push(8);
            steak.Push(6);
            steak.Push(4);
            //Console.WriteLine(steak.Contains(15));
            //Console.WriteLine(steak.Peek());
            //Console.WriteLine(steak.Sum());

            foreach(var item in steak) { Console.WriteLine(item);}

            Object k1 = new Object(1);
            Object k2 = new Object(2);
            Object k3 = new Object(3);
            Object k4 = new Object(4);
            Object k5 = new Object(5);
            Object k6 = new Object(6);

            var dicObj = new Dictionary<Object, string>();
            dicObj.TryAdd(k2, "s2");
            dicObj.TryAdd(k5, "s5");
            dicObj.TryAdd(k6, "s6");
            dicObj.TryAdd(k4, "s4");
            dicObj.TryAdd(k3, "s3");
            dicObj.TryAdd(k1, "s1");

            foreach (var item in dicObj) { Console.WriteLine(item); }
            Console.WriteLine();

            
            var sortedDict = dicObj.OrderBy(x => x.Key, new ObjectComparer<Object>());
            foreach (var item in sortedDict) { Console.WriteLine(item); }

        }
    }
    public class Object
    {
        public int Id { get; set; }
        public Object(int id)
        {
            Id = id;
        }
        public override string ToString()
        {
            return $"{Id}";
        }
    }
    public class ObjectComparer<T> : IComparer<T> where T : Object
    {
        public int Compare(T x, T y)
        {
            return x.Id.CompareTo(y.Id);
        }
    }
    //public class ObjectComparer : IComparer<KeyValuePair<Object, string>>
    //{
    //    public int Compare(KeyValuePair<Object, string> x, KeyValuePair<Object, string> y)
    //    {
    //        return x.Key.Id.CompareTo(y.Key.Id);
    //    }
    //}
}
