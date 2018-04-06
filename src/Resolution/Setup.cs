using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Injection;
using Unity.Lifetime;
using Unity.Specification.TestData;

namespace Unity.Specification.Resolution
{
    public abstract partial class SpecificationTests : TestFixtureBase
    {
        private IUnityContainer _container;

        [TestInitialize]
        public void Setup()
        {
            _container = GetContainer();

            _container.RegisterType(typeof(IList<>), typeof(List<>), new InjectionConstructor());
            _container.RegisterType(typeof(IFoo<>), typeof(Foo<>));
            _container.RegisterType<IService, Service>("1", new ContainerControlledLifetimeManager());
            _container.RegisterType<IService, Service>("2", new ContainerControlledLifetimeManager());
            _container.RegisterType<IService, Service>("3", new ContainerControlledLifetimeManager());
            _container.RegisterType<IService, Service>();

            Service.Instances = 0;
        }

        public interface ITest1<T> { }

        public interface ITest2<T> { }

        public class Test<T> : ITest1<T>, ITest2<T>
        {
            public string Id { get; } = Guid.NewGuid().ToString();
        }

        public interface IFoo<TEntity>
        {
            TEntity Value { get; }
        }

        public interface IFoo { }
        public interface IFoo1 { }
        public interface IFoo2 { }

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

        public class Foo : IFoo, IFoo1, IFoo2
        {
        }
    }
}
