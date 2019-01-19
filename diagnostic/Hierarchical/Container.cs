using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Diagnostic.Hierarchical
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void ChildContainer()
        {
            // Act
            var child = Container.CreateChildContainer();

            // Validate
            Assert.IsNotNull(child);
        }
    }
}
