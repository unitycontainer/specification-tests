using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Threading;
using Unity.Injection;

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


    public class ClassWithOneArrayGenericParameter<T>
    {
        private T[] injectedValue;
        public readonly bool DefaultConstructorCalled;

        public ClassWithOneArrayGenericParameter()
        {
            DefaultConstructorCalled = true;
        }

        public ClassWithOneArrayGenericParameter(T[] injectedValue)
        {
            DefaultConstructorCalled = false;

            this.injectedValue = injectedValue;
        }

        public T[] InjectedValue
        {
            get { return this.injectedValue; }
            set { this.injectedValue = value; }
        }
    }

    // Our various test objects
    public class ClassWithOneGenericParameter<T>
    {
        public T InjectedValue;

        public ClassWithOneGenericParameter(string s, object o)
        {
        }

        public ClassWithOneGenericParameter(T injectedValue)
        {
            InjectedValue = injectedValue;
        }
    }

    public class GenericTypeWithMultipleGenericTypeParameters<T, U>
    {
        private T theT;
        private U theU;
        public string Value;

        [InjectionConstructor]
        public GenericTypeWithMultipleGenericTypeParameters()
        {
        }

        public GenericTypeWithMultipleGenericTypeParameters(T theT)
        {
            this.theT = theT;
        }

        public GenericTypeWithMultipleGenericTypeParameters(U theU)
        {
            this.theU = theU;
        }

        public void Set(T theT)
        {
            this.theT = theT;
        }

        public void Set(U theU)
        {
            this.theU = theU;
        }

        public void SetAlt(T theT)
        {
            this.theT = theT;
        }

        public void SetAlt(string value)
        {
            this.Value = value;
        }
    }

    // Our generic interface 
    public interface ICommand<T>
    {
        void Execute(T data);
        void ChainedExecute(ICommand<T> inner);
    }

    // An implementation of ICommand that executes them.
    public class ConcreteCommand<T> : ICommand<T>
    {
        private object p = null;

        public void Execute(T data)
        {
        }

        public void ChainedExecute(ICommand<T> inner)
        {
        }

        public object NonGenericProperty
        {
            get { return p; }
            set { p = value; }
        }
    }

    // And a decorator implementation that wraps an Inner ICommand<>
    public class LoggingCommand<T> : ICommand<T>
    {
        private ICommand<T> inner;

        public bool ChainedExecuteWasCalled = false;
        public bool WasInjected = false;

        public LoggingCommand(ICommand<T> inner)
        {
            this.inner = inner;
        }

        public LoggingCommand()
        {
        }

        public ICommand<T> Inner
        {
            get { return inner; }
            set { inner = value; }
        }

        public void Execute(T data)
        {
            // do logging here
            Inner.Execute(data);
        }

        public void ChainedExecute(ICommand<T> innerCommand)
        {
            ChainedExecuteWasCalled = true;
        }

        public void InjectMe()
        {
            WasInjected = true;
        }
    }

    // Test class for lifetime and dispose with open generics
    public class DisposableCommand<T> : ICommand<T>, IDisposable
    {
        public bool Disposed { get; private set; }

        public void Execute(T data)
        {
        }

        public void ChainedExecute(ICommand<T> inner)
        {
        }

        public void Dispose()
        {
            Disposed = true;
        }
    }

    // A type with some nasty generics in the constructor
    public class Pathological<T1, T2>
    {
        public Pathological(ICommand<T2> cmd1, ICommand<T1> cmd2)
        {
        }

        public ICommand<T2> AProperty
        {
            get { return null; }
            set { }
        }
    }

    // A couple of sample objects we're stuffing into our commands
    public class User
    {
        public void DoSomething(string message)
        {
        }
    }

    public class Account
    {
    }

    public class ServiceNewConstraint<T> : IService<T> where T : new() { }

    public class TypeWithNoPublicNoArgCtors
    {
        public TypeWithNoPublicNoArgCtors(int _) { }
        private TypeWithNoPublicNoArgCtors() { }
    }

    public class ServiceInterfaceConstraint<T> : IService<T> where T : IEnumerable { }

    public class ServiceClass<T> : IService<T> where T : class { }

    public class ServiceStruct<T> : IService<T> where T : struct { }

    public interface IService<T> { }

    public class ServiceA<T> : IService<T> { }

    public class ServiceB<T> : IService<T> { }

    public class FooRepository : IRepository<Service>
    {
    }

    public interface IRepository<TEntity> { }

    public class Refer<TEntity>
    {
        private string str;

        public string Str
        {
            get { return str; }
            set { str = value; }
        }

        public Refer()
        {
            str = "Hello";
        }
    }

    public class MockRespository<TEntity> : IRepository<TEntity>
    {
        private Refer<TEntity> obj;

        [Dependency]
        public Refer<TEntity> Add
        {
            get { return obj; }
            set { obj = value; }
        }

        [InjectionConstructor]
        public MockRespository(Refer<TEntity> obj)
        { }
    }

    public class GenericArrayPropertyDependency<T>
    {
        public T[] Stuff { get; set; }
    }

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
