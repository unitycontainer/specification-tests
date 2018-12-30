using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Parameter.Attribute
{
    public abstract partial class SpecificationTests : TestFixtureBase
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
    }
}
