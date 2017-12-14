using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Specification.TestData;

namespace Unity.Specification.Registration
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void IUnitContainer_Registrations()
        {
            _container.RegisterType<IService, Service>();
            _container.RegisterType<IService, Service>("first");
            _container.RegisterType<IService, Service>("second");

            var reg0 = _container.Registrations.ToArray();

            using (IUnityContainer child = _container.CreateChildContainer())
            {
                child.RegisterType<IService, Service>("third");

                var reg1 = child.Registrations.ToArray();
            }

            _container.RegisterType<IService, Service>("fourth");

            var reg2 = _container.Registrations.ToArray();
        }

    }
}
