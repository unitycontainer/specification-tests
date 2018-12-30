using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Factory.Resolution
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void Factory_Hierarchical()
        {
            Container.RegisterType<IService>(Lifetime.Hierarchical, Invoke.Factory((c, t, n) => new Service()));

            var service = Container.Resolve<IService>();

            Assert.IsNotNull(service);
            Assert.AreSame(service, Container.Resolve<IService>());

            using (var child = Container.CreateChildContainer())
            {
                Assert.AreNotSame(service, child.Resolve<IService>());
            }
        }

        [TestMethod]
        public void Factory_Singleton()
        {
            Container.RegisterSingleton<IService>(Invoke.Factory((c, t, n) => new Service()));

            var service = Container.Resolve<IService>();

            Assert.IsNotNull(service);
            Assert.AreSame(service, Container.Resolve<IService>());
        }

        [TestMethod]
        public void Factory_Transient()
        {
            Container.RegisterType<IService>(Invoke.Factory((c, t, n) => new Service()));

            var service = Container.Resolve<IService>();

            Assert.IsNotNull(service);
            Assert.AreNotSame(service, Container.Resolve<IService>());
        }
    }
}
