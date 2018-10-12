using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Injection;

namespace Unity.Specification.Injection.Factory
{
    public abstract partial class SpecificationTests
    {

        [TestMethod]
        public void Factory_Hierarchycal()
        {
            Container.RegisterType<IService>(new HierarchicalLifetimeManager(),
                                              new InjectionFactory((c, t, n) => new Service()));

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
            Container.RegisterSingleton<IService>(new InjectionFactory((c, t, n) => new Service()));

            var service = Container.Resolve<IService>();

            Assert.IsNotNull(service);
            Assert.AreSame(service, Container.Resolve<IService>());
        }

        [TestMethod]
        public void Factory_Transient()
        {
            Container.RegisterType<IService>(new InjectionFactory((c, t, n) => new Service()));

            var service = Container.Resolve<IService>();

            Assert.IsNotNull(service);
            Assert.AreNotSame(service, Container.Resolve<IService>());
        }

        [TestMethod]
        public void Factory_IsNotNull()
        {
            Container.RegisterType<IService>(new InjectionFactory((c, t, n) => new Service()));

            Assert.IsNotNull(Container.Resolve<IService>());
        }
    }
}
