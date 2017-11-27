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

            /*
            new Stream<double>(() => Sieve(Increment(2))).
                Take(100).
                ForEach(i => Console.WriteLine($"--- [{i}] [count:{count++}]"));
                */

            /*
            IEnumerable<int> decimalEnumerable = EnumerateInterval(1000, 10000 - 1000).Where(i => i % 10 == 0);
            foreach (int i in decimalEnumerable)
            {
                Console.WriteLine(i);
            }
            */

            Stream<double> pisums = new Stream<double>(() => Add(1, 2)).
                Zip(new Stream<double>(() => Multi(1, -1)), (s, f) => 4 * f / s);

            Stream<double> pistream = new Stream<double>(() => Sum(pisums.Chain, pisums.Chain));

            Stream<double> factEulerPiStream = AccelerateSequence(pistream, EulerTransform);

            count = 1;
            factEulerPiStream.Take(20).ForEach(x => Console.WriteLine($"--- [{x}] [count:{count++}]"));

            /*
            double result = 0;
            new Stream<double>(() => Add(1, 2)).
                Zip(new Stream<double>(() => Multi(1, -1)), (s, f) => 4 * f/s).
                Take(100).
                ForEach(x => result += x);
            Console.WriteLine($"--- [{result}]");
            */

            /*
            new Stream<double>(() => Sieve(Increment(2, 1))).
                //Zip(new Stream<double>(() => Increment(2)), (s, f) => s + f).
                Take(50).
                ForEach(i => Console.WriteLine($"--- [{i}] [count:{count++}]"));
                */

            /*
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
            */
        }

        public static bool IsSeave(double num, List<double> prev)
        {
            return prev.All(x => !IsDivisible(num, x));
        }

        public static StreamItem<double> EulerTransform(StreamItem<double> stream)
        {
            double s0 = stream.ElementAt(0);
            double s1 = stream.ElementAt(1);
            double s2 = stream.ElementAt(2);

            double val = s2 - Math.Pow(s2 - s1, 2) / (s0 - 2* s1 + s2);
            Console.WriteLine($"--- [{val}]");
            return new StreamItem<double>(val, 
                () => EulerTransform(stream.Cdr));
        }

        public static StreamItem<double> Repeat(double number)
        {
            return new StreamItem<double>(number, () => Repeat(number));
        }

        public static StreamItem<double> Multi(double number, double mul)
        {
            return new StreamItem<double>(number, () => Multi(number * mul, mul));
        }

        public static StreamItem<double> Add(double number, double add)
        {
            return new StreamItem<double>(number, () => Add(number + add, add));
        }

        public static StreamItem<StreamItem<double>> TabloTramsform(StreamItem<double> stream, Func<StreamItem<double>, StreamItem<double>> transformFunc)
        {
            return new StreamItem<StreamItem<double>>(stream, () => TabloTramsform(transformFunc(stream), transformFunc));
        }

        public static Stream<double> AccelerateSequence(Stream<double> stream, Func<StreamItem<double>, StreamItem<double>> transformFunc)
        {
            return new Stream<double>(() => TabloTramsform(stream.Chain, transformFunc).Select(x => x.Car));
        }

        public static StreamItem<double> Sum(StreamItem<double> stream, StreamItem<double> first)
        {
            return new StreamItem<double>(stream.Car, () => Sum(stream.Cdr.Zip(first, (a, b) => a + b), first));
        }

        public static StreamItem<double> Sieve(StreamItem<double> stream)
        {
            return new StreamItem<double>(stream.Car, () => Sieve(stream.Where(x => !IsDivisible(x, stream.Car))));
        }

        public static StreamItem<double> Fib(double a, double b)
        {
            return new StreamItem<double>(a, () => Fib(b, a + b));
        }

        public static bool IsDivisible(double x, double b)
        {
            //Console.WriteLine($"Is Divisible? {x} % {b}");
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
