using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Unity.Specification.Container.IsRegistered
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void IUnityContainer()
        {
            var registrations = Container.Registrations;
            Assert.IsNotNull(registrations.FirstOrDefault(r => r.RegisteredType == typeof(IUnityContainer)));
        }

        [TestMethod]
        public void ContainerListsItselfAsRegistered()
        {
            Assert.IsTrue(Container.IsRegistered(typeof(IUnityContainer)));
        }

        [TestMethod]
        public void ContainerDoesNotListItselfUnderNonDefaultName()
        {
            Assert.IsFalse(Container.IsRegistered(typeof(IUnityContainer), other));
        }

        [TestMethod]
        public void ContainerListsItselfAsRegisteredUsingGenericOverload()
        {
            Assert.IsTrue(Container.IsRegistered<IUnityContainer>());
        }

        [TestMethod]
        public void ContainerDoesNotListItselfUnderNonDefaultNameUsingGenericOverload()
        {
            Assert.IsFalse(Container.IsRegistered<IUnityContainer>(other));
        }
    }
}
