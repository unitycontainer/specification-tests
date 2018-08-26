using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Unity.Specification.TestData;

namespace Unity.Specification.Registration
{
    public abstract partial class SpecificationTests
    {
        private string other = "other";

        [TestMethod]
        public void IsRegistered()
        {
            Assert.IsTrue( Container.IsRegistered(typeof(IUnityContainer)));
            Assert.IsTrue(Container.IsRegistered(typeof(IUnityContainer), string.Empty));
            Assert.IsFalse(Container.IsRegistered(typeof(IUnityContainer), Name));
        }

        [TestMethod]
        public void IsRegistered_GenericOverload()
        {
            Assert.IsTrue(Container.IsRegistered<IUnityContainer>());
            Assert.IsTrue(Container.IsRegistered<IUnityContainer>(string.Empty));
            Assert.IsFalse(Container.IsRegistered<IUnityContainer>(Name));
        }

        [TestMethod]
        public void IsRegistered_Type()
        {
            Assert.IsTrue(Container.IsRegistered(typeof(ILogger)));
            Assert.IsTrue(Container.IsRegistered(typeof(ILogger), Name));
            Assert.IsFalse(Container.IsRegistered(typeof(ILogger), other));
        }

        [TestMethod]
        public void IsRegistered_Instance()
        {
            Assert.IsTrue(Container.IsRegistered(typeof(IService)));
            Assert.IsTrue(Container.IsRegistered(typeof(IService), Name));
            Assert.IsFalse(Container.IsRegistered(typeof(IService), other));
        }

        [TestMethod]
        public void IsRegistered_Generic()
        {
            Assert.IsTrue(Container.IsRegistered(typeof(IFoo<>)));
            Assert.IsTrue(Container.IsRegistered(typeof(IFoo<>), Name));
            Assert.IsFalse(Container.IsRegistered(typeof(IFoo<>), other));
        }

        [TestMethod]
        public void IsRegistered_FromChildContainer()
        {
            var child = Container.CreateChildContainer();

            Assert.IsTrue(child.IsRegistered(typeof(ILogger)));
            Assert.IsTrue(child.IsRegistered(typeof(ILogger), Name));
            Assert.IsFalse(child.IsRegistered(typeof(ILogger), other));
            Assert.IsTrue(child.IsRegistered(typeof(IService)));
            Assert.IsTrue(child.IsRegistered(typeof(IService), Name));
            Assert.IsFalse(child.IsRegistered(typeof(IService), other));
            Assert.IsTrue(child.IsRegistered(typeof(IFoo<>)));
            Assert.IsTrue(child.IsRegistered(typeof(IFoo<>), Name));
            Assert.IsFalse(child.IsRegistered(typeof(IFoo<>), other));
        }

        [TestMethod]
        public void IsRegistered_FromChildChildContainer()
        {
            var child = Container.CreateChildContainer()
                                  .CreateChildContainer();

            Assert.IsTrue(child.IsRegistered(typeof(ILogger)));
            Assert.IsTrue(child.IsRegistered(typeof(ILogger), Name));
            Assert.IsFalse(child.IsRegistered(typeof(ILogger), other));
            Assert.IsTrue(child.IsRegistered(typeof(IService)));
            Assert.IsTrue(child.IsRegistered(typeof(IService), Name));
            Assert.IsFalse(child.IsRegistered(typeof(IService), other));
            Assert.IsTrue(child.IsRegistered(typeof(IFoo<>)));
            Assert.IsTrue(child.IsRegistered(typeof(IFoo<>), Name));
            Assert.IsFalse(child.IsRegistered(typeof(IFoo<>), other));
        }

        [TestMethod]
        public void IsRegistered_IUnityContainer()
        {
            Assert.IsNotNull(Container.Registrations.FirstOrDefault(r => r.RegisteredType == typeof(IUnityContainer)));
        }
    }
}
