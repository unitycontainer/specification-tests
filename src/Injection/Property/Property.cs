using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Injection.Property
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void Property()
        {
            // Arrange
            Container.RegisterType(typeof(object));

            // Act
            var result = Container.Resolve<object>();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Property_None()
        {
            var service = Container.Resolve(typeof(object), null, null);

            Assert.IsNotNull(service);
            Assert.IsInstanceOfType(service, typeof(object));
        }
    }
}
