using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Unity.Specification.Issues.GitHub
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


    public interface IFoo { }

    public interface IBar { }

    public class Bar : IBar { }

    public class Foo : IFoo 
    {
        private readonly string _dependency;

        public Foo([Dependency] string dependency)
        {
            _dependency = dependency;
        }

        public override string ToString() => _dependency;
    }

    public interface IZoo
    {

        IAnimal GetAnimal();
    }

    public class Zoo : IZoo
    {
        private readonly IAnimal _animal;

        public Zoo(IAnimal animal)
        {
            _animal = animal;
        }


        public IAnimal GetAnimal()
        {
            return _animal;
        }
    }

    public interface IAnimal
    {
        string Name { get; set; }
    }

    public class Cat : IAnimal
    {
        public string Name { get; set; }
    }

    public class Dog : IAnimal
    {

        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    public interface IGeneric<T>
    {
    }

    public interface IThing
    {
    }

    public class Thing : IThing
    {
        [InjectionConstructor]
        public Thing()
        {
        }

        public Thing(int i)
        {
        }
    }

    public class Gen1 : IGeneric<IThing>
    {
    }

    public class Gen2 : IGeneric<IThing>
    {
    }

    public interface ILogger
    {
    }

    public class MockLogger : ILogger
    {
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

    public class InvalidService : IService
    {
        public InvalidService()
        {
            throw new Exception("As Expected");
        }
    }

    public interface IOtherService
    {
    }

    public class Consumer
    {
        private readonly IEnumerable<IService> _interfaces;

        public Consumer(IEnumerable<IService> interfaces)
        {
            _interfaces = interfaces;
        }

        public void Consume()
        {
            _interfaces.ToArray();
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

    public interface IProctRepository
    {
        string Value { get; }
    }

    public class ProctRepository : IProctRepository
    {
        public string Value { get; }

        public ProctRepository(string base_name = "default.sqlite")
        {
            Value = base_name;
        }
    }

    public class ObjectWithThreeProperties
    {
        [Dependency]
        public string Name { get; set; }

        public object Property { get; set; }

        [Dependency]
        public IUnityContainer Container { get; set; }
    }

    #endregion
}