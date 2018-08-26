using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Specification.TestData;

namespace Unity.Specification.Injection
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void Parameter_None()
        {
            var service = Container.Resolve(typeof(object), null, null);

            Assert.IsNotNull(service);
            Assert.IsInstanceOfType(service, typeof(object));
        }

        [TestMethod]
        public void Parameter_ObjectWithOneDependency()
        {
            var service = Container.Resolve<ObjectWithOneDependency>();

            Assert.IsNotNull(service);
            Assert.IsInstanceOfType(service, typeof(ObjectWithOneDependency));
            Assert.IsNotNull(service.InnerObject);
        }
    }
}
