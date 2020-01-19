using System;
using System.Collections.Generic;
using System.Threading;

namespace Unity.Specification.Constructor.Injection
{
    public abstract partial class SpecificationTests
    {
        private class TestClass
        {
            public TestClass() { }                                                    // 0
            public TestClass(object first) { }                                        // 1
            public TestClass(int first, string second, string third) { }              // 2
            public TestClass(Type first, Type second, Type third) { }                 // 3
            public TestClass(int first, string second, double third) { }              // 4
            public TestClass(string first, string second, string third) { }           // 5
            public TestClass(string first, string second, IUnityContainer third) { }  // 6
        }

        public class GenericTestClass<TA, TB, TC>
        {
            public TB CollectionName { get; }

            public GenericDependencyClass<TA, TC> GenDependency { get; }

            public GenericTestClass(TB name)
            {
                CollectionName = name;
            }

            public GenericTestClass(GenericDependencyClass<TA, TC> genericDependency)
            {
                GenDependency = genericDependency;
            }

            public GenericTestClass()
            {

            }
        }

        public interface IGenericService<T>
        {
        }

        public class OtherService : IService, IDisposable
        {
            public string ID { get; } = Guid.NewGuid().ToString();

            public static int Instances = 0;

            public OtherService()
            {
                Interlocked.Increment(ref Instances);
            }

            public bool Disposed = false;
            public void Dispose()
            {
                Disposed = true;
            }
        }

        public class TypeWithMultipleCtors
        {
            public const string One = "1";
            public const string Two = "2";
            public const string Three = "3";
            public const string Four = "4";
            public const string Five = "5";

            public string Signature { get; }

            public TypeWithMultipleCtors()
            {
                Signature = One;
            }

            public TypeWithMultipleCtors(int first, string second, float third)
            {
                Signature = Two;
            }

            public TypeWithMultipleCtors(Type first, Type second, Type third)
            {
                Signature = Three;
            }

            public TypeWithMultipleCtors(string first, string second, string third)
            {
                Signature = first;
            }

            public TypeWithMultipleCtors(string first, [Dependency(Five)]string second, IUnityContainer third)
            {
                Signature = second;
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
}
