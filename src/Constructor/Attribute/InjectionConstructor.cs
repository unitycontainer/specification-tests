using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    }
}
