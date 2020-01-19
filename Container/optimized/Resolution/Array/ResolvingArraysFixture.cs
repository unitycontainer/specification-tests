using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Unity.Injection;

namespace Unity.Specification.Resolution.Array
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void ContainerCanResolveListOfT()
        {
            // Arrange
            Container.RegisterType(typeof(List<>), Invoke.Constructor());

            // Act
            var result = Container.Resolve<List<EmptyClass>>();

            // Validate
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ContainerReturnsEmptyArrayIfNoObjectsRegistered()
        {
            // Act
            var result = Container.Resolve<object[]>();

            // Validate
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void ResolveReturnsRegisteredObjects()
        {
            // Arrange
            object o1 = new object();
            object o2 = new object();

            Container.RegisterInstance("o1", o1)
                     .RegisterInstance("o2", o2);

            // Act
            var result = Container.Resolve<object[]>();

            // Validate
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Length);
            Assert.AreSame(o1, result[0]);
            Assert.AreSame(o2, result[1]);
        }

        [TestMethod]
        public void ResolveAllReturnsRegisteredObjects()
        {
            // Arrange
            object o1 = new object();
            object o2 = new object();

            Container.RegisterInstance("o1", o1)
                     .RegisterInstance("o2", o2);

            // Act
            var result = Container.ResolveAll<object>()
                                  .ToArray();

            // Validate
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Length);
            Assert.AreSame(o1, result[0]);
            Assert.AreSame(o2, result[1]);
        }

        [TestMethod]
        public void ResolveReturnsRegisteredObjectsForBaseClass()
        {
            // Arrange
            IService o1 = new Service();
            IService o2 = new OtherService();

            Container.RegisterInstance("o1", o1)
                     .RegisterInstance("o2", o2);

            // Act
            var result = Container.Resolve<IService[]>();

            // Validate
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Length);
            Assert.AreSame(o1, result[0]);
            Assert.AreSame(o2, result[1]);
        }

        [TestMethod]
        public void ResolveAllReturnsRegisteredObjectsForBaseClass()
        {
            // Arrange
            IService o1 = new Service();
            IService o2 = new OtherService();

            Container.RegisterInstance("o1", o1)
                     .RegisterInstance("o2", o2);

            // Act
            var result = Container.ResolveAll<IService>()
                                  .ToArray();

            // Validate
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Length);
            Assert.AreSame(o1, result[0]);
            Assert.AreSame(o2, result[1]);
        }

        [TestMethod]
        public void ResolverWithElementsReturnsEmptyArrayIfThereAreNoElements()
        {
            // Arrange
            object o1 = new object();
            object o2 = new object();

            Container.RegisterInstance("o1", o1)
                     .RegisterInstance("o2", o2)

                     .RegisterType<InjectedObject>(Invoke.Constructor(Inject.Array<object>()))

                     .RegisterType<InjectedObject>(Legacy, 
                        new InjectionConstructor(new ResolvedArrayParameter(typeof(object))));

            // Act
            var result = (object[])Container.Resolve<InjectedObject>().InjectedValue;
            var legacy = (object[])Container.Resolve<InjectedObject>(Legacy).InjectedValue;

            // Validate
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Length);

            Assert.IsNotNull(legacy);
            Assert.AreEqual(0, legacy.Length);
        }

        [TestMethod]
        public void ResolverWithElementsReturnsLiteralElements()
        {
            // Arrange
            object o1 = new object();
            object o2 = new object();
            object o3 = new object();

            Container.RegisterInstance("o1", o1)
                     .RegisterInstance("o2", o2)
                     
                     .RegisterType<InjectedObject>(
                        Invoke.Constructor(Inject.Parameter(new object[] { o1, o3 })))

                     .RegisterType<InjectedObject>(Legacy, 
                        new InjectionConstructor(
                            new ResolvedArrayParameter(typeof(object),
                                new InjectionParameter(typeof(object), o1), 
                                new InjectionParameter(typeof(object), o3) )));

            // Act
            var result = (object[])Container.Resolve<InjectedObject>().InjectedValue;
            var legacy  = (object[])Container.Resolve<InjectedObject>().InjectedValue;

            // Validate
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Length);
            Assert.AreSame(o1, result[0]);
            Assert.AreSame(o3, result[1]);

            Assert.IsNotNull(legacy);
            Assert.AreEqual(2, legacy.Length);
            Assert.AreSame(o1, legacy[0]);
            Assert.AreSame(o3, legacy[1]);
        }

        [TestMethod]
        public void ResolverWithElementsReturnsResolvedElements()
        {
            // Arrange
            object o1 = new object();
            object o2 = new object();
            object o3 = new object();

            Container.RegisterInstance("o1", o1)
                     .RegisterInstance("o2", o2)
                     .RegisterInstance("o3", o3)

                     .RegisterType<InjectedObject>(
                        Invoke.Constructor(
                            Inject.Array(typeof(object),
                                Resolve.Dependency<object>("o1"),
                                Resolve.Dependency<object>("o2") )))

                     .RegisterType<InjectedObject>(Legacy,
                        new InjectionConstructor(
                            new ResolvedArrayParameter(typeof(object),
                                new ResolvedParameter<object>("o1"),
                                new ResolvedParameter<object>("o2") )));
            // Act
            var result = (object[])Container.Resolve<InjectedObject>().InjectedValue;
            var legacy = (object[])Container.Resolve<InjectedObject>().InjectedValue;

            // Validate
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Length);
            Assert.AreSame(o1, result[0]);
            Assert.AreSame(o2, result[1]);

            Assert.IsNotNull(legacy);
            Assert.AreEqual(2, legacy.Length);
            Assert.AreSame(o1, legacy[0]);
            Assert.AreSame(o2, legacy[1]);
        }

        [TestMethod]
        public void ResolverWithElementsReturnsResolvedElementsForBaseClass()
        {
            // Arrange
            ILogger o1 = new MockLogger();
            ILogger o2 = new SpecialLogger();

            Container.RegisterInstance("o1", o1)
                     .RegisterInstance("o2", o2)
                     .RegisterType<InjectedObject>(Invoke.Constructor(typeof(ILogger[])))
                     .RegisterType<InjectedObject>(Legacy, new InjectionConstructor(typeof(ILogger[])));

            // Act
            var result = (ILogger[])Container.Resolve<InjectedObject>().InjectedValue;
            var legacy = (object[])Container.Resolve<InjectedObject>().InjectedValue;

            // Validate
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Length);
            Assert.AreSame(o1, result[0]);
            Assert.AreSame(o2, result[1]);

            Assert.IsNotNull(legacy);
            Assert.AreEqual(2, legacy.Length);
            Assert.AreSame(o1, legacy[0]);
            Assert.AreSame(o2, legacy[1]);
        }
    }
}
