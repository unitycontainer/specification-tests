using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Parameter.Attribute
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void OptionalDependencyAttribute()
        {
            // Arrange
            Container.RegisterType<Service>(Invoke.Method(nameof(Service.OptionalDependencyAttribute)));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.AreEqual(result.Called, 4);
            Assert.IsInstanceOfType(result.Value, typeof(object));
        }

        [TestMethod]
        public void OptionalNamedDependencyAttribute()
        {
            // Arrange
            Container.RegisterType<Service>(Invoke.Method(nameof(Service.OptionalNamedDependencyAttribute)));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.AreEqual(result.Called, 5);
            Assert.IsInstanceOfType(result.Value, typeof(string));
            Assert.AreEqual(result.Value, Name);
        }

        [TestMethod]
        public void OptionalDependencyAttributeMissing()
        {
            // Arrange
            Container.RegisterType<Service>(Invoke.Method(nameof(Service.OptionalDependencyAttributeMissing)));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.AreEqual(result.Called, 6);
            Assert.IsNull(result.Value);
        }

        [TestMethod]
        public void OptionalNamedDependencyAttributeMissing()
        {
            // Arrange
            Container.RegisterType<Service>(Invoke.Method(nameof(Service.OptionalNamedDependencyAttributeMissing)));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.AreEqual(result.Called, 7);
            Assert.IsNull(result.Value);
        }

        [TestMethod]
        public void OptionalDependencyAttributeWithDefaultInt()
        {
            // Arrange
            Container.RegisterType<Service>(Invoke.Method(nameof(Service.OptionalDependencyAttributeWithDefaultInt)));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.AreEqual(result.Called, 12);
            Assert.AreEqual(result.Value, Service.DefaultInt);
        }

        [TestMethod]
        public void OptionalNamedDependencyAttributeWithDefaultInt()
        {
            // Arrange
            Container.RegisterType<Service>(Invoke.Method(nameof(Service.OptionalNamedDependencyAttributeWithDefaultInt)));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.AreEqual(result.Called, 13);
            Assert.AreEqual(result.Value, Service.DefaultInt);
        }
    }
}
