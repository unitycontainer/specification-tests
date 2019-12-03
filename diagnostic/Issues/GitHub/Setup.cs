using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Unity.Specification.Diagnostic.Issues.GitHub
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


        public bool Disposed = false;
        public void Dispose()
        {
            Disposed = true;
        }
    }

    public interface IInterface
    {
    }

    public class Class1 : IInterface
    {
        public void MyCompletelyUnambiguousInitializeMethod(bool arg)
        {
            Console.WriteLine($"Initialized: {arg}");
        }
    }

    public class Class2 : IInterface
    {
        public void AmbiguousInitializeMethod1(bool arg)
        {
            Console.WriteLine($"Initialized 1: {arg}");
        }
        public void AmbiguousInitializeMethod2(bool arg)
        {
            Console.WriteLine($"Initialized 2: {arg}");
        }
    }

    public class ATestClass
    {
        public ATestClass(IEnumerable<IInterface> interfaces)
        {
            Value = interfaces;
        }

        public IEnumerable<IInterface> Value { get; }
    }

    public interface ILogger
    {
    }

    public class ObjectWithLotsOfDependencies
    {
        private ILogger ctorLogger;
        private ObjectWithOneDependency dep1;
        private ObjectWithTwoConstructorDependencies dep2;
        private ObjectWithTwoProperties dep3;

        public ObjectWithLotsOfDependencies(ILogger logger, ObjectWithOneDependency dep1)
        {
            this.ctorLogger = logger;
            this.dep1 = dep1;
        }

        [Dependency]
        public ObjectWithTwoConstructorDependencies Dep2
        {
            get { return dep2; }
            set { dep2 = value; }
        }

        [InjectionMethod]
        public void InjectMe(ObjectWithTwoProperties dep3)
        {
            this.dep3 = dep3;
        }

        public void Validate()
        {
            Assert.IsNotNull(ctorLogger);
            Assert.IsNotNull(dep1);
            Assert.IsNotNull(dep2);
            Assert.IsNotNull(dep3);

            dep1.Validate();
            dep2.Validate();
            dep3.Validate();
        }

        public ILogger CtorLogger
        {
            get { return ctorLogger; }
        }

        public ObjectWithOneDependency Dep1
        {
            get { return dep1; }
        }

        public ObjectWithTwoProperties Dep3
        {
            get { return dep3; }
        }
    }

    public class ObjectWithOneDependency
    {
        private object inner;

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

    public class ObjectWithTwoProperties
    {
        private object obj1;
        private object obj2;

        [Dependency]
        public object Obj1
        {
            get { return obj1; }
            set { obj1 = value; }
        }

        [Dependency]
        public object Obj2
        {
            get { return obj2; }
            set { obj2 = value; }
        }

        public void Validate()
        {
            Assert.IsNotNull(obj1);
            Assert.IsNotNull(obj2);
            Assert.AreNotSame(obj1, obj2);
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

    public class TestClass
    {
        public TestClass(string field)
        {
            this.Field = field;
        }
        public string Field { get; }
    }

    #endregion
}
