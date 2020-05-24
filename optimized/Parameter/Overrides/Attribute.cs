using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Parameter.Overrides
{
    public abstract partial class SpecificationTests 
    {
        [TestMethod]
        public void DependencyAttribute()
        {
            // Arrange
            Container.RegisterType<Service>(Invoke.Method(nameof(Service.DependencyAttribute)));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.AreEqual(result.Called, 2);
            Assert.IsInstanceOfType(result.Value, typeof(object));
        }

        [TestMethod]
        public void NamedDependencyAttribute()
        {
            // Arrange
            Container.RegisterType<Service>(Invoke.Method(nameof(Service.NamedDependencyAttribute)));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.AreEqual(result.Called, 3);
            Assert.IsInstanceOfType(result.Value, typeof(string));
            Assert.AreEqual(result.Value, Name);
        }

        [TestMethod]
        public void DependencyAttributeWithDefaultInt()
        {
            // Arrange
            Container.RegisterType<Service>(Invoke.Method(nameof(Service.DependencyAttributeWithDefaultInt)));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.AreEqual(result.Called, 10);
            Assert.AreEqual(result.Value, Service.DefaultInt);
        }

        [TestMethod]
        public void NamedDependencyAttributeWithDefaultInt()
        {
            // Arrange
            Container.RegisterType<Service>(Invoke.Method(nameof(Service.NamedDependencyAttributeWithDefaultInt)));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.AreEqual(result.Called, 11);
            Assert.AreEqual(result.Value, Service.DefaultInt);
        }

        [TestMethod]
        public void DependencyAttributeWithDefaultNullUnresolved()
        {
            // Arrange
            Container.RegisterType<Service>(Invoke.Method(nameof(Service.DependencyAttributeWithDefaultNullUnresolved)));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.AreEqual(result.Called, 16);
            Assert.IsNull(result.Value);
        }

    }
}
