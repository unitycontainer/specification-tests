using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Registrations.IsRegistered
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void ContainerListsItselfAsRegistered()
        {
            Assert.IsTrue(Container.IsRegistered(typeof(IUnityContainer)));
        }

        [TestMethod]
        public void ContainerDoesNotListItselfUnderNonDefaultName()
        {
            Assert.IsFalse(Container.IsRegistered(typeof(IUnityContainer), "other"));
        }

        [TestMethod]
        public void ContainerListsItselfAsRegisteredUsingGenericOverload()
        {
            Assert.IsTrue(Container.IsRegistered<IUnityContainer>());
        }

        [TestMethod]
        public void ContainerDoesNotListItselfUnderNonDefaultNameUsingGenericOverload()
        {
            Assert.IsFalse(Container.IsRegistered<IUnityContainer>("other"));
        }

        [TestMethod]
        public void IsRegisteredWorksForRegisteredType()
        {
            Container.RegisterType<ILogger, MockLogger>();

            Assert.IsTrue(Container.IsRegistered<ILogger>());
        }

    }
}
