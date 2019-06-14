using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;

namespace Unity.Specification.Diagnostic.Constructor.Parameters
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void DynamicParameter()
        {
            // Act
            var instance = Container.Resolve<TypeWithDynamicParameter>();

            // Validate
            Assert.IsNotNull(instance);
        }
    }
}
