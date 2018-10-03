using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Injection.Parameter
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
    }
}
