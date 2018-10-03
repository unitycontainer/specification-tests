using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Unity.Specification.TestData;

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
