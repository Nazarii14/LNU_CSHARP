using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
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

            //Розробити засоби для для генерування xml-файлiв з даними про порушникiв швидкiсного режиму.
            //Данi на основi вiдеоспостереження за учасниками руху заданi xml - файлом у форматi
            //<дата, година> <номер автомобiля> <категорiя автомобiля> <швидкiсть>.

            //Розрiзняють три категорiї: вантажiвки, автобуси i легковi авто.
            //Об’єкт класу Detector зчитує вмiстиме файла i послiдовно
            //аналiзує данi на предмет перевищення швидкостi 50 км за год.
            //Для кожного випадку, коли швидкiсть перевищена, цей об’єкт генерує подiю з вiдповiдними даними.
            //Два об’єкти - обсервери є попередньо зареєстрованими в детекторi i збирають данi про порушникiв
            //серед пасажирського i вантажного транспорту, формуючи кожен вiдповiдний xml-файл з даними
            //порушникiв <дата, година> <номер автомобiля> <швидкiсть>.

            //2.Отримати за допомогою механiзму подiй(використовуючи патерн Observer) зазначенi
            //xml - файли про порушникiв серед пасажирського i вантажного транспорту.
            
            var path = "data.xml";

            var read1 = new Detector(path);
            read1.printList();

        }

        public class SpeedEventArgs : EventArgs
        {
            public string Message { get; set; }
            public SpeedEventArgs(string s)
            {
                Message = s;
            }
        }
        public class Detector
        {
            
            public List<XElement> AllDrivers { get; set; }

            public event EventHandler<SpeedEventArgs> SpeedEventHandler;
            public delegate void EventHandler(object sender, EventArgs e);
            public virtual void onSpeedEvent(SpeedEventArgs e)
            {
                var handler = SpeedEventHandler;
                if (handler != null)
                {
                    e.Message += $"{Date}";
                }
                handler(this, e);
            }

            public Detector(string path)
            {
                using (var fs = new FileStream(path, FileMode.Open))
                {
                    var helpList = new List<XElement>();
                    
                    var root = XElement.Load(fs);
                    helpList.Add(root);
                    AllDrivers = helpList;

                    //foreach (var i in helpList.Elements())
                    //{
                    //    Date = DateTime.Parse(i.Element("Date").Value);
                    //    Number = Convert.ToInt32(i.Element("Number").Value);
                    //    CarCategory = i.Element("CarCat").Value.ToString();
                    //    Speed = Convert.ToInt32(i.Element("Speed").Value);
                    //}
                }
            }
            public void printList() 
            {
                foreach(var i in AllDrivers) { Console.WriteLine(i); }
            }

        }
    }
}


