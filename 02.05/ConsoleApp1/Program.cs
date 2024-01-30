using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class PointPair
    {
        public int x1, y1, x2, y2;
        public PointPair(int a = -100, int b = 100)
        {
            Random random = new Random();
            do
            {
                x1 = random.Next(a, b + 1);
                x2 = random.Next(a, b + 1);
                y1 = random.Next(a, b + 1);
                y2 = random.Next(a, b + 1);
            } while (!IsValid());
        }
        public PointPair(int x, int y, int width, int height)
        {
            x1 = x;
            y1 = y;
            x2 = x + width;
            y2 = y + height;
        }
        public bool IsValid()
        {
            return x1 != x2 &&  y1 != y2;
        }
    }
    public class PointPairEventArgs: EventArgs
    {
        public PointPair PointPair { get; }
        public PointPairEventArgs(PointPair pointPair)
        {
            PointPair = pointPair;
        }
    }
    public class RectangleCollection
    {
        public event EventHandler<PointPairEventArgs> PairGenerated;
        private List<PointPair> prpts;
        public RectangleCollection(int count, int a, int b)
        {
            prpts = new List<PointPair>();
            Random random = new Random();
            for (int i = 0; i < count; i++)
            {
                int x = random.Next(a, b + 1);
                int y = random.Next(a, b + 1);
                int width = random.Next(1, b - x + 1);
                int height = random.Next(1, b - y + 1);
                prpts.Add(new PointPair(x, y, width, height));
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {

        }
    }
}
