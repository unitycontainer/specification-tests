using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Unity.Specification.Injection.Constructor
{
    public abstract partial class SpecificationTests : TestFixtureBase
    {
        [TestInitialize]
        public override void Setup()
        {
            Container = GetContainer();
        }
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


    public class InjectionTestCollection<T>
    {
        public IGenericService<T> Printer { get; }

        public string CollectionName { get; }

        public InjectionTestCollection()
        {
            CollectionName = typeof(InjectionTestCollection<>).Name;
        }

        public InjectionTestCollection(string name)
        {

        }

        public InjectionTestCollection(string name, IGenericService<T> printService)
        {
            CollectionName = name;
            Printer = printService;
        }

        public InjectionTestCollection(string name, IGenericService<T> printService, T[] items)
            : this(name, printService)
        {
            Items = items;
        }

        public InjectionTestCollection(string name, IGenericService<T> printService, T[][] itemsArray)
            : this(name, printService, itemsArray.Length > 0 ? itemsArray[0] : null)
        { }



        public T[] Items { get; set; }
    }


    public class GenericInjectionTestClass<A, B, C>
    {
        public IGenericService<A> Printer { get; }

        public string CollectionName { get; }

        public GenericDependencyClass<A, C> GenDependency { get; }

        public GenericInjectionTestClass(string name, IGenericService<A> printService, B[] itemsArray, IEnumerable<C> enumerable)
        {
            CollectionName = name;
            Printer = printService;
            ItemsAll = itemsArray;
            Items = enumerable;
        }

        public GenericInjectionTestClass(GenericDependencyClass<A, C> genericDependency)
        {
            GenDependency = genericDependency;
        }

        public GenericInjectionTestClass(string name, IGenericService<A> printService)
        {
            CollectionName = name;
            Printer = printService;
        }

        public GenericInjectionTestClass(string name)
        {
            CollectionName = name;
        }

        public GenericInjectionTestClass()
        {
            CollectionName = typeof(InjectionTestCollection<>).Name;
        }
        public B[] ItemsAll { get; set; }

        public IEnumerable<C> Items { get; set; }
    }


    public class GenericDependencyClass<T, V>
    {
        public IGenericService<T> Printer { get; }

        public IEnumerable<V> Items { get; set; }
    }
}
