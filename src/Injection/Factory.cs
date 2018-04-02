using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Lifetime;
using Unity.Registration;
using Unity.Specification.TestData;

namespace Unity.Specification.Injection
{
    public abstract partial class SpecificationTests
    {

        [TestMethod]
        public void Specification_Injection_Factory_Hierarchycal()
        {
            _container.RegisterType<IService>(new HierarchicalLifetimeManager(),
                                              new InjectionFactory((c, t, n) => new Service()));

            var service = _container.Resolve<IService>();

            Assert.IsNotNull(service);
            Assert.AreSame(service, _container.Resolve<IService>());

            using (var child = _container.CreateChildContainer())
            {
                Assert.AreNotSame(service, child.Resolve<IService>());
            }
        }

        [TestMethod]
        public void Specification_Injection_Factory_Singleton()
        {
            _container.RegisterSingleton<IService>(new InjectionFactory((c, t, n) => new Service()));

            var service = _container.Resolve<IService>();

            Assert.IsNotNull(service);
            Assert.AreSame(service, _container.Resolve<IService>());
        }

        [TestMethod]
        public void Specification_Injection_Factory_Transient()
        {
            _container.RegisterType<IService>(new InjectionFactory((c, t, n) => new Service()));

            var service = _container.Resolve<IService>();

            Assert.IsNotNull(service);
            Assert.AreNotSame(service, _container.Resolve<IService>());
        }

        [TestMethod]
        public void Specification_Injection_Factory_IsNotNull()
        {
            _container.RegisterType<IService>(new InjectionFactory((c, t, n) => new Service()));

            Assert.IsNotNull(_container.Resolve<IService>());
        }
    }
}
