using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Unity.Specification.Resolution.Mapping
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


        public interface IService
        {
        }

        public interface IGenericService<T>
        {
        }


        public interface ITest1<T> { }

        public interface ITest2<T> { }

        public class Test<T> : ITest1<T>, ITest2<T>
        {
            public string Id { get; } = Guid.NewGuid().ToString();
        }


        public class Service : IService, IDisposable
        {
            public string ID { get; } = Guid.NewGuid().ToString();

            public static int Instances = 0;

            public Service()
            {
                Interlocked.Increment(ref Instances);
            }

            public bool Disposed = false;
            public void Dispose()
            {
                Disposed = true;
            }
        }


        public interface IOtherService
        {
        }

        public class OtherService : IService, IOtherService, IDisposable
        {
            [InjectionConstructor]
            public OtherService()
            {

            }

            public OtherService(IUnityContainer container)
            {

            }


            public bool Disposed = false;
            public void Dispose()
            {
                Disposed = true;
            }
        }
    }
}
