using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Specification.TestData;

namespace Unity.Specification.BuildUp
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
            Assert.AreEqual(Container,                  instance.Container);
        }

        [TestMethod]
        public void AmbiguousAnnotations()
        {
            // Arrange
            var instance = new TypeWithAmbiguousAnnotations();

            // Act
            Container.BuildUp(instance);

            // Assert
            Assert.AreEqual(1,         instance.Ctor);
            Assert.AreEqual(Container, instance.Container);
        }
    }
}
