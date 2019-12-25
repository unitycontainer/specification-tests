using System;
using Unity.Resolution;

namespace Unity.Specification.Utility
{
    public class ValidatingResolverFactory : IResolverFactory<Type>
    {
        private object _value;

        public ValidatingResolverFactory(object value)
        {
            _value = value;
        }

        public Type Type { get; private set; }

        public ResolveDelegate<TContext> GetResolver<TContext>(Type info) 
            where TContext : IResolveContext
        {
            return (ref TContext context) =>
            {
                Type = context.Type;
                return _value;
            };
        }
    }
}
