using System;
using Unity.Resolution;

namespace Unity.Specification.Utility
{
    public class ValidatingResolver : IResolve
    {
        private object _value;

        public ValidatingResolver(object value)
        {
            _value = value;
        }

        public object Resolve<TContext>(ref TContext context) where TContext : IResolveContext
        {
            Type = context.Type;

            return _value;
        }

        public Type Type { get; private set; }
    }
}
