using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using Unity.Injection;
using Unity.Lifetime;

namespace Unity.Specification.Resolution.Generic
{
    public abstract partial class SpecificationTests : TestFixtureBase
    {
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();

            Container.RegisterType<IService, Service>("1");
            Container.RegisterType<IService, Service>("2");
            Container.RegisterType<IService, OtherService>("3");
            Container.RegisterType<IService, Service>();

            Service.Instances = 0;
        }
    }

    #region Test Data


    public interface IFoo<TEntity>
    {
        TEntity Value { get; }
    }

    public class Foo<TEntity> : IFoo<TEntity>
    {
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


        public bool Disposed;
        public void Dispose()
        {
            Disposed = true;
        }
    }

    #endregion
}
