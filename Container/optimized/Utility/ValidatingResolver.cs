﻿using System;
using Unity.Resolution;

namespace Unity.Specification.Utility
{
    // Move to regression
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
            Name = context.Name;

            return _value;
        }

        public Type Type { get; private set; }

        public string Name { get; private set; }
    }
}
