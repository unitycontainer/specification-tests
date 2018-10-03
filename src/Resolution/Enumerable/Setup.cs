using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Injection;
using Unity.Lifetime;
using Unity.Specification.TestData;

namespace Unity.Specification.Resolution.Enumerable
{
    public abstract partial class SpecificationTests : TestFixtureBase
    {
        [TestInitialize]
        public override void Setup()
        {
            Container = GetContainer();

            Container.RegisterType(typeof(IList<>), typeof(List<>), new InjectionConstructor());
            Container.RegisterType(typeof(IFoo<>), typeof(Foo<>));
            Container.RegisterType<IService, Service>("1", new ContainerControlledLifetimeManager());
            Container.RegisterType<IService, Service>("2", new ContainerControlledLifetimeManager());
            Container.RegisterType<IService, Service>("3", new ContainerControlledLifetimeManager());
            Container.RegisterType<IService, Service>();

            Service.Instances = 0;
        }

        public interface ITest1<T> { }

        public interface ITest2<T> { }

        public class Test<T> : ITest1<T>, ITest2<T>
        {
            public string Id { get; } = Guid.NewGuid().ToString();
        }
    }
}
