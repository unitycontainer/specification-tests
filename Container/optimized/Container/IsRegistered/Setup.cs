using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;

namespace Unity.Specification.Container.IsRegistered
{
    public abstract partial class SpecificationTests : TestFixtureBase
    {
        private string other = "other";

        [TestInitialize]
        public override void Setup()
        {
            Container = GetContainer();


            Container.RegisterType<ILogger, MockLogger>();
            Container.RegisterType<ILogger, MockLogger>(Name);

            var service = new Service();
            Container.RegisterInstance<IService>(service);
            Container.RegisterInstance<IService>(Name, service);

            Container.RegisterType(typeof(IFoo<>), typeof(Foo<>));
            Container.RegisterType(typeof(IFoo<>), typeof(Foo<>), Name);
        }
    }


    #region Test Data

    public interface ILogger
    {
    }

    public class MockLogger : ILogger
    {
    }

    public class SpecialLogger : ILogger
    {
    }

    public interface IService
    {
    }

    public interface IGenericService<T>
    {
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

    #endregion
}

