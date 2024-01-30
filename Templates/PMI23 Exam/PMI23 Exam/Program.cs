using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Program
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var path_zapysy = "D:\\Programming\\C#\\Univer\\Templates\\PMI23 Exam\\PMI23 Exam\\zapysy.xml";
            var path_posv = "D:\\Programming\\C#\\Univer\\Templates\\PMI23 Exam\\PMI23 Exam\\posvidchennya.xml";
            var path_pract = "D:\\Programming\\C#\\Univer\\Templates\\PMI23 Exam\\PMI23 Exam\\pract.xml";
            var path_theory = "D:\\Programming\\C#\\Univer\\Templates\\PMI23 Exam\\PMI23 Exam\\theory.xml";

            var zapysy = XElement.Load(path_zapysy);
            var posvs = XElement.Load(path_posv);
            var pract = XElement.Load(path_pract);
            var theory = XElement.Load(path_theory);

            var all_zapysy = zapysy.Elements("Zapys").Select(w => new
            {
                PosvId = (int)w.Element("PosvId"),
                Date = (DateTime)w.Element("Date"),
                TheoryId = (string)w.Element("TheoryId"),
                PractId = (string)w.Element("PractId")
            });

            var all_posvs = posvs.Elements("Posv").Select(p => new
            {
                PosvId = (int)p.Element("PosvId"),
                DriverSurname = (string)p.Element("DriverSurname"),
                LicenseCat = (string)p.Element("LicenseCat"),
                LicenseDate = (DateTime)p.Element("LicenseDate")
            });

            var all_practs = pract.Elements("Pr").Select(w => new
            {
                PrId = (int)w.Element("PrId"),
                CarMark = (string)w.Element("CarMark"),
                Mark = (int)w.Element("Mark")
            });

            var all_theory = theory.Elements("Th").Select(w => new
            {
                ThId = (int)w.Element("ThId"),
                Question = (string)w.Element("Question"),
                Mark = (int)w.Element("Mark")
            });


            //task A

            var queryForTaskA = from zap in all_zapysy
                                join posv in all_posvs on zap.PosvId equals posv.PosvId
                                orderby posv.DriverSurname
                                select new
                                {
                                    DriverSurname = posv.DriverSurname,
                                    LicenseCat = posv.LicenseCat,
                                    Termin = (posv.LicenseDate - zap.Date).TotalDays
                                };

            var XTask_a = new XElement("TaskA",
                from item in queryForTaskA
                select new XElement("Driver",
                new XElement("DriverSurname", item.DriverSurname),
                new XElement("LicenseCat", item.LicenseCat),
                new XElement("StartDate", item.Termin)
                ));

            XTask_a.Save("D:\\Programming\\C#\\Univer\\Templates\\PMI23 Exam\\PMI23 Exam\\task_a.xml");



            //task B

            var queryForTaskB = from zap in all_zapysy
                                join posv in all_posvs on zap.PosvId equals posv.PosvId
                                join pr in all_practs on posv.PosvId equals pr.PrId
                                join th in all_theory on posv.PosvId equals th.ThId
                                orderby posv.DriverSurname
                                select new
                                {
                                    DriverSurname = posv.DriverSurname,
                                    LicenseCat = posv.LicenseCat,
                                    StartDate = zap.Date,
                                    EndDate = posv.LicenseDate,
                                    TotalMark = th.Mark + pr.Mark
                                };

            var XTask_b = new XElement("TaskB",
                from item in queryForTaskB
                select new XElement("Driver",
                new XElement("DriverSurname", item.DriverSurname),
                new XElement("LicenseCat", item.LicenseCat),
                new XElement("StartDate", item.StartDate),
                new XElement("EndDate", item.EndDate),
                new XElement("TotalMark", item.TotalMark)
                ));

            XTask_b.Save("D:\\Programming\\C#\\Univer\\Templates\\PMI23 Exam\\PMI23 Exam\\task_b.xml");


            //task C

            var queryForTaskC = from zap in all_zapysy
                                join posv in all_posvs on zap.PosvId equals posv.PosvId
                                join pr in all_practs on posv.PosvId equals pr.PrId
                                join th in all_theory on posv.PosvId equals th.ThId
                                orderby posv.DriverSurname, posv.PosvId
                                group posv by new {posv.LicenseCat, zap.Date} into gr
                                select new
                                {
                                    Category = gr.Key.LicenseCat,
                                    Surnames = gr.Select(g => new { Surnames = all_posvs.FirstOrDefault(s => s.PosvId == g.PosvId), g.LicenseDate })
                                };

            var XTask_c = new XElement("TaskC",
                from item in queryForTaskC
                select new XElement("Category",
                new XAttribute("Category", item.Category),
                from w in item.Surnames
                select new XElement("Person",
                new XElement("Surname", w.Surnames.DriverSurname),
                new XElement("PosvId", w.Surnames.PosvId),
                new XElement("LicenseDate", w.LicenseDate)
                )));

            XTask_c.Save("D:\\Programming\\C#\\Univer\\Templates\\PMI23 Exam\\PMI23 Exam\\task_c.xml");



            //task D

            var queryForTaskD = from zap in all_zapysy
                                join posv in all_posvs on zap.PosvId equals posv.PosvId
                                join pr in all_practs on posv.PosvId equals pr.PrId
                                join th in all_theory on posv.PosvId equals th.ThId
                                group th by new { posv.LicenseCat } into gr
                                select new
                                {
                                    Category = gr.Key.LicenseCat,
                                    Questions = gr.Select(g => new {Question = all_theory.FirstOrDefault(s => s.ThId == g.ThId)})
                                };
            
            var XTask_d = new XElement("TaskD",
                from item in queryForTaskD
                let minimum = item.Questions.ToList().Min(s => s.Question.Mark)
                select new XElement("Category", 
                new XAttribute("Category", item.Category),
                from w in item.Questions
                where w.Question.Mark == minimum
                select new XElement("Questions",
                new XElement("Question", w.Question.Question),
                new XElement("Mark", w.Question.Mark)
                )));

            XTask_d.Save("D:\\Programming\\C#\\Univer\\Templates\\PMI23 Exam\\PMI23 Exam\\task_d.xml");
        }
    }
}