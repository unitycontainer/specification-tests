using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Specification.TestData;

namespace Unity.Specification.Constructor.Attribute
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
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
        public void MultipleConstructorsAnnotated()
        {
            // Act
            var instance = Container.Resolve<TypeWithAmbuguousAnnotations>();

            // Assert
            Assert.AreEqual(Container, instance.Container);
        }

    }
}
