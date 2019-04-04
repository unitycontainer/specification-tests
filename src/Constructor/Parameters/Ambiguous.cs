using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Specification.TestData;

namespace Unity.Specification.Constructor.Parameters
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void AmbuguousConstructor()
        {
            // Act
            var instance = Container.Resolve<TypeWithAmbiguousCtors>();

            // Assert
            Assert.AreEqual(Container, instance.Container);
        }
    }
}
