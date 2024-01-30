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

            var path1 = "D:\\Programming\\C#\\Univer\\Personal Work 25.04.2023\\Personal Work 25.04.2023\\tasks.xml";
            var path2 = "D:\\Programming\\C#\\Univer\\Personal Work 25.04.2023\\Personal Work 25.04.2023\\students.xml";
            var path3 = "D:\\Programming\\C#\\Univer\\Personal Work 25.04.2023\\Personal Work 25.04.2023\\results.xml";

            var allTasks = new List<XElement>();
            var allStudents = new List<XElement>();
            var allResults = new List<XElement>();


            using (var fs = new FileStream(path1, FileMode.Open))
            {
                var root = XElement.Load(fs);
                allTasks.Add(root);
            }

            using (var fs = new FileStream(path2, FileMode.Open))
            {
                var root = XElement.Load(fs);
                allStudents.Add(root);
            }

            using (var fs = new FileStream(path3, FileMode.Open))
            {
                var root = XElement.Load(fs);
                allResults.Add(root);
            }

            var taskA = from stud in allStudents
                        join res in allResults on stud.Element("StudentId").Value equals res.Element("StudId").Value
                        join task in allTasks on res.Element("TskId").Value equals task.Element("TaskId").Value
                        group new
                        {
                            Group = stud.Element("Group").Value,
                            Surname = stud.Element("Surname").Value,
                            Topic = task.Element("Topic").Value,
                            Date = res.Element("ReviewDate").Value
                        } 
                        by stud.Element("Group").Value into gr
                        orderby gr.Key, gr.Select(x => x.Surname)
                        select gr;


            foreach (var el in taskA) { Console.WriteLine(el); }
                        
                        










        }

        public class Task
        {
            public uint Id { get; set; }
            public string Topic { get; set; }
            public DateTime Date { get; set; }
            public Task(uint patid, string tpc, DateTime dt)
            {
                Id = patid;
                Topic = tpc;
                Date = dt;
            }
            public override string ToString()
            {
                return "PatId: " + Id + " Topic: " + Topic + " Date: " + Date;
            }
        }
        public class Student
        {
            public uint StudentId { get; set; }
            public string Surname { get; set; }
            public string Name { get; set; }
            public string Group { get; set; }
            public Student(uint stdid, string srnm, string nm, string grp)
            {
                StudentId = stdid;
                Surname = srnm;
                Name = nm;
                Group = grp;
            }
            public override string ToString()
            {
                return "StudentId: " + StudentId + " Surname: " + Surname + " Name: " + Name + " Group: " + Group;
            }
        }
        public class Result
        {
            public uint TaskId { get; set; }
            public uint StudentId { get; set; }
            public uint Mark { get; set; }
            public DateTime ReviewDate { get; set; }
            public Result(uint tskid, uint stdid, uint mrk, DateTime rvwdt)
            {
                TaskId = tskid;
                StudentId = stdid;
                Mark = mrk;
                ReviewDate = rvwdt;
            }
            public override string ToString()
            {
                return "TaskId: " + TaskId + " StudentId: " + StudentId + " Mark: " + Mark + " ReviewDate: " + ReviewDate;
            }
        }
    }
}


