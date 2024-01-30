using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Program
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var path_workers = "D:\\Programming\\C#\\Univer\\Templates\\Personal Work 23.05.2023\\Personal Work 23.05.2023\\worker.xml";
            var path_project = "D:\\Programming\\C#\\Univer\\Templates\\Personal Work 23.05.2023\\Personal Work 23.05.2023\\project.xml";
            var path_posada = "D:\\Programming\\C#\\Univer\\Templates\\Personal Work 23.05.2023\\Personal Work 23.05.2023\\posada.xml";
            var path_checks = "D:\\Programming\\C#\\Univer\\Templates\\Personal Work 23.05.2023\\Personal Work 23.05.2023\\checks.xml";

            var workers = XElement.Load(path_workers);
            var projects = XElement.Load(path_project);
            var posadas = XElement.Load(path_posada);
            var checks = XElement.Load(path_checks);


            var all_projects = projects.Elements("Project").Select(w => new
            {
                ProjectId = (int)w.Element("ProjectId"),
                ProjectName = (string)w.Element("ProjectName")
            });

            var all_posadas = posadas.Elements("Posada").Select(p => new
            {
                PosadaId = (int)p.Element("PosadaId"),
                PosadaName = (string)p.Element("Name"),
                PosadaPrice = (uint)p.Element("Price")
            });

            var all_workers = workers.Elements("Worker").Select(w => new
            {
                WorkerId = (int)w.Element("WorkerId"),
                WorkerName = (string)w.Element("Name"),
                WorkerSurname = (string)w.Element("Surname"),
                WorkerPosadaId = (int)w.Element("PosadaId"),
                Pidrozdil = (string)w.Element("Pidrozdil")
            });

            var all_checks = checks.Elements("Check").Select(w => new
            {
                Date = (DateTime)w.Element("Date"),
                WorkerId = (int)w.Element("WorkerId"),
                ProjectId = (int)w.Element("ProjectId"),
                WorkedHours = (uint)w.Element("Hours"),
                Zmist = (string)w.Element("Zmist")
            });


            //task a
            var task_a = from check in all_checks
                         join project in all_projects on check.ProjectId equals project.ProjectId
                         join worker in all_workers on check.WorkerId equals worker.WorkerId
                         orderby project.ProjectName, worker.WorkerSurname
                         group check by new { project.ProjectName } into g
                         select new
                         {
                             ProjectName = g.Key.ProjectName,
                             Workers = g.Select(g => new { Worker = all_workers.FirstOrDefault(s => s.WorkerId == g.WorkerId), g.WorkedHours })
                         };
            //xml-файл, де звiти систематизованi за схемою
            //<назва проєкту, перелiк прiзвищ(з iнiцiалами) працiвникiв разом iз сумарною кiлькiстю годин,
            //вiдпрацьованих кожним з них>;
            //вмiст впорядкувати у лексико-графiчному порядку за назвою проєкту i прiзвищем працiвникiв;

            var XTask_a = new XElement("TaskA",
                from item in task_a
                select new XElement("Project",
                new XElement("ProjectName", item.ProjectName),
                from w in item.Workers
                select new XElement("Worker",
                new XElement("Name", w.Worker.WorkerName),
                new XElement("Surname", w.Worker.WorkerSurname),
                new XElement("WorkedHours", w.WorkedHours)
                )));

            XTask_a.Save("D:\\Programming\\C#\\Univer\\Templates\\Personal Work 23.05.2023\\Personal Work 23.05.2023\\task_a.xml");
            

            //task b

            var queryForB = from check in all_checks
                            join project in all_projects on check.ProjectId equals project.ProjectId
                            join worker in all_workers on check.WorkerId equals worker.WorkerId
                            join posada in all_posadas on worker.WorkerPosadaId equals posada.PosadaId
                            select new
                            {
                                ProjName = project.ProjectName,
                                TotalPrice = posada.PosadaPrice * check.WorkedHours,
                                FullName = worker.WorkerName + " " + worker.WorkerSurname,
                                WorkerId = worker.WorkerId + " " + worker.WorkerId,
                                Hours = check.WorkedHours
                            };

            //xml-файл, описаний у попередньому завданнi, але подати крiм
            //вiдпрацьованих годин ще й зароблену суму грошей;

            var task_b = from item in queryForB
                         group item by new { item.ProjName } into gr
                         select new
                         {
                             ProjectName = gr.Key.ProjName,
                             Workers = gr.Select(g => new { 
                                 Worker = all_workers.FirstOrDefault(s => s.WorkerId == s.WorkerId),
                                 g.TotalPrice, g.Hours
                             })
                         };


            var XTask_b = new XElement("TaskB",                 //<TaskB>
                from item in task_b
                select new XElement("Project",                      //<Project>
                new XElement("ProjectName", item.ProjectName),         //<ProjectName>
                from w in item.Workers
                select new XElement("Worker",
                    new XElement("Name", w.Worker.WorkerName),
                    new XElement("Surname", w.Worker.WorkerSurname),
                    new XElement("WorkedHours", w.Hours),
                    new XElement("TotalPrice", w.TotalPrice)
                    )
                )
            );

            XTask_b.Save("D:\\Programming\\C#\\Univer\\Templates\\Personal Work 23.05.2023\\Personal Work 23.05.2023\\task_b.xml");



            var taskC = from check in all_checks
                        join project in all_projects on check.ProjectId equals project.ProjectId
                        join worker in all_workers on check.WorkerId equals worker.WorkerId
                        join posada in all_posadas on worker.WorkerPosadaId equals posada.PosadaId
                        select new
                        {
                            WorkerName = worker.WorkerName,
                            ProjectId = project.ProjectId,
                            ProjectName = project.ProjectName,
                            Hours = check.WorkedHours,
                            Seller = posada.PosadaPrice,
                            Position = posada.PosadaName
                        };

            //для кожного проєкту (заданого iдентифiкатором)
            //вказати сумарний час роботи над ним працiвниками вiдповiдних посад;
            //вмiст впорядкувати у лексико-графiчному порядку за iдентифiкатором проєкту;

            var forTaskC = new XElement("TaskC",
                                                from i in taskC
                                                group i by i.ProjectId into p
                                                orderby p.Key
                                                select new XElement("project", new XAttribute("id", p.Key),
                                                    from j in p
                                                    group j by j.Position into w
                                                    orderby w.Key
                                                    select new XElement("position", new XAttribute("name", w.Key),
                                                       new XElement("hours", w.Sum(k => k.Hours))
                                                    )
                                                )
                                            );

            forTaskC.Save("D:\\Programming\\C#\\Univer\\Templates\\Personal Work 23.05.2023\\Personal Work 23.05.2023\\task_c.xml");


            //d
            var forTaskD = new XElement("forTaskD",
                from i in taskC
                group i by i.ProjectName into p
                orderby p.Key
                select new XElement("project", 
                new XAttribute("name", p.Key),
                    new XElement("hours", p.Sum(k => k.Hours)),
                    new XElement("total_seller", p.Sum(k => k.Seller * k.Hours))
                )
            );

            //для кожного проєкту (заданого iдентифiкатором) вказати сумарний час роботи
            //над ним i освоєну суму грошей; цi результати впорядкувати за сумарним часом у
            //спадному порядку.

            forTaskD.Save("D:\\Programming\\C#\\Univer\\Templates\\Personal Work 23.05.2023\\Personal Work 23.05.2023\\task_d.xml");
        }
    }
}
