using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Lifetime;

namespace Unity.Specification.Lifetime
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void ThenResolvingInParentActsLikeContainerControlledLifetime()
        {
            Container.RegisterType<TestClass>(new HierarchicalLifetimeManager());

            var o1 = Container.Resolve<TestClass>();
            var o2 = Container.Resolve<TestClass>();
            Assert.AreSame(o1, o2);
        }

        [TestMethod]
        public void ThenParentAndChildResolveDifferentInstances()
        {
            var child1 = Container.CreateChildContainer();
            Container.RegisterType<TestClass>(new HierarchicalLifetimeManager());

            var o1 = Container.Resolve<TestClass>();
            var o2 = child1.Resolve<TestClass>();
            Assert.AreNotSame(o1, o2);
        }

        [TestMethod]
        public void ThenChildResolvesTheSameInstance()
        {
            var child1 = Container.CreateChildContainer();
            Container.RegisterType<TestClass>(new HierarchicalLifetimeManager());

            var o1 = child1.Resolve<TestClass>();
            var o2 = child1.Resolve<TestClass>();
            Assert.AreSame(o1, o2);
        }

        [TestMethod]
        public void ThenSiblingContainersResolveDifferentInstances()
        {
            var child1 = Container.CreateChildContainer();
            var child2 = Container.CreateChildContainer();
            Container.RegisterType<TestClass>(new HierarchicalLifetimeManager());

            var o1 = child1.Resolve<TestClass>();
            var o2 = child2.Resolve<TestClass>();
            Assert.AreNotSame(o1, o2);
        }

        [TestMethod]
        public void ThenDisposingOfChildContainerDisposesOnlyChildObject()
        {
            var child1 = Container.CreateChildContainer();
            Container.RegisterType<TestClass>(new HierarchicalLifetimeManager());

            var o1 = Container.Resolve<TestClass>();
            var o2 = child1.Resolve<TestClass>();

            child1.Dispose();
            Assert.IsFalse(o1.Disposed);
            Assert.IsTrue(o2.Disposed);
        }
    }
}
