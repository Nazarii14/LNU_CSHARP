using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Program
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            //0.0.1
            //1.Розробити засоби для iнформацiйної системи сервiсного центру.
            //Вироби вiдносяться до однiєї з категорiй. Категорiя виробу характеризується iдентифiкацiйним номером категорiї, 
            //назвою i кiлькiстю мiсяцiв гарантiйного обслуговування, яка вiдраховується вiд
            //дати випуску виробу.
            //Операцiя з виробом характеризуються iдентифiкацiйним номером, назвою i вартiстю. Обслуговування в перiод 
            //гарантiї безкоштовне.
            //Квитанцiя про обслуговування мiстить данi у форматi < iдентифiкацiйний номер категорiї виробу,
            //дата випуску виробу, дата обслуговування, номер операцiї>
            //Усi данi задано окремими xml-файлами.

            //2. Отримати (використовуючи linq):
            //(а)xml - файл, де для кожної категорiї виробiв (впорядкування у лексико-графiчному порядку) вказати кiлькiсть кожної з 
            //    виконаних операцiй у форматi <назва операцiї: кiлькiсть >, цей перелiк
            
            //(в)xml - файл, де для заданої категорiї виробiв вказати кiлькiсть виконаних операцiй для виробiв
            //на гарантiї, перелiк впорядкований у спадному порядку за кiлькiстю.

            var path_cats = "cats.xml";
            var path_checks = "check.xml";
            var path_operations = "operation.xml";

            var categories = XElement.Load(path_cats);
            var checks = XElement.Load(path_checks);
            var operations = XElement.Load(path_operations);

            //task a
            var task_a = from check in checks.Elements()
                         join cat in categories.Elements() on check.Element("CatId").Value equals cat.Element("CatId").Value
                         join oper in operations.Elements() on check.Element("OpId").Value equals oper.Element("OpId").Value
                         group new
                         {
                             OperName = oper.Element("Name").Value,
                             Number = check.Element("CatId").Value.Count()
                         } by cat.Element("CatId").Value into g
                         select new XElement("Category",
                         new XElement("Id", g.Key),
                             from item in g
                             select 
                             new XElement("Operation", new XElement("OpName", item.OperName), new XElement("OpCount", item.Number)));

            string task_a_path = "task_a.xml";
            
            var task_a_res = new XElement("task_a", task_a);
            task_a_res.Save(task_a_path);


            //task b

            var task_b = from check in checks.Elements()
                         join cat in categories.Elements() on check.Element("CatId").Value equals cat.Element("CatId").Value
                         join oper in operations.Elements() on check.Element("OpId").Value equals oper.Element("OpId").Value
                         group new
                         {
                             OperName = oper.Element("Name").Value,
                             TotalSum = Int32.Parse(oper.Element("Price").Value) * check.Element("CatId").Value.Count()
                         } by cat.Element("CatId").Value into g
                         select new XElement("Category",
                         new XElement("OpName", g.Key),
                         from item in g
                         select new XElement("operation",
                         new XElement("OpName", item.OperName),
                         new XElement("TotalSum", item.TotalSum)));

            string task_b_path = "task_b.xml";

            var task_b_res = new XElement("task_b", task_b);
            task_b_res.Save(task_b_path);

            //(б)xml - файл, де для кожної категорiї виробiв (впорядкування у лексико-графiчному порядку) вказати
            //зароблену суму по кожнiй операцiї <назва операцiї: сума>,
            //цей перелiк впорядкований у спадному порядку стосовно суми;


        }
    }
}
