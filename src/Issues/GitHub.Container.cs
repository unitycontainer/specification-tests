using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Issues
{
    public abstract partial class SpecificationTests : TestFixtureBase
    {

        [TestMethod]
        public void unitycontainer_container_67()
        {
            var container = GetContainer();

            container.RegisterType<ILogger, MockLogger>(new TransientLifetimeManager());

            var child = container.CreateChildContainer();

            child.RegisterType<OtherService>(new TransientLifetimeManager());

            Assert.IsTrue(child.IsRegistered<ILogger>());
            Assert.IsFalse(child.IsRegistered<MockLogger>());
            Assert.IsTrue(child.IsRegistered<OtherService>());

            container.RegisterType<IOtherService, OtherService>();

            child = child.CreateChildContainer();

            Assert.IsTrue(child.IsRegistered<ILogger>());
            Assert.IsFalse(child.IsRegistered<MockLogger>());
            Assert.IsTrue(child.IsRegistered<IOtherService>());
            Assert.IsTrue(child.IsRegistered<OtherService>());
        }

    }
}
