using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Resolution
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void Resolution_clr_object()
        {
            Assert.IsNotNull(_container.Resolve<object>()); 
        }

    }
}
