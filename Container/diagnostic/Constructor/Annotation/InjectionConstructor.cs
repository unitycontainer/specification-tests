using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Specification.TestData;

namespace Unity.Specification.Diagnostic.Constructor.Annotation
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void Constructor()
        {
            #region attribute_ctor

            // Act
            var instance = Container.Resolve<Service>();

            // 2 == instance.Ctor

            #endregion
            // Assert
            Assert.AreEqual(2, instance.Ctor);
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void MultipleConstructorsAnnotated()
        {
            #region attribute_ctor

            // Act
            var instance = Container.Resolve<TypeWithAmbuguousAnnotations>();

            // 2 == instance.Ctor

            #endregion
            // Assert
            Assert.AreEqual(Container, instance.Container);
        }
    }
}
