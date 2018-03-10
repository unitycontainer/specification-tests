using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Resolution
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void Specification_Resolution_Mapping_Simple()
        {
            object o = _container.Resolve<object>();

            Assert.IsNotNull(o);
        }

    }
}
