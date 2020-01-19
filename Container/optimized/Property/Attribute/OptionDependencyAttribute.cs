using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Property.Attribute
{
    public abstract partial class SpecificationTests 
    {
        [TestMethod]
        public void OptionalDependencyAttribute()
        {
            // Act
            var result = Container.Resolve<OptionalDependencyAttributeType>();

            // Assert
            Assert.AreEqual(result.Called, 4);
            Assert.IsInstanceOfType(result.Value, typeof(object));
        }

        [TestMethod]
        public void OptionalNamedDependencyAttribute()
        {
            // Act
            var result = Container.Resolve<OptionalNamedDependencyAttributeType>();

            // Assert
            Assert.AreEqual(result.Called, 5);
            Assert.IsInstanceOfType(result.Value, typeof(string));
            Assert.AreEqual(result.Value, Name);
        }

        [TestMethod]
        public void OptionalDependencyAttributeMissing()
        {
            // Act
            var result = Container.Resolve<OptionalDependencyAttributeMissingType>();

            // Assert
            Assert.AreEqual(result.Called, 6);
            Assert.IsNull(result.Value);
        }

        [TestMethod]
        public void OptionalNamedDependencyAttributeMissing()
        {
            // Act
            var result = Container.Resolve<OptionalNamedDependencyAttributeMissingType>();

            // Assert
            Assert.AreEqual(result.Called, 7);
            Assert.IsNull(result.Value);
        }
    }
}
