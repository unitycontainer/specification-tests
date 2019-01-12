using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Parameter.Attribute
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void NoAttribute()
        {
            // Arrange
            Container.RegisterType<Service>(Invoke.Method(nameof(Service.NoAttributeParameter)));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.AreEqual(result.Called, 1);
            Assert.IsInstanceOfType(result.Value, typeof(object));
        }

        [TestMethod]
        public void NoAttributeWithDefault()
        {
            // Arrange
            Container.RegisterType<Service>(Invoke.Method(nameof(Service.NoAttributeWithDefault)));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.AreEqual(result.Called, 8);
            Assert.AreNotEqual(result.Value, Service.DefaultString);
        }

        [TestMethod]
        public void NoAttributeWithDefaultInt()
        {
            // Arrange
            Container.RegisterType<Service>(Invoke.Method(nameof(Service.NoAttributeWithDefaultInt)));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.AreEqual(result.Called, 9);
            Assert.AreEqual(result.Value, Service.DefaultInt);
        }

        [TestMethod]
        public void NoAttributeWithDefaultUnresolved()
        {
            // Arrange
            Container.RegisterType<Service>(Invoke.Method(nameof(Service.NoAttributeWithDefaultUnresolved)));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.AreEqual(result.Called, 14);
            Assert.AreEqual((long)result.Value, 100);
        }

        [TestMethod]
        public void NoAttributeWithDisposableUnresolved()
        {
            // Arrange
            Container.RegisterType<Service>(Invoke.Method(nameof(Service.WithDefaultDisposableUnresolved)));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.AreEqual(result.Called, 15);
            Assert.IsNull(result.Value);
        }
    }
}
