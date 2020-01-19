using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Unity.Specification.Instance
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void SingletonAtRoot()
        {
            // Arrange
            var service = Unresolvable.Create();

            var child1 = Container.CreateChildContainer();
            var child2 = child1.CreateChildContainer();

            Container.RegisterInstance(typeof(IService), null, service, InstanceLifetime.Singleton);


            // Act/Verify

            Assert.AreSame(service, Container.Resolve<IService>());
            Assert.AreSame(service, child1.Resolve<IService>());
            Assert.AreSame(service, child2.Resolve<IService>());
        }

        [TestMethod]
        public void SingletonAtChild()
        {
            // Arrange
            var service = Unresolvable.Create();

            var child1 = Container.CreateChildContainer();
            var child2 = child1.CreateChildContainer();

            child1.RegisterInstance(typeof(IService), null, service, InstanceLifetime.Singleton);


            // Act/Verify

            Assert.AreSame(service, Container.Resolve<IService>());
            Assert.AreSame(service, child1.Resolve<IService>());
            Assert.AreSame(service, child2.Resolve<IService>());
        }

        [TestMethod]
        public void PerContainerAtRoot()
        {
            // Arrange
            var service = Unresolvable.Create();

            var child1 = Container.CreateChildContainer();
            var child2 = child1.CreateChildContainer();

            Container.RegisterInstance(typeof(IService), null, service, InstanceLifetime.PerContainer);


            // Act/Verify

            Assert.AreSame(service, Container.Resolve<IService>());
            Assert.AreSame(service, child1.Resolve<IService>());
            Assert.AreSame(service, child2.Resolve<IService>());
        }

        [TestMethod]
        public void PerContainerAtChild()
        {
            // Arrange
            var service = Unresolvable.Create();

            var child1 = Container.CreateChildContainer();
            var child2 = child1.CreateChildContainer();

            Container.RegisterInstance(typeof(IService), null, Unresolvable.Create(), InstanceLifetime.PerContainer);
            child1.RegisterInstance(typeof(IService), null, Unresolvable.Create(), InstanceLifetime.PerContainer);
            child2.RegisterInstance(typeof(IService), null, Unresolvable.Create(), InstanceLifetime.PerContainer);


            // Act/Verify

            Assert.AreNotSame(service, Container.Resolve<IService>());
            Assert.AreNotSame(service, child1.Resolve<IService>());
            Assert.AreNotSame(service, child2.Resolve<IService>());
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void PerContainerThrows()
        {
            // Arrange
            var child1 = Container.CreateChildContainer();

            child1.RegisterInstance(typeof(IService), null, Unresolvable.Create(), InstanceLifetime.PerContainer);

            // Act/Verify
            var result = Container.Resolve<IService>();
        }


        [TestMethod]
        public void ExternalAtRoot()
        {
            // Arrange
            var service = Unresolvable.Create();

            var child1 = Container.CreateChildContainer();
            var child2 = child1.CreateChildContainer();

            Container.RegisterInstance(typeof(IService), null, service, InstanceLifetime.External);


            // Act/Verify

            Assert.AreSame(service, Container.Resolve<IService>());
            Assert.AreSame(service, child1.Resolve<IService>());
            Assert.AreSame(service, child2.Resolve<IService>());
        }

        [TestMethod]
        public void ExternalAtChild()
        {
            // Arrange
            var service = Unresolvable.Create();

            var child1 = Container.CreateChildContainer();
            var child2 = child1.CreateChildContainer();

            Container.RegisterInstance(typeof(IService), null, Unresolvable.Create(), InstanceLifetime.External);
            child1.RegisterInstance(typeof(IService), null, Unresolvable.Create(), InstanceLifetime.External);
            child2.RegisterInstance(typeof(IService), null, Unresolvable.Create(), InstanceLifetime.External);


            // Act/Verify

            Assert.AreNotSame(service, Container.Resolve<IService>());
            Assert.AreNotSame(service, child1.Resolve<IService>());
            Assert.AreNotSame(service, child2.Resolve<IService>());
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void ExternalThrows()
        {
            // Arrange
            var child1 = Container.CreateChildContainer();

            child1.RegisterInstance(typeof(IService), null, Unresolvable.Create(), InstanceLifetime.External);

            // Act/Verify
            var result = Container.Resolve<IService>();
        }
    }
}
