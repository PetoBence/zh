using System;
using System.Collections.Generic;
using System.IO;

namespace TwoDimensionalArray
{
    public enum Point2Tipus
    {
        Általános,
        Xtengelyen,
        Ytengelyen
    }

    public class Pont2 : IComparable<Pont2>
    {
        private double x;
        private double y;
        private Point2Tipus p2Tipus;
        private double tav2;

        public double X
        {
            get { return x; }
            set { x = value; }
        }

        public double Y
        {
            get { return y; }
            set { y = value; }
        }

        public Point2Tipus P2Tipus
        {
            get { return p2Tipus; }
            set { p2Tipus = value; }
        }

        public double Tav2
        {
            get { return tav2; }
            set { tav2 = value; }
        }

        public Pont2(double x, double y)
        {
            this.x = x;
            this.y = y;
            this.p2Tipus = CalculatePoint2Tipus();
            this.tav2 = Math.Sqrt(x * x + y * y);
        }

        private Point2Tipus CalculatePoint2Tipus()
        {
            if (x == 0 && y == 0)
                return Point2Tipus.Általános;
            else if (x == 0)
                return Point2Tipus.Ytengelyen;
            else if (y == 0)
                return Point2Tipus.Xtengelyen;
            else
                return Point2Tipus.Általános;
        }

        public override string ToString()
        {
            return $"({x},{y}), p2Tipus: {p2Tipus}";
        }

        public int CompareTo(Pont2 other)
        {
            if (tav2 > other.tav2)
                return 1;
            else if (tav2 < other.tav2)
                return -1;
            else
                return 0;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Pont2> pontok = new List<Pont2>();

            try
            {
                using (StreamReader sr = new StreamReader("pontok.txt"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] koordinatak = line.Split('.');
                        double x = double.Parse(koordinatak[0]);
                        double y = double.Parse(koordinatak[1]);

                        Pont2 pont = new Pont2(x, y);
                        pontok.Add(pont);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("A fájl nem olvasható:");
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("Koordináták listája:");
            foreach (Pont2 pont in pontok)
            {
                Console.WriteLine(pont.ToString());
            }

            Console.WriteLine("Pontok amelyik az origón köré rajzol 2 egység sugarú körön kívül esnek:");
            foreach (Pont2 pont in pontok)
            {
                if (pont.Tav2 > 2)
                    Console.WriteLine(pont.ToString());
            }

            pontok.Sort();

            Console.WriteLine("Pontok sorba rendezve:");
            foreach (Pont2 pont in pontok)
            {
                Console.WriteLine(pont.ToString());
            }
        }
    }
}

