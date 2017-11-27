using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stream
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = 1;
            /*
            new Stream<double>(() => Fib(1, 1)).
                Where(i => i % 2 == 0).
                Take(1000).
                ForEach(i => Console.WriteLine($"[{count++}]-[{i}]"));
                */

            
            new Stream<double>(() => Sieve(Increment(2))).
                Take(100).
                ForEach(i => Console.WriteLine($"--- [{i}] [count:{count++}]"));
                
            /*
            IEnumerable<int> decimalEnumerable = EnumerateInterval(1000, 10000 - 1000).Where(i => i % 10 == 0);
            foreach (int i in decimalEnumerable)
            {
                Console.WriteLine(i);
            }
            */
            
            double num = 2;
            List<double> result = new List<double> { 2 };
            while (result.Count < 100)
            {
                num++;
                if (IsSeave(num, result))
                {
                    result.Add(num);
                    Console.WriteLine($"--- [{num}] [count:{result.Count}]");
                }
            }
        }

        public static bool IsSeave(double num, List<double> prev)
        {
            return prev.All(x => !IsDivisible(num, x));
        }

        public static StreamItem<double> Increment(double number)
        {
            Console.WriteLine($"Inc {number}");
            return new StreamItem<double>(number, () => Increment(number + 1));
        }

        public static StreamItem<double> Sieve(StreamItem<double> stream)
        {
            Console.WriteLine($"Sieve {stream.Car}");
            return new StreamItem<double>(stream.Car, () => Sieve(stream.Where(x => !IsDivisible(x, stream.Car))));
        }

        public static StreamItem<double> Fib(double a, double b)
        {
            return new StreamItem<double>(a, () => Fib(b, a + b));
        }

        public static bool IsDivisible(double x, double b)
        {
            Console.WriteLine($"Is Divisible? {x} % {b}");
            return x % b == 0;
        }

        public static IEnumerable<int> EnumerateInterval(int start, int end)
        {
            for (int i = start; i < end; i++)
            {
                yield return i;
            }
        }

    }
}
