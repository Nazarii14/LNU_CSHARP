using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Individual
{
    internal class Program
    {
        public class SpeedEventArgs : EventArgs
        {
            public string Time { get; set; }
            public string CarNumber { get; set; }
            public string CarCategory { get; set; }
            public uint Speed { get; set; }
        }
        public interface ISpeedObserver
        {
            void OnSpeeding(object sender, SpeedEventArgs args);
        }
        public class SpeedingDetector
        {
            public List<ISpeedObserver> observers = new List<ISpeedObserver>();
            public void ReadData(string pathData)
            {
                var XData = new List<XElement>();
                using (var fs = new FileStream(pathData, FileMode.Open))
                {
                    var root = XElement.Load(fs);
                    XData.Add(root);
                }
                var querry = from i in XData.Descendants("Data")
                             select new
                             {
                                 date = i.Element("Time").Value,
                                 carNum = i.Element("CarNumber").Value,
                                 carCat = i.Element("CarCategory").Value,
                                 speed = uint.Parse(i.Element("Speed").Value)
                             };
                foreach (var item in querry)
                {
                    if (item.speed > 50)
                    {
                        SpeedEventArgs args = new SpeedEventArgs
                        {
                            Time = item.date,
                            CarNumber = item.carNum,
                            CarCategory = item.carCat,
                            Speed = item.speed
                        };
                        foreach (ISpeedObserver observer in observers)
                        {
                            observer.OnSpeeding(this, args);
                        }
                    }
                }
            }
        }
        public class LehkObserver : ISpeedObserver
        {
            private List<SpeedEventArgs> speedEvents = new List<SpeedEventArgs>();

            public void OnSpeeding(object sender, SpeedEventArgs args)
            {
                if (args.CarCategory == "Lehk" || args.CarCategory == "Bus")
                {
                    speedEvents.Add(args);
                }
            }

            public void WriteData(string outputFile)
            {
                var querry = from sp in speedEvents
                             select new
                             {
                                 Time = sp.Time,
                                 CarNum = sp.CarNumber,
                                 Speed = sp.Speed
                             };

                var toWrite = new XElement("ShtrafsLehk",
                    from item in querry
                    select new XElement("Shtraf",
                    new XElement("Time", item.Time),
                    new XElement("CarNumber", item.CarNum),
                    new XElement("Speed", item.Speed)));
                toWrite.Save(outputFile);
            }
        }

        public class TruckObserver : ISpeedObserver
        {
            private List<SpeedEventArgs> speedEvents = new List<SpeedEventArgs>();

            public void OnSpeeding(object sender, SpeedEventArgs args)
            {
                if (args.CarCategory == "Truck")
                {
                    speedEvents.Add(args);
                }
            }
            public void WriteData(string outputFile)
            {
                var querry = from sp in speedEvents
                             select new
                             {
                                 Time = sp.Time,
                                 CarNum = sp.CarNumber,
                                 Speed = sp.Speed
                             };

                var toWrite = new XElement("ShtrafsTruck",
                    from item in querry
                    select new XElement("Shtraf",
                    new XElement("Time", item.Time),
                    new XElement("CarNumber", item.CarNum),
                    new XElement("Speed", item.Speed)));
                toWrite.Save(outputFile);
            }

        }
        static void Main(string[] args)
        {
            string pathData = "Data.xml";
            string lehkOutputFile = "ShtrafsLehk.xml";
            string truckOutputFile = "ShtrafsTruck.xml";

            var detector = new SpeedingDetector();
            var lehkObserver = new LehkObserver();
            var truckObserver = new TruckObserver();

            detector.observers.Add(lehkObserver);
            detector.observers.Add(truckObserver);

            detector.ReadData(pathData);

            lehkObserver.WriteData(lehkOutputFile);
            truckObserver.WriteData(truckOutputFile);
        }
    }
}
