using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Method.Parameters
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

    // Our generic interface 
    public interface ICommand<T>
    {
        T Executed { get; }

        ICommand<T> Chained { get; }

        void Execute(T data);

        void ChainedExecute(ICommand<T> inner);
    }

    // An implementation of ICommand that executes them.
    public class ConcreteCommand<T> : ICommand<T>
    {
        public T Executed { get; private set; }

        public ICommand<T> Chained { get; private set; }


        public void Execute(T data)
        {
            Executed = data;
        }

        public void ChainedExecute(ICommand<T> inner)
        {
            Chained = inner;
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

        public T Executed { get; private set; }

        public ICommand<T> Chained { get; private set; }


        public ICommand<T> Inner
        {
            get { return inner; }
            set { inner = value; }
        }

        public void Execute(T data)
        {
            // do logging here
            Inner.Execute(data);
            Executed = data;
        }

        public void ChainedExecute(ICommand<T> innerCommand)
        {
            ChainedExecuteWasCalled = true;
            Chained = innerCommand;
        }

        public void InjectMe()
        {
            WasInjected = true;
        }
    }

    public class Account
    {
    }

    public class TypeWithMethodWithRefParameter
    {
        [InjectionMethod]
        public void MethodWithRefParameter(ref string ignored)
        {
        }

        public int Property { get; set; }
    }

    public class TypeWithMethodWithOutParameter
    {
        [InjectionMethod]
        public void MethodWithOutParameter(out string ignored)
        {
            ignored = null;
        }

        public int Property { get; set; }
    }

    public interface I1 { }
    public interface I2 { }

    public class C1 : I1 { public C1(I2 i2) { } }

    public class C2 : I2 { public C2(I1 i1) { } }

    #endregion
}
