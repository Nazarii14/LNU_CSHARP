using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;

namespace StockObserverEventDR
{
    /// <summary>
    /// Observer Design Pattern
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            var ibm = new IBM("IBM", 120.00);
            ibm.Price = 120.10;
            var ibm_S = new Investor("Sorros", ibm);
            ibm.Price = 121.0;
            var ibm_B = new Investor("Berkshire", ibm);
            ibm.Price = 120.50;
            ibm.Price = 12.75;
            ibm.Price = 12.70;
        }
    }

    public class StockEventArgs : EventArgs
    {
        public StockEventArgs(string s)
        {
            Message = s;
        }
        public string Message { get; set; }
    }

    public abstract class Stock
    {
        private double price;
        public event EventHandler<StockEventArgs> RaiseStokEvent;
        public Stock(string s, double p)
        {
            Symbol = s;
            Price = p;
        }
        protected virtual void onRaiseStockEvent(StockEventArgs e)
        {
            var handler = RaiseStokEvent;

            if (handler != null)
            {
                e.Message += $" at {DateTime.Now}";
                handler(this, e);
            }
        }
        public double Price
        {
            get { return price; }
            set
            {
                if (price != value)
                {
                    price = value;
                    onRaiseStockEvent(new StockEventArgs("price was changed"));
                }
            }
        }
        public string Symbol { get; set; }
    }

    public class IBM : Stock
    {
        public IBM(string symbol, double price) : base(symbol, price) { }
    }
    public interface IInvestor
    {
        void Update(Object sender, StockEventArgs e);
    }
    public class Investor : IInvestor
    {
        public Investor(string name, Stock stock)
        {
            Name = name;
            stock.RaiseStokEvent += Update;
        }
        public void Update(Object sender, StockEventArgs e)
        {
            Console.WriteLine($"Notified {Name} of {((Stock)sender).Symbol}'s change to {((Stock)sender).Price:C} -- {e.Message}");
        }
        public string Name { get; }
        public Stock Stock { get; set; }
    }
}
