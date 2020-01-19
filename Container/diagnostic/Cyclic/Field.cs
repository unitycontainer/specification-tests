using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Diagnostic.Cyclic
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void FieldToInterface()
        {
            // Arrange
            Container.RegisterType<I1, D1>();

            // Act
            Container.Resolve<D1>();
        }
    }
}
