using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Unity.Specification.Lifetime
{
    public abstract partial class SpecificationTests : TestFixtureBase
    {
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
        }


        private void ThreadProcedure(object o)
        {
            ThreadInformation info = o as ThreadInformation;

            info.SetThreadResult(Thread.CurrentThread, info.Container.Resolve<IService>());
        }

        public class ThreadInformation
        {
            private readonly object dictLock = new object();

            public ThreadInformation(IUnityContainer container)
            {
                Container = container;
                ThreadResults = new Dictionary<Thread, IService>();
            }

            public IUnityContainer Container { get; }

            public Dictionary<Thread, IService> ThreadResults { get; }

            public void SetThreadResult(Thread t, IService result)
            {
                lock (dictLock)
                {
                    ThreadResults.Add(t, result);
                }
            }
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

    public class TestClass : IDisposable
    {
        public bool Disposed { get; private set; }

        public void Dispose()
        {
            Disposed = true;
        }
    }

    public interface IService { }

    public class Service : IService { }

    public class OtherService : IService { }

    public interface IPresenter { }

    public class MockPresenter : IPresenter
    {
        public IView View { get; set; }

        public MockPresenter(IView view)
        {
            View = view;
        }
    }

    public interface IView
    {
        IPresenter Presenter { get; set; }
    }

    public class View : IView
    {
        [Dependency]
        public IPresenter Presenter { get; set; }
    }

    public class SomeService { }

    public class SomeOtherService
    {
        public SomeService SomeService { get; set; }
        public SomeOtherService(SomeService someService)
        {
            this.SomeService = someService;
        }
    }

    public class AService
    {
        public AService(SomeOtherService otherService)
        {
            this.OtherService = otherService;
        }

        [Dependency]
        public SomeService SomeService { get; set; }

        public SomeOtherService OtherService { get; set; }
    }

    public class ObjectWithOneDependency
    {
        private object inner;
        public string id = Guid.NewGuid().ToString();

        public ObjectWithOneDependency(object inner)
        {
            this.inner = inner;
        }

        public object InnerObject
        {
            get { return inner; }
        }

        public void Validate()
        {
            Assert.IsNotNull(inner);
        }
    }

    public class ObjectWithTwoConstructorDependencies
    {
        private ObjectWithOneDependency oneDep;

        public ObjectWithTwoConstructorDependencies(ObjectWithOneDependency oneDep)
        {
            this.oneDep = oneDep;
        }

        public ObjectWithOneDependency OneDep
        {
            get { return oneDep; }
        }

        public void Validate()
        {
            Assert.IsNotNull(oneDep);
            oneDep.Validate();
        }
    }

    #endregion
}
