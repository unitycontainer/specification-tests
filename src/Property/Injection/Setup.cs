using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Property.Injection
{
    public abstract partial class SpecificationTests : TestFixtureBase
    {
        private const string Other = "other";

        [TestInitialize]
        public override void Setup()
        {
            base.Setup();

            Container.RegisterInstance(Name);
        }
    }

    #region Test Data

    public class ObjectWithThreeProperties
    {
        [Dependency]
        public string Name { get; set; }

        public object Property { get; set; }

        [Dependency]
        public IUnityContainer Container { get; set; }
    }

    public class ObjectWithFourProperties : ObjectWithThreeProperties
    {
        public object SubProperty { get; set; }

        public object ReadOnlyProperty { get; } 
    }

    public class ObjectWithDependency
    {
        public ObjectWithDependency(ObjectWithThreeProperties obj)
        {
            Dependency = obj;
        }

        public ObjectWithThreeProperties Dependency { get; }

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

    // InjectionParameterValue type used for testing nesting
    public struct Customer
    {
    }


    #endregion
}
