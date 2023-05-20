using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Specification.TestData;

namespace Unity.Specification.Diagnostic.Constructor.Attribute
{
    public abstract partial class SpecificationTests : Unity.Specification.Constructor.Attribute.SpecificationTests
    {
        [TestInitialize]
        public override void Setup() => base.Setup();


        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public override void MultipleConstructorsAnnotated()
        {
            // Act
            var instance = Container.Resolve<TypeWithAmbiguousAnnotations>();

            // Assert
            Assert.AreEqual(Container, instance.Container);
        }

    }
}
