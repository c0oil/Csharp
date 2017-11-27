using System;

namespace stream
{
    public class DelayedObject<T>
    {
        private readonly Func<T> resolveValue;
        private bool isResolved;
        private T resolvedValue;

        public T Value
        {
            get
            {
                if (!isResolved)
                {
                    isResolved = true;
                    resolvedValue = resolveValue();
                }
                return resolvedValue;
            }
        }
        
        public DelayedObject(Func<T> resolveValue)
        {
            this.resolveValue = resolveValue;
        }
    }
}