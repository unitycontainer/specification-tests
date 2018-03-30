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

        public interface IFoo<TEntity>
        {
            TEntity Value { get; }
        }

        public interface IFoo
        {
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

        public class Foo : IFoo
        {
        }
    }
}
