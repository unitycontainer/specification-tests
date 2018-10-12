using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Select.Constructor
{
    public abstract partial class SpecificationTests
    {
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
