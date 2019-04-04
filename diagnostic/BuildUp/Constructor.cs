using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Specification.TestData;

namespace Unity.Specification.Diagnostic.BuildUp
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void AmbuguousConstructor()
        {
            // Arrange
            var instance = new TypeWithAmbiguousCtors();

            // Act
            Container.BuildUp(instance);

            // Assert
            Assert.AreEqual(TypeWithAmbiguousCtors.One, instance.Signature);
            Assert.AreEqual(Container, instance.Container);
        }

        [TestMethod]
        public void AmbuguousAnnotations()
        {
            // Arrange
            var instance = new TypeWithAmbuguousAnnotations();

            // Act
            Container.BuildUp(instance);

            // Assert
            Assert.AreEqual(1, instance.Ctor);
            Assert.AreEqual(Container, instance.Container);
        }
    }
}
