using System;
using System.Diagnostics;
using System.Diagnostics.SymbolStore;
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
            var path_reestr = "D:\\Programming\\C#\\Univer\\Templates\\PMI22 Exam\\PMI22 Exam\\reestr.xml";
            var path_test = "D:\\Programming\\C#\\Univer\\Templates\\PMI22 Exam\\PMI22 Exam\\test.xml";
            var path_server = "D:\\Programming\\C#\\Univer\\Templates\\PMI22 Exam\\PMI22 Exam\\server.xml";
            var path_platform = "D:\\Programming\\C#\\Univer\\Templates\\PMI22 Exam\\PMI22 Exam\\platforma.xml";

            var reestr = XElement.Load(path_reestr);
            var test = XElement.Load(path_test);
            var server = XElement.Load(path_server);
            var platform = XElement.Load(path_platform);

            var all_reestrs = reestr.Elements("Reestr").Select(w => new
            {
                TestId = (int)w.Element("TestId"),
                Date = (DateTime)w.Element("Date"),
                ServerId = (string)w.Element("ServerId"),
                Result = (string)w.Element("Result")
            });
            foreach (var i in all_reestrs) { Console.WriteLine(i); }


            var all_tests = test.Elements("Test").Select(p => new
            {
                TestId = (int)p.Element("TestId"),
                Login = (string)p.Element("Login"),
                Language = (string)p.Element("Language"),
                CodeSize = (int)p.Element("CodeSize")
            });
            foreach (var i in all_tests) { Console.WriteLine(i); }


            var all_platforms = platform.Elements("Platform").Select(w => new
            {
                PlatformaId = (string)w.Element("PlatformaId"),
                Name = (string)w.Element("Name")
            });
            foreach (var i in all_platforms) { Console.WriteLine(i); }


            var all_servers = server.Elements("Server").Select(w => new
            {
                ServerId = (string)w.Element("ServerId"),
                PlatformaId = (string)w.Element("PlatformaId")
            });
            foreach (var i in all_servers) { Console.WriteLine(i); }



            //task A

            var queryForTaskA = from t in all_tests
                                join r in all_reestrs on t.TestId equals r.TestId
                                join s in all_servers on r.ServerId equals s.ServerId
                                join p in all_platforms on s.PlatformaId equals p.PlatformaId
                                orderby t.Login
                                select new
                                {
                                    TestId = t.TestId,
                                    Login = t.Login,
                                    TestDate = r.Date,
                                    Result = r.Result
                                };

            var XTask_a = new XElement("TaskA",
                from item in queryForTaskA
                select new XElement("TaskA",
                new XElement("TestId", item.TestId),
                new XElement("Login", item.Login),
                new XElement("TestDate", item.TestDate),
                new XElement("Result", item.Result)
                ));


            XTask_a.Save("D:\\Programming\\C#\\Univer\\Templates\\PMI22 Exam\\PMI22 Exam\\task_a.xml");




            var queryForTaskB = from t in all_tests
                                join r in all_reestrs on t.TestId equals r.TestId
                                join s in all_servers on r.ServerId equals s.ServerId
                                join p in all_platforms on s.PlatformaId equals p.PlatformaId
                                orderby t.Login, p.Name
                                select new
                                {
                                    TestId = t.TestId,
                                    Login = t.Login,
                                    TestDate = r.Date,
                                    Result = r.Result,
                                    PlatformName = p.Name
                                };

            var XTask_b = new XElement("TaskB",
                from item in queryForTaskB
                select new XElement("TaskB",
                new XElement("TestId", item.TestId),
                new XElement("Login", item.Login),
                new XElement("TestDate", item.TestDate),
                new XElement("Result", item.Result),
                new XElement("PlatformName", item.PlatformName)
                ));


            XTask_b.Save("D:\\Programming\\C#\\Univer\\Templates\\PMI22 Exam\\PMI22 Exam\\task_b.xml");


            var queryForTaskC = from t in all_tests
                                join r in all_reestrs on t.TestId equals r.TestId
                                join s in all_servers on r.ServerId equals s.ServerId
                                join p in all_platforms on s.PlatformaId equals p.PlatformaId
                                group t by new { p.Name } into gr
                                select new
                                {
                                    PlatformName = gr.Key.Name,
                                    TotalCodeSize = gr.Select(g => new { CdSz = all_tests.FirstOrDefault(s => s.TestId == g.TestId) })
                                };

            var XTask_c = new XElement("TaskC",
                from item in queryForTaskC
                let TotalSize = item.TotalCodeSize.ToList().Sum(s=> s.CdSz.CodeSize)
                select new XElement("TaskC",
                new XAttribute("PlatformName", item.PlatformName),
                new XElement("Result", TotalSize)));


            XTask_c.Save("D:\\Programming\\C#\\Univer\\Templates\\PMI22 Exam\\PMI22 Exam\\task_c.xml");



            var queryForTaskD = from t in all_tests
                                join r in all_reestrs on t.TestId equals r.TestId
                                select new
                                {
                                    Login = t.Login,
                                    Language = t.Language,
                                    Result = r.Result
                                };

            var query2 = from k in queryForTaskD
                         group k by new { k.Login } into gr
                         select new
                         {
                             Login = gr.Key.Login,
                             Languages = from s in gr
                                         group s by new { s.Language } into gr2
                                         let successfulTests = gr2.Count(x => x.Result == "CORRECT")
                                         let totalTests = gr2.Count()
                                         orderby gr2.Key
                                         select new
                                         {
                                             Language = gr2.Key,
                                             SuccessRate = totalTests > 0 ? (double)successfulTests / totalTests * 100 : 0
                                         }
                         };

            var XTask_d = new XElement("TaskD",
                from item in query2
                select new XElement("TaskD",
                new XAttribute("Login", item.Login),
                from w in item.Languages
                let perc = w.SuccessRate.ToString("0.00") + "%"
                select new XElement("Language",
                new XElement("LanguageName", w.Language), 
                new XElement("Percentage", perc))));

            XTask_d.Save("D:\\Programming\\C#\\Univer\\Templates\\PMI22 Exam\\PMI22 Exam\\task_d.xml");
        }
    }
}