using System;

namespace stream
{
    public class Stream<T>
    {
        private readonly DelayedObject<StreamItem<T>> delayedChain;
        public StreamItem<T> Chain => delayedChain.Value;

        public Stream(Func<StreamItem<T>> getChain)
        {
            delayedChain = new DelayedObject<StreamItem<T>>(getChain);
        }
    }

    public class StreamItem<T>
    {
        private readonly DelayedObject<StreamItem<T>> delayedCdr;
        public T Car { get; }

        public StreamItem<T> Cdr => delayedCdr.Value;

        public StreamItem(T car, Func<StreamItem<T>> getCdr)
        {
            Car = car;
            delayedCdr = new DelayedObject<StreamItem<T>>(getCdr);
        }
    }

    public static class StreamEnumerable
    {
        public static void ForEach<T>(this Stream<T> stream, Action<T> doAction)
        {
             stream.Chain.ForEach(doAction);
        }

        public static Stream<int> GetRange(int low, int high)
        {
            return new Stream<int>(() => StreamItemEnumerable.GetRange(low, high));
        }

        public static Stream<T> Where<T>(this Stream<T> stream, Func<T, bool> predicate)
        {
            return new Stream<T>(() => stream.Chain.Where(predicate));
        }

        public static Stream<T> Take<T>(this Stream<T> stream, int count)
        {
            return new Stream<T>(() => stream.Chain.Take(count));
        }

        public static Stream<T> Skip<T>(this Stream<T> stream, int count)
        {
            return new Stream<T>(() => stream.Chain.Skip(count));
        }
    }

    public static class StreamItemEnumerable
    {
        public static StreamItem<int> GetRange(int low, int high)
        {
            if (low > high)
            {
                return GetEnd<int>();
            }
            return new StreamItem<int>(low, () => GetRange(low + 1, high));
        }

        public static void ForEach<T>(this StreamItem<T> stream, Action<T> doAction)
        {
            if (stream.IsEnd())
            {
                return;
            }

            doAction(stream.Car);
            ForEach(stream.Cdr, doAction);
        }

        public static StreamItem<T> Where<T>(this StreamItem<T> stream, Func<T, bool> predicate)
        {
            if (stream.IsEnd())
            {
                return GetEnd<T>();
            }

            return predicate(stream.Car)
                ? new StreamItem<T>(stream.Car, () => Where(stream.Cdr, predicate))
                : Where(stream.Cdr, predicate);
        }

        public static StreamItem<T> Take<T>(this StreamItem<T> stream, int count)
        {
            if (stream.IsEnd())
            {
                return GetEnd<T>();
            }

            return count > 0
                ? new StreamItem<T>(stream.Car, () => Take(stream.Cdr, count - 1))
                : GetEnd<T>();
        }

        public static StreamItem<T> Skip<T>(this StreamItem<T> stream, int count)
        {
            if (stream.IsEnd())
            {
                return GetEnd<T>();
            }

            return count > 0
                ? new StreamItem<T>(stream.Car, () => Skip(stream.Cdr, count - 1))
                : stream.Cdr;
        }

        private static StreamItem<T> GetEnd<T>()
        {
            return null;
        }

        private static bool IsEnd<T>(this StreamItem<T> stream)
        {
            return stream == null;
        }
    }
}