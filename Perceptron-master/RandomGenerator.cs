using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;

namespace Hopfield
{
    public static class RandomGenerator
    {
        private static readonly Random rand;

        static RandomGenerator()
        {
            rand = new Random(DateTime.Now.Millisecond);
        }

        public static bool Match(int probability)
        {
            return rand.Next(100) < probability;
        }

        public static int Next(int limit)
        {
            return rand.Next(limit);
        }

        public static Point NextPoint(int limit)
        {
            return new Point { X = Next(limit), Y = Next(limit) };
        }

        public static double NextDoubleFromMinusOneToPlusOne()
        {
            bool positive = rand.Next(2) == 0;
            double value = positive ? rand.NextDouble() : -1 * rand.NextDouble();
            return value;
        }
    }
}
