using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Specification.TestData;

namespace Unity.Specification.Injection
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

}
