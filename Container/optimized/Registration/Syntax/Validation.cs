using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Lifetime;

namespace Unity.Specification.Registration.Syntax
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void InvalidRegistration()
        {
            // Act
            Container.RegisterType<IService>();
        }

        [TestMethod]
        public void Singleton()
        {
            Container.RegisterSingleton<IService, Service>();
            Container.RegisterType<IService, Service>(Name, new ContainerControlledLifetimeManager());

            // Act
            var anonymous = Container.Resolve<IService>();
            var named = Container.Resolve<IService>(Name);

            // Validate
            Assert.IsNotNull(anonymous);
            Assert.IsNotNull(named);
            Assert.AreNotSame(anonymous, named);
        }

    }
}
