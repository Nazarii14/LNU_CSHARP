using System;
using System.Collections;
using System.Linq;
using System.Security.Cryptography;
using System.IO;

namespace Program
{
    class Program
    {
        public static void ReadTextByStreamReader(string path)
        {
            if (File.Exists(path)) File.Delete(path);
            using (var reader = new StreamReader(path))
            {
                string line;

                while ((line = reader.ReadLine()) != null) 
                {
                    Console.WriteLine(line);
                }
            }
        }
        public static void addTextByStreamWriter(string path)
        {
            if (File.Exists(path)) File.Delete(path);
            var fs = new FileStream(path, FileMode.CreateNew);

            using (var writer = new StreamWriter(fs))
            {
                writer.WriteLine("ttt");
                writer.Write("rrr");
            }
        }

        static void Main(string[] args)
        {
            //var lst = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //var evenLst = from i in lst
            //              where i % 2 == 0
            //              select i;

            //foreach (var i in evenLst) { Console.WriteLine(i); }

            //var students = new List<Student>() {
            //    new Student(2, "Yarema"),
            //    new Student(5, "Vika"),
            //    new Student(4, "Nastia"),
            //    new Student(3, "Sanya"),
            //    new Student(1, "Nazarii")
            //    };

            //var good_friends = from s in students
            //                   where s.Id > 3 || s.Name == "Nazarii"
            //                   select s;
            //foreach (var s in good_friends) { Console.WriteLine(s); }

            string[] words = { "the", "quick", "brown", "fox", "jumps" };
            var query = from word in words
                        orderby word.Length ascending, word.Substring(0, 1) descending
                        select word;

            foreach (var i in query) { Console.WriteLine(i); }

        }
    }
    class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Student(int id, string name) 
        {
            Id = id;
            Name = name;
        }
        public override string ToString()
        {
            return Id + " " + Name;
        }
    }
}