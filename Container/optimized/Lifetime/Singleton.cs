using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Unity.Specification.Lifetime
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void Singleton_Instance_Null()
        {
            // Arrange
            Container.RegisterInstance(typeof(IService), null, null, InstanceLifetime.Singleton);

            // Act
            var instance = Container.Resolve<IService>();

            // Validate
            Assert.IsNull(instance);
            Assert.IsNull(Container.Resolve<IService>());
        }

        [TestMethod]
        public void Singleton_Factory_Null()
        {
            // Arrange
            Container.RegisterFactory<IService>(c => null, FactoryLifetime.Singleton);

            // Act
            var instance = Container.Resolve<IService>();

            // Validate
            Assert.IsNull(instance);
            Assert.IsNull(Container.Resolve<IService>());
        }

        [TestMethod]
        public void Singleton_ResolveInRootContainer()
        {
            // Arrange
            Container.RegisterType(typeof(ISingletonService), typeof(SingletonService), TypeLifetime.Singleton);
            Container.RegisterType(typeof(ITestElement), typeof(TestElement));
            Container.RegisterType(typeof(ITestElementFactory), typeof(TestElementFactory));

            var rootContainerId = Container.GetHashCode();
            var childContainer1 = Container.CreateChildContainer();
            var childContainer2 = Container.CreateChildContainer();

            // Act
            var reference1 = Container.Resolve<ISingletonService>();
            var reference2 = childContainer1.Resolve<ISingletonService>();
            var reference3 = childContainer2.Resolve<ISingletonService>();

            // Validate
            Assert.AreEqual(reference1, reference2, "reference1 and reference2 must be same");
            Assert.AreEqual(reference1, reference3, "reference1 and reference3 must be same");

            Assert.AreEqual(rootContainerId, reference1.ContainerId, "Instance by reference1 should be created in root container");
            Assert.AreEqual(rootContainerId, reference2.ContainerId, "Instance by reference2 should be created in root container");
            Assert.AreEqual(rootContainerId, reference3.ContainerId, "Instance by reference3 should be created in root container");
        }

        [TestMethod]
        public void Singleton_ResolveInChildContainer()
        {
            // Arrange
            Container.RegisterType(typeof(ISingletonService), typeof(SingletonService), TypeLifetime.Singleton);
            Container.RegisterType(typeof(ITestElement), typeof(TestElement));
            Container.RegisterType(typeof(ITestElementFactory), typeof(TestElementFactory));

            var rootContainerId = Container.GetHashCode();
            var childContainer1 = Container.CreateChildContainer();
            var childContainer2 = Container.CreateChildContainer();

            // Act
            var reference1 = childContainer1.Resolve<ISingletonService>();
            var reference2 = childContainer2.Resolve<ISingletonService>();
            var reference3 = Container.Resolve<ISingletonService>();

            // Validate
            Assert.AreEqual(reference1, reference2, "reference1 and reference2 must be same");
            Assert.AreEqual(reference1, reference3, "reference1 and reference3 must be same");

            Assert.AreEqual(rootContainerId, reference1.ContainerId, "Instance by reference1 should be created in root container");
            Assert.AreEqual(rootContainerId, reference2.ContainerId, "Instance by reference2 should be created in root container");
            Assert.AreEqual(rootContainerId, reference3.ContainerId, "Instance by reference3 should be created in root container");
        }

        [TestMethod]
        public void Singleton_AsFactoryInRootContainer()
        {
            // Arrange
            Container.RegisterType(typeof(ISingletonService), typeof(SingletonService), TypeLifetime.Singleton);
            Container.RegisterType(typeof(ITestElement), typeof(TestElement));
            Container.RegisterType(typeof(ITestElementFactory), typeof(TestElementFactory));

            var rootContainerId = Container.GetHashCode();
            var childContainer1 = Container.CreateChildContainer();
            var childContainer2 = Container.CreateChildContainer();

            // Act
            var reference1 = Container.Resolve<ISingletonService>();
            var reference2 = childContainer1.Resolve<ISingletonService>();
            var reference3 = childContainer2.Resolve<ISingletonService>();

            var itemsFrom1 = reference1.GetElements();
            var itemsFrom2 = reference1.GetElements();
            var itemsFrom3 = reference1.GetElements();

            // Validate
            Assert.IsTrue(itemsFrom1.All(i => i.ContainerId == rootContainerId), "Not all items from instance by reference1 are created in root container");
            Assert.IsTrue(itemsFrom2.All(i => i.ContainerId == rootContainerId), "Not all items from instance by reference2 are created in root container");
            Assert.IsTrue(itemsFrom3.All(i => i.ContainerId == rootContainerId), "Not all items from instance by reference3 are created in root container");
        }

        [TestMethod]
        public void Signleton_AsFactoryInChildContainer()
        {
            // Arrange
            Container.RegisterType(typeof(ISingletonService), typeof(SingletonService), TypeLifetime.Singleton);
            Container.RegisterType(typeof(ITestElement), typeof(TestElement));
            Container.RegisterType(typeof(ITestElementFactory), typeof(TestElementFactory));

            var rootContainerId = Container.GetHashCode();
            var childContainer1 = Container.CreateChildContainer();
            var childContainer2 = Container.CreateChildContainer();

            // Act
            var reference1 = childContainer1.Resolve<ISingletonService>();
            var reference2 = childContainer2.Resolve<ISingletonService>();
            var reference3 = Container.Resolve<ISingletonService>();

            var itemsFrom1 = reference1.GetElements();
            var itemsFrom2 = reference1.GetElements();
            var itemsFrom3 = reference1.GetElements();

            // Validate
            Assert.IsTrue(itemsFrom1.All(i => i.ContainerId == rootContainerId), "Not all items from instance by reference1 are created in root container");
            Assert.IsTrue(itemsFrom2.All(i => i.ContainerId == rootContainerId), "Not all items from instance by reference2 are created in root container");
            Assert.IsTrue(itemsFrom3.All(i => i.ContainerId == rootContainerId), "Not all items from instance by reference3 are created in root container");
        }

        [TestMethod]
        public void Signleton_AsFactory_WHEN_ItIsResolvedInRootContainer_AND_ChildContainerIsDisposed_THEN_ItWorksFromRootContainer()
        {
            // Arrange
            Container.RegisterType(typeof(ISingletonService), typeof(SingletonService), TypeLifetime.Singleton);
            Container.RegisterType(typeof(ITestElement), typeof(TestElement));
            Container.RegisterType(typeof(ITestElementFactory), typeof(TestElementFactory));

            Container.Resolve<ISingletonService>();

            var childContainer = Container.CreateChildContainer();

            var singleton = childContainer.Resolve<ISingletonService>();
            childContainer.Dispose();

            var items = singleton.GetElements();

            Assert.AreEqual(10, items.Count(), "Unexpected items count");
        }


        [TestMethod]
        public void Signleton_AsFactory_WHEN_ItIsResolvedInChildContainer_AND_ChildContainerIsDisposed_THEN_ItWorksFromRootContainer()
        {
            // Arrange
            Container.RegisterType(typeof(ISingletonService), typeof(SingletonService), TypeLifetime.Singleton);
            Container.RegisterType(typeof(ITestElement), typeof(TestElement));
            Container.RegisterType(typeof(ITestElementFactory), typeof(TestElementFactory));


            var childContainer = Container.CreateChildContainer();

            var singleton = childContainer.Resolve<ISingletonService>();
            childContainer.Dispose();

            var items = singleton.GetElements();

            Assert.AreEqual(10, items.Count(), "Unexpected items count");
        }


        [TestMethod]
        public void Singleton_Disposing_WHEN_ItIsResolvedInChildContainer_AND_ChildContainerIsDisposed_THEN_ItIs_NotDisposed()
        {
            // Arrange
            Container.RegisterType(typeof(ISingletonService), typeof(SingletonService), TypeLifetime.Singleton);
            Container.RegisterType(typeof(ITestElement), typeof(TestElement));
            Container.RegisterType(typeof(ITestElementFactory), typeof(TestElementFactory));


            var childContainer = Container.CreateChildContainer();
            var singleton = childContainer.Resolve<ISingletonService>();

            childContainer.Dispose();

            Assert.IsFalse(singleton.IsDisposed, "Singleton instance should not be disposed");
        }

        [TestMethod]
        public void Singleton_Disposing_WHEN_ItIsResolvedInChildContainer_AND_RootContainerIsDisposed_THEN_ItIs_Disposed()
        {
            // Arrange
            Container.RegisterType(typeof(ISingletonService), typeof(SingletonService), TypeLifetime.Singleton);
            Container.RegisterType(typeof(ITestElement), typeof(TestElement));
            Container.RegisterType(typeof(ITestElementFactory), typeof(TestElementFactory));


            var childContainer = Container.CreateChildContainer();
            var singleton = childContainer.Resolve<ISingletonService>();

            Container.Dispose();

            Assert.IsTrue(singleton.IsDisposed, "Singleton instance should be disposed");
        }

        [TestMethod]
        public void Singleton_Disposing_WHEN_ItIsResolvedInRootContainer_AND_ChildContainerIsDisposed_THEN_ItIs_NotDisposed()
        {
            // Arrange
            Container.RegisterType(typeof(ISingletonService), typeof(SingletonService), TypeLifetime.Singleton);
            Container.RegisterType(typeof(ITestElement), typeof(TestElement));
            Container.RegisterType(typeof(ITestElementFactory), typeof(TestElementFactory));

            Container.Resolve<ISingletonService>();

            var childContainer = Container.CreateChildContainer();
            var singleton = childContainer.Resolve<ISingletonService>();

            childContainer.Dispose();

            Assert.IsFalse(singleton.IsDisposed, "Singleton instance should not be disposed");
        }

        [TestMethod]
        public void Singleton_Disposing_WHEN_ItIsResolvedInRootContainer_AND_RootContainerIsDisposed_THEN_ItIs_Disposed()
        {
            // Arrange
            Container.RegisterType(typeof(ISingletonService), typeof(SingletonService), TypeLifetime.Singleton);
            Container.RegisterType(typeof(ITestElement), typeof(TestElement));
            Container.RegisterType(typeof(ITestElementFactory), typeof(TestElementFactory));


            var childContainer = Container.CreateChildContainer();
            var singleton = childContainer.Resolve<ISingletonService>();

            Container.Dispose();

            Assert.IsTrue(singleton.IsDisposed, "Singleton instance should be disposed");
        }


        [TestMethod]
        public void Singleton_CreatedDependenciesDisposing_WHEN_ItIsResolvedInRootContainer_AND_ChildContainerIsDisposed_THEN_ItDependencies_NotDisposed()
        {
            // Arrange
            Container.RegisterType(typeof(ISingletonService), typeof(SingletonService), TypeLifetime.Singleton);
            Container.RegisterType(typeof(ITestElement), typeof(TestElement));
            Container.RegisterType(typeof(ITestElementFactory), typeof(TestElementFactory));

            Container.Resolve<ISingletonService>();

            var childContainer = Container.CreateChildContainer();
            var singleton = childContainer.Resolve<ISingletonService>();
            var items = singleton.GetElements();

            childContainer.Dispose();

            Assert.IsFalse(items.Any(i => i.IsDisposed), "Items created by singleton should not be disposed");
        }

        [TestMethod]
        public void Singleton_CreatedDependenciesDisposing_WHEN_ItIsResolvedInChildContainer_AND_ChildContainerIsDisposed_THEN_ItDependencies_NotDisposed()
        {
            // Arrange
            Container.RegisterType(typeof(ISingletonService), typeof(SingletonService), TypeLifetime.Singleton);
            Container.RegisterType(typeof(ITestElement), typeof(TestElement));
            Container.RegisterType(typeof(ITestElementFactory), typeof(TestElementFactory));


            var childContainer = Container.CreateChildContainer();
            var singleton = childContainer.Resolve<ISingletonService>();
            var items = singleton.GetElements();

            childContainer.Dispose();

            Assert.IsFalse(items.Any(i => i.IsDisposed), "Items created by singleton should not be disposed");
        }
    }
}
