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

            var path1 = "category.xml";
            var path2 = "kvytantsia.xml";
            var path3 = "operation.xml";

            var XCategories = new List<XElement>();
            var XOperations = new List<XElement>();
            var XChecks = new List<XElement>();

            using (var fs = new FileStream(path1, FileMode.Open))
            {
                var root = XElement.Load(fs);
                XCategories.Add(root);
            }
            using (var fs = new FileStream(path3, FileMode.Open))
            {
                var root = XElement.Load(fs);
                XOperations.Add(root);
            }
            using (var fs = new FileStream(path2, FileMode.Open))
            {
                var root = XElement.Load(fs);
                XChecks.Add(root);
            }

            // Task A

            var query = from cat in XCategories.Descendants("Category")
                        join check in XChecks.Descendants("Check") on cat.Element("CatId").Value equals check.Element("CatId").Value
                        join operation in XOperations.Descendants("Operation") on check.Element("OpId").Value equals operation.Element("OpId").Value
                        select new
                        {
                            CategoryID = cat.Element("CatId").Value,
                            OperationName = operation.Element("Name").Value
                        };
        
            var result = query.GroupBy(x => new { x.CategoryID, x.OperationName })
                              .Select(g => new { CategoryID = g.Key.CategoryID, OperationName = g.Key.OperationName, Count = g.Count() })
                              .OrderByDescending(x => x.Count);

            var xResult = new XElement("CategoryOperations",
                from r in result
                group r by r.CategoryID into g
                orderby g.Key
                select new XElement("Category",
                    new XAttribute("ID", g.Key),
                    from item in g
                    select new XElement("Operation",
                        new XAttribute("Name", item.OperationName),
                        new XAttribute("Count", item.Count))));

            var result_task_a_path = "task_a.xml";
            xResult.Save(result_task_a_path);


            // Task B

            var query_b = from cat in XCategories.Descendants("Category")
                        join check in XChecks.Descendants("Check") on cat.Element("CatId").Value equals check.Element("CatId").Value
                        join operation in XOperations.Descendants("Operation") on check.Element("OpId").Value equals operation.Element("OpId").Value
                        select new
                        {
                            CategoryID = cat.Element("CatId").Value,
                            OperationName = operation.Element("Name").Value,
                            OperationId = operation.Element("OpId").Value,
                            TotalPrice = Convert.ToInt32(operation.Element("Price").Value) * Convert.ToInt32(check.Element("OpId").Value.Count())
                        };

            var result_b = query_b.GroupBy(x => new { 
                                    x.CategoryID, 
                                    x.OperationName, 
                                    x.OperationId, 
                                    x.TotalPrice })
                                   .Select(g => new { 
                                       CategoryID = g.Key.CategoryID, 
                                       OperationName = g.Key.OperationName, 
                                       OperationId = g.Key.OperationId,
                                       Sum = g.Key.TotalPrice
                                   })
                                    .OrderByDescending(x => x.Sum);


            var xResult_b = new XElement("CategoryOperations",
                from r in result_b
                group r by r.CategoryID into g
                orderby g.Key
                select new XElement("Category",
                    new XAttribute("ID", g.Key),
                    from item in g
                    select new XElement("Operation",
                        new XAttribute("Name", item.OperationName),
                        new XAttribute("OpId", item.OperationId),
                        new XAttribute("Sum", item.Sum))));

            var result_task_b_path = "task_b.xml";
            xResult_b.Save(result_task_b_path);


            // Task C

            string someCategoryId = "123456";

            var query_c = from cat in XCategories.Descendants("Category")
                          join check in XChecks.Descendants("Check") on cat.Element("CatId").Value equals check.Element("CatId").Value
                          join operation in XOperations.Descendants("Operation") on check.Element("OpId").Value equals operation.Element("OpId").Value
                          where cat.Element("CatId").Value == someCategoryId && DateTime.Parse(check.Element("OpDate").Value).AddMonths(int.Parse(cat.Element("Guarrantee").Value)) > DateTime.Parse(check.Element("OpDate").Value)
                          group operation by cat.Element("CatId").Value into g
                          orderby g.Count() descending
                          select new XElement("Product",
                                              new XAttribute("ID", g.Key),
                                              new XAttribute("OperationsPerformed", g.Count()));

            var XTaskC = new XElement("PerformedOperations", query_c);
            XTaskC.Save("task_c.xml");
        }


    }
}


