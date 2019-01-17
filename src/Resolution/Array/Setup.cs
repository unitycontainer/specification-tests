using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading;
using Unity.Injection;
using Unity.Lifetime;

namespace Unity.Specification.Resolution.Array
{
    public abstract partial class SpecificationTests : TestFixtureBase
    {
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();

            Container.RegisterType(typeof(IList<>), typeof(List<>), new InjectionConstructor());
            Container.RegisterType(typeof(IFoo<>), typeof(Foo<>));
            Container.RegisterType<IService, Service>("1", new ContainerControlledLifetimeManager());
            Container.RegisterType<IService, Service>("2", new ContainerControlledLifetimeManager());
            Container.RegisterType<IService, Service>("3", new ContainerControlledLifetimeManager());
            Container.RegisterType<IService, Service>();

            Service.Instances = 0;
        }

        #region Test Data

        public interface ILogger
        {
        }

        public class SpecialLogger : ILogger
        {
        }

        public class MockLogger : ILogger
        {
        }

        public class TypeWithArrayConstructorParameter
        {
            public readonly ILogger[] Loggers;

            public TypeWithArrayConstructorParameter(ILogger[] loggers)
            {
                Loggers = loggers;
            }
        }

        public class GenericTypeWithArrayConstructorParameter<T>
        {
            public readonly T[] Values;

            public GenericTypeWithArrayConstructorParameter(T[] values)
            {
                Values = values;
            }
        }

        public class TypeWithArrayProperty
        {
            [Dependency]
            public ILogger[] Loggers { get; set; }
        }

        public class TypeWithArrayConstructorParameterOfRankTwo
        {
            private readonly ILogger[,] _unknown;

            public TypeWithArrayConstructorParameterOfRankTwo(ILogger[,] array)
            {
                _unknown = array;
            }
        }

        #endregion

        public interface IFoo<TEntity>
        {
            TEntity Value { get; }
        }

        public class Foo<TEntity> : IFoo<TEntity>
        {
            public Foo()
            {
            }

            public Foo(TEntity value)
            {
                Value = value;
            }

            public TEntity Value { get; }
        }

        public interface IService
        {
        }

        public class Service : IService, IDisposable
        {
            public string Id { get; } = Guid.NewGuid().ToString();

            public static int Instances;

            public Service()
            {
                Interlocked.Increment(ref Instances);
            }

            public bool Disposed;
            public void Dispose()
            {
                Disposed = true;
            }
        }

    }
}
