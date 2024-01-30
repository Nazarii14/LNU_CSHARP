using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Xml.Linq;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;

namespace ConsoleApp1
{
    class Program
    {
        public static void Main(string[] args)
        {
            //variant 2
            var tovar1 = new Dictionary<Tovar, int>
            {
                { new Tovar(1, "Korivka", "Solodoshchi", 10), 10 },
                { new Tovar(2, "Stozhary", "Solodoshchi", 16), 5 },
                { new Tovar(3, "Beton", "Building", 180), 1 },
                { new Tovar(4, "Tsement", "Building", 200), 15 },
                { new Tovar(5, "Mouse", "Electronics", 20), 17 },
                { new Tovar(6, "KeyBoard", "Electronics", 41), 28 }
            };

            var tovar2 = new Dictionary<Tovar, int>
            {
                { new Tovar(6, "KeyBoard", "Electronics", 41), 34 },
                { new Tovar(2, "Stozhary", "Solodoshchi", 16), 50 },
                { new Tovar(1, "Korivka", "Solodoshchi", 10), 17 },
                { new Tovar(4, "Tsement", "Building", 200), 5 },
                { new Tovar(5, "Mouse", "Electronics", 20), 72 },
                { new Tovar(3, "Beton", "Building", 180), 10 },
            };

            var categories = new List<Category>()
            {
                {new Category("#24", "Solodoshchi", 15)  },
                {new Category("#25", "Building", 25) },
                {new Category("#26", "Electronics", 20) }
            };


            //(а) для кожного вiдпущеного товару, заданого назвою, вказати загальну кiлькiсть проданих екземплярiв;
            //для такого перелiку застосувати лексико-графiчне впорядкування;

            var allTovar = new Dictionary<Tovar, int>();

            foreach (var item in tovar1) 
            {
                if (allTovar.ContainsKey(item.Key))
                {
                    allTovar[item.Key] += item.Value;
                }
                else
                {
                    allTovar.TryAdd(item.Key, item.Value);
                }
            }

            foreach (var item in tovar2)
            {
                if (allTovar.ContainsKey(item.Key))
                {
                    allTovar[item.Key] += item.Value;
                }
                else
                {
                    allTovar.TryAdd(item.Key, item.Value);
                }
            }
            Console.WriteLine("Task a");
            Console.WriteLine("Unsorted: ");
            foreach(var item in allTovar) { Console.WriteLine(item); }
            var sortedAllTovar = allTovar.OrderBy(x => x.Key, new TovarComparer<Tovar>());
            Console.WriteLine("Sorted: ");
            foreach (var item in sortedAllTovar) { Console.WriteLine(item); }
            Console.WriteLine("\n");


            //(б) для кожної категорiї товарiв, заданої назвою, вказати перелiк вiдпущених товарiв, в якому
            //для кожного з товарiв вказувати назву i сумарну вартiсть; категорiї впорядкувати лексико-графiчно, а
            //товари у спадному порядку стосовно суми;

            var task2 = new Dictionary<string, Dictionary<string, int>>();
            foreach (var item in categories)
            {
                if (task2.ContainsKey(item.CatName))
                {
                    var helpDict = new Dictionary<string, int>();

                    foreach (var i in allTovar)
                    {
                        if (helpDict.ContainsKey(i.Key.Name) && i.Key.Name == item.CatName)
                        {
                            helpDict[i.Key.Name] += i.Value;
                        }
                        else
                        {
                            helpDict[i.Key.Name] = i.Value;
                        }
                    }
                    task2[item.CatName] = helpDict;
                }
                else
                {
                    var helpDict = new Dictionary<string, int>();

                    foreach (var i in allTovar)
                    {
                        if (helpDict.ContainsKey(i.Key.Name) && i.Key.Name == item.CatName)
                        {
                            helpDict[i.Key.Name] += i.Value;
                        }
                        else
                        {
                            helpDict[i.Key.Name] = i.Value;
                        }
                    }
                    task2[item.CatName] = helpDict;
                }
            }

            Console.WriteLine("Task b");
            foreach(var item in task2) 
            {
                Console.WriteLine(item.Key); 
                foreach (var i in item.Value) { Console.WriteLine(i); }
            }

            //(в) перелiком категорiй у форматi < назва категорiї > , <загальна вартiсть вiдпущених товарiв>;
            //перелiк впорядкувати у спадному порядку за вартiстю.

        }
    }
    public class Tovar
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CatId { get; set; }
        public double Price { get; set; }

        public Tovar(int id, string nm, string ctId, double prc)
        {
            Id = id;
            Name = nm;
            CatId = ctId;
            Price = prc;
        }
        public override string ToString()
        {
            return $"{Id} {Name} {CatId} {Price}";
        }
    }
    public class Category
    {
        public string CatId { get; set; }
        public string CatName { get; set; }
        public uint Discount { get; set; }

        public Category(string id, string CtNm, uint ds)
        {
            CatId = id;
            CatName = CtNm;
            Discount = ds;
        }
        public override string ToString()
        {
            return $"{CatId} {CatName} {Discount}";
        }
    }
    public class TovarComparer<T> : IComparer<T> where T : Tovar
    {
        public int Compare(T x, T y)
        {
            return x.Name.CompareTo(y.Name);
        }
    }
    public class TovarPriceComparer<T> : IComparer<T> where T : Tovar
    {
        public int Compare(T x, T y)
        {
            return x.Price.CompareTo(y.Price);
        }
    }
}
