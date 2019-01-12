using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Constructor.Overrides
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void Constructor()
        {
            using (IUnityContainer container = GetContainer())
            {
                // Arrange

                // Act

                // Assert
            }
        }

        [TestMethod]
        public void SelectConstructorAttribute()
        {
            // Arrange
            Container.RegisterType(typeof(object));

            // Act
            var result = Container.Resolve<object>();

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
