using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Issues.GitHub
{
    public abstract partial class SpecificationTests : TestFixtureBase
    {
        [TestMethod]
        // https://github.com/unitycontainer/container/issues/67
        public void Container_67()
        {
            Container.RegisterType<ILogger, MockLogger>(new TransientLifetimeManager());

            var child = Container.CreateChildContainer();

            child.RegisterType<OtherService>(new TransientLifetimeManager());

            Assert.IsTrue(child.IsRegistered<ILogger>());
            Assert.IsFalse(child.IsRegistered<MockLogger>());
            Assert.IsTrue(child.IsRegistered<OtherService>());

            Container.RegisterType<IOtherService, OtherService>();

            child = child.CreateChildContainer();

            Assert.IsTrue(child.IsRegistered<ILogger>());
            Assert.IsFalse(child.IsRegistered<MockLogger>());
            Assert.IsTrue(child.IsRegistered<IOtherService>());
            Assert.IsTrue(child.IsRegistered<OtherService>());
        }

    }
}
