using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading;
using Unity.Injection;
using Unity.Lifetime;

namespace Unity.Specification.Resolution.Lazy
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

        public interface ILogger
        {
        }
        public class MockLogger : ILogger
        {
        }
        public class SpecialLogger : ILogger
        {
        }


        public class ObjectThatGetsALazy
        {
            [Dependency]
            public Lazy<ILogger> LoggerLazy { get; set; }
        }

        public class ObjectThatGetsMultipleLazy
        {
            [Dependency]
            public Lazy<ILogger> LoggerLazy1 { get; set; }

            [Dependency]
            public Lazy<ILogger> LoggerLazy2 { get; set; }
        }

        public class EmailService : IService, IDisposable
        {
            public string Id { get; } = Guid.NewGuid().ToString();

            public bool Disposed;
            public void Dispose()
            {
                Disposed = true;
            }
        }

        // A dummy class to support testing type mapping
        public class OtherEmailService : IService, IOtherService, IDisposable
        {
            public string Id = Guid.NewGuid().ToString();

            [InjectionConstructor]
            public OtherEmailService()
            {

            }

            public OtherEmailService(IUnityContainer container)
            {

            }

            public bool Disposed;
            public void Dispose()
            {
                Disposed = true;
            }
        }

        public interface IBase
        {
            IService Service { get; set; }
        }

        public interface ILazyDependency
        {
            Lazy<EmailService> Service { get; set; }
        }

        public class Base : IBase
        {
            [Dependency]
            public IService Service { get; set; }
        }

        public class LazyDependency : ILazyDependency
        {
            [Dependency]
            public Lazy<EmailService> Service { get; set; }
        }

        public class LazyDependencyConstructor
        {
            private Lazy<EmailService> service;

            public LazyDependencyConstructor(Lazy<EmailService> s)
            {
                service = s;
            }
        }

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


            public bool Disposed;
            public void Dispose()
            {
                Disposed = true;
            }
        }
    }
}
