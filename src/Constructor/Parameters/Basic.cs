using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Constructor.Parameters
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
