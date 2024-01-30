using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Program
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var pathChecks = "D:\\Programming\\C#\\Univer\\Templates\\Own work preparation\\Own work preparation\\check.xml";
            var pathWorkers = "D:\\Programming\\C#\\Univer\\Templates\\Own work preparation\\Own work preparation\\worker.xml";
            var pathProjects = "D:\\Programming\\C#\\Univer\\Templates\\Own work preparation\\Own work preparation\\project.xml";
            var pathPosadas = "D:\\Programming\\C#\\Univer\\Templates\\Own work preparation\\Own work preparation\\posada.xml";

            var checks = XElement.Load(pathChecks);
            var workers = XElement.Load(pathWorkers);
            var posadas = XElement.Load(pathPosadas);
            var projects = XElement.Load(pathProjects);

            var all_projects = projects.Elements("Project").Select(x => new
            {
                ProjectId = (int)x.Element("ProjectId"),
                ProjectName = (string)x.Element("ProjectName")
            });

            var all_workers = workers.Elements("Worker").Select(x => new
            {
                WorkerId = (int)x.Element("WorkerId"),
                Name = (string)x.Element("Name"),
                Surname = (string)x.Element("Surname"),
                PosadaId = (int)x.Element("PosadaId"),
                Pidrozdil = (string)x.Element("Pidrozdil")
            });

            var all_posadas = posadas.Elements("Posada").Select(x => new
            {
                PosadaId = (int)x.Element("PosadaId"),
                Name = (string)x.Element("Name"),
                Price = (int)x.Element("Price")
            });

            var all_checks = checks.Elements("Check").Select(x => new
            {
                Date = (DateTime)x.Element("Date"),
                WorkerId = (int)x.Element("WorkerId"),
                ProjectId = (int)x.Element("ProjectId"),
                Hours = (int)x.Element("Hours"),
                Zmist = (string)x.Element("Zmist")
            });

            //foreach(var i in all_checks) { Console.WriteLine(i); }
            //foreach(var i in all_posadas) { Console.WriteLine(i); }
            //foreach(var i in all_projects) { Console.WriteLine(i); }
            //foreach(var i in all_workers) { Console.WriteLine(i); }


            var queryForA = from proj in all_projects
                            join check in all_checks on proj.ProjectId equals check.ProjectId
                            join worker in all_workers on check.WorkerId equals worker.WorkerId
                            join posada in all_posadas on worker.PosadaId equals posada.PosadaId
                            group check by new { proj.ProjectName } into g
                            select new
                            {
                                ProjectName = g.Key,
                                Workers = g.Select(w => new { Worker = all_workers.FirstOrDefault(s => s.WorkerId == w.WorkerId), w.Hours })
                            };

            var XTask_A = new XElement("TaskA",
                from item in queryForA
                select new XElement("Project",
                new XElement("ProjectName", item.ProjectName),
                from w in item.Workers
                select new XElement("Worker",
                new XElement("Surname", w.Worker.Surname),
                new XElement("Name", w.Worker.Name),
                new XElement("Hours", w.Hours))));

            XTask_A.Save("D:\\Programming\\C#\\Univer\\Templates\\Own work preparation\\Own work preparation\\task_a.xml");


            //де звiти систематизованi за схемою
            //<назва проєкту, перелiк прiзвищ (з iнiцiалами) працiвникiв разом iз сумарною кiлькiстю годин,
            //вiдпрацьованих кожним з них>;


            var queryForB = from proj in all_projects
                            join check in all_checks on proj.ProjectId equals check.ProjectId
                            join worker in all_workers on check.WorkerId equals worker.WorkerId
                            join posada in all_posadas on worker.PosadaId equals posada.PosadaId
                            select new
                            {
                                ProjName = proj.ProjectName,
                                TotalPrice = posada.Price * check.Hours,
                                FullName = worker.Surname + " " + worker.Name[0] + ".",
                                WorkerId = worker.WorkerId,
                                WorkedHours = check.Hours
                            };

            var task_b = from item in queryForB
                         group item by new { item.ProjName } into gr
                         select new
                         {
                             ProjName = gr.Key,
                             Workers = gr.Select(w => new { Worker = all_workers.FirstOrDefault(s => s.WorkerId == w.WorkerId), w.WorkedHours, w.TotalPrice })
                         };

            //xml-файл, описаний у попередньому завданнi, але подати
            //крiм вiдпрацьованих годин ще й зароблену суму грошей;

            var XTask_B = new XElement("TaskB",
                from item in task_b
                select new XElement("Project",
                new XElement("ProjectName", item.ProjName),
                from w in item.Workers
                select new XElement("Worker",
                new XElement("Name", w.Worker.Name),
                new XElement("Surname", w.Worker.Surname),
                new XElement("Hours", w.WorkedHours),
                new XElement("TotalPrice", w.TotalPrice)
                )));

            XTask_B.Save("D:\\Programming\\C#\\Univer\\Templates\\Own work preparation\\Own work preparation\\task_b.xml");






            var queryForC = from proj in all_projects
                            join check in all_checks on proj.ProjectId equals check.ProjectId
                            join worker in all_workers on check.WorkerId equals worker.WorkerId
                            join posada in all_posadas on worker.PosadaId equals posada.PosadaId
                            select new
                            {
                                ProjId = proj.ProjectId,
                                Hours = check.Hours,
                                ProjectName = proj.ProjectName,
                                PosadaPrice = posada.Price,
                                Position = posada.Name,
                                WorkerId = worker.WorkerId,
                            };

            //для кожного проєкту (заданого iдентифiкатором)
            //вказати сумарний час роботи над ним працiвниками вiдповiдних посад;
            //вмiст впорядкувати у лексико-графiчному порядку за iдентифiкатором проєкту;

            var XTask_C = new XElement("TaskC",
                             from item in queryForC
                             group item by item.ProjId into gr
                             orderby gr.Key
                             select new XElement("Project", new XAttribute("ProjId", gr.Key),
                                 from j in gr
                                 group j by j.Position into w
                                 orderby w.Key
                                 select 
                                    new XElement("Position", new XAttribute("Name", w.Key),
                                    new XElement("Hours", w.Sum(k => k.Hours))
                                        )
                                    )
                                );


            XTask_C.Save("D:\\Programming\\C#\\Univer\\Templates\\Own work preparation\\Own work preparation\\task_c.xml");

            //для кожного проєкту (заданого iдентифiкатором) вказати сумарний час роботи
            //над ним i освоєну суму грошей; цi результати впорядкувати за сумарним часом у
            //спадному порядку.

            var XTask_D = new XElement("TaskD",
                             from item in queryForC
                             group item by item.ProjId into gr
                             orderby gr.Key
                             select new XElement("Project", new XAttribute("ProjId", gr.Key),
                                    new XElement("Hours", gr.Sum(k => k.Hours),
                                    new XElement("TotalPriceEarned", gr.Sum(k => k.Hours * k.PosadaPrice)))));


            XTask_D.Save("D:\\Programming\\C#\\Univer\\Templates\\Own work preparation\\Own work preparation\\task_d.xml");
        }
    }
}