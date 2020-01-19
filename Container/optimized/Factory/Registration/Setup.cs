using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;

namespace Unity.Specification.Factory.Registration
{
    public abstract partial class SpecificationTests : TestFixtureBase
    {
        [TestInitialize]
        public override void Setup()
        {
            Container = GetContainer();
        }
    }

    #region Test Data

    public interface IFoo<T> { }
    public class Foo<T> : IFoo<T> { }

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

    #endregion
}
