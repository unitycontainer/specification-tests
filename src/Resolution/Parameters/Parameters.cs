using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unity.Specification.Resolution.Parameters
{
    public abstract partial class SpecificationTests : TestFixtureBase
    {
        [TestMethod]
        public void NoParameters()
        {
            // Arrange
            Container.RegisterType<Service>(Invoke.Method(nameof(Service.NoParameters)));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.AreEqual(result.Called, 0);
        }

        [TestMethod]
        public void NoAttributeParameter()
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
        public void NoAttributeParameterWithDefault()
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
