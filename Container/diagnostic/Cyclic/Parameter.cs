using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Diagnostic.Cyclic
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void ParameterToInterface()
        {
            // Arrange
            Container.RegisterType<I1, B1>();

            // Act
            Container.Resolve<I1>();
        }
    }
}
