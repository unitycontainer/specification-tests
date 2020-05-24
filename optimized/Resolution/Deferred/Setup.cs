using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Unity.Injection;

namespace Unity.Specification.Resolution.Deferred
{
    public abstract partial class SpecificationTests : TestFixtureBase
    {
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();

            Container.RegisterType(typeof(IList<>), typeof(List<>), new InjectionConstructor());
            Container.RegisterType(typeof(IFoo<>), typeof(Foo<>));
            Container.RegisterType<IService, Service>("1");
            Container.RegisterType<IService, Service>("2");
            Container.RegisterType<IService, OtherService>("3");
            Container.RegisterType<IService, Service>();

            Service.Instances = 0;
        }

        public static void AreEquivalent(ICollection expected, string[] actual)
        {
            if (expected == actual)
            {
                return;
            }

            if (expected.Count != actual.Length)
            {
                throw new AssertFailedException("collections differ in size");
            }

            var expectedCounts = expected.Cast<object>().GroupBy(e => e).ToDictionary(g => g.Key, g => g.Count());
            var actualCounts = actual.Cast<object>().GroupBy(e => e).ToDictionary(g => g.Key, g => g.Count());

            foreach (var kvp in expectedCounts)
            {
                int actualCount = 0;
                if (actualCounts.TryGetValue(kvp.Key, out actualCount))
                {
                    if (actualCount != kvp.Value)
                    {
                        throw new AssertFailedException(string.Format(System.Globalization.CultureInfo.InvariantCulture, "collections have different count for element {0}", kvp.Key));
                    }
                }
                else
                {
                    throw new AssertFailedException(string.Format(System.Globalization.CultureInfo.InvariantCulture, "actual does not contain element {0}", kvp.Key));
                }
            }
        }


        public class ObjectThatGetsAResolver
        {
            [Dependency]
            public Func<IService> LoggerResolver { get; set; }
        }

        public interface IFoo<TEntity>
        {
            TEntity Value { get; }
        }

        public interface IFoo { }
        public interface IFoo1 { }
        public interface IFoo2 { }

        public class Foo<TEntity> : IFoo<TEntity>
        {
            public Foo()
            {
            }

            public Foo(TEntity value)
            {
                Value = value;
            }

            public TEntity Value { get; }
        }

        public class Foo : IFoo, IFoo1, IFoo2
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

        public interface ITest1<T> { }

        public interface ITest2<T> { }

        public class Test<T> : ITest1<T>, ITest2<T>
        {
            public string Id { get; } = Guid.NewGuid().ToString();
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
    }
}
