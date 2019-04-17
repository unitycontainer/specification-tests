using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;

namespace Unity.Specification.Resolution.Array
{
    public abstract partial class SpecificationTests : TestFixtureBase
    {
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
        }
    }
    #region Test Data

    public class EmptyClass
    {
    }

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

    public interface IOtherService
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

    #endregion
}
