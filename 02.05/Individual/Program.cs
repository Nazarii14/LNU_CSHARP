using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Individual
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //string path1 = "C:\\LNU\\2-й курс\\Програмування (C#)\\Практики\\02.05\\Individual\\Products.xml";
            //string path2 = "C:\\LNU\\2-й курс\\Програмування (C#)\\Практики\\02.05\\Individual\\Operations.xml";
            //string path3 = "C:\\LNU\\2-й курс\\Програмування (C#)\\Практики\\02.05\\Individual\\Checks.xml";
            //string oupPath1 = "C:\\LNU\\2-й курс\\Програмування (C#)\\Практики\\02.05\\Individual\\Out1.xml";
            //string oupPath2 = "C:\\LNU\\2-й курс\\Програмування (C#)\\Практики\\02.05\\Individual\\Out2.xml";
            //string oupPath3 = "C:\\LNU\\2-й курс\\Програмування (C#)\\Практики\\02.05\\Individual\\Out3.xml";

            string path1 = "Products.xml";
            string path2 = "Operations.xml";
            string path3 = "Checks.xml";
            string oupPath1 = "Out1.xml";
            string oupPath2 = "Out2.xml";
            string oupPath3 = "Out3.xml";

            var XProducts = new List<XElement>();
            var XOperations = new List<XElement>();
            var XChecks = new List<XElement>();

            using (var fs = new FileStream(path1, FileMode.Open))
            {
                var root = XElement.Load(fs);
                XProducts.Add(root);
            }
            using (var fs = new FileStream(path2, FileMode.Open))
            {
                var root = XElement.Load(fs);
                XOperations.Add(root);
            }
            using (var fs = new FileStream(path3, FileMode.Open))
            {
                var root = XElement.Load(fs);
                XChecks.Add(root);
            }

            // Task A

            var taskA = from product in XProducts.Descendants("Product")
                        join check in XChecks.Descendants("Check") on product.Element("CategoryID").Value equals check.Element("CategoryID").Value
                        join operation in XOperations.Descendants("Operation") on check.Element("OperID").Value equals operation.Element("OperID").Value
                        select new
                        {
                            CategoryID = product.Element("CategoryID").Value,
                            OperationName = operation.Element("OperName").Value
                        };

            var taskAGrouped = taskA.GroupBy(x => new { x.CategoryID, x.OperationName })
                              .Select(g => new { CategoryID = g.Key.CategoryID, OperationName = g.Key.OperationName, Count = g.Count() })
                              .OrderByDescending(x => x.Count);

            var XTaskA = new XElement("CategoryOperations",
                from r in taskAGrouped
                group r by r.CategoryID into g
                orderby g.Key
                select new XElement("Category",
                    new XAttribute("ID", g.Key),
                    from item in g
                    select new XElement("Operation",
                        new XAttribute("Name", item.OperationName),
                        new XAttribute("Count", item.Count))));

            XTaskA.Save(oupPath1);

            // Task B

            var taskB = from product in XProducts.Descendants("Product")
                        join check in XChecks.Descendants("Check") on product.Element("CategoryID").Value equals check.Element("CategoryID").Value
                        join operation in XOperations.Descendants("Operation") on check.Element("OperID").Value equals operation.Element("OperID").Value
                        orderby product.Element("CategoryID").Value, operation.Element("Price").Value descending
                        group new { OperationName = operation.Element("OperName").Value, Price = decimal.Parse(operation.Element("Price").Value) } by product.Element("CategoryID").Value into g
                        select new XElement("Category",
                            new XAttribute("ID", g.Key),
                            from item in g
                            group item by item.OperationName into ig
                            let sum = ig.Sum(x => x.Price)
                            //let sum = check.Element("ReleaseDate").Value.AddMonths(product.Element("Garant")) > check.Element("OperDate").Value ? 0 : ig.Sum(x => x.Price)
                            orderby sum descending
                            select new XElement("Operation",
                                new XAttribute("Name", ig.Key),
                                new XAttribute("Amount", sum)));

            var XTaskB = new XDocument(new XElement("Categories", taskB));
            XTaskB.Save(oupPath2);

            // Task C

            string category = "123456";

            var taskC = from product in XProducts.Descendants("Product")
                        where product.Element("CategoryID").Value == category
                        join check in XChecks.Descendants("Check") on product.Element("ProductID").Value equals check.Element("ProductID").Value
                        join operation in XOperations.Descendants("Operation") on check.Element("OperID").Value equals operation.Element("OperID").Value
                        let warrantyEndDate = DateTime.Parse(product.Element("ReleaseDate").Value).AddMonths(int.Parse(product.Element("Garant").Value))
                        where warrantyEndDate > DateTime.Parse(check.Element("OperDate").Value)
                        group operation by product.Element("ProductID").Value into g
                        orderby g.Count() descending
                        select new XElement("Product",
                                            new XAttribute("ID", g.Key),
                                            new XAttribute("OperationsPerformed", g.Count()));
      
            var XTaskC = new XElement("PerformedOperations", taskC);
            XTaskC.Save(oupPath3);
        }
    }
}
