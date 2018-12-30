using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Field.Attribute
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void NoAttribute()
        {
            // Act
            var result = Container.Resolve<NoAttributeType>();

            // Assert
            Assert.AreEqual(result.Called, 1);
            Assert.IsNull(result.Value);
        }

        [TestMethod]
        public void DependencyAttribute()
        {
            // Act
            var result = Container.Resolve<DependencyAttributeType>();

            // Assert
            Assert.AreEqual(result.Called, 2);
            Assert.IsInstanceOfType(result.Value, typeof(object));
        }

        [TestMethod]
        public void NamedDependencyAttribute()
        {
            // Act
            var result = Container.Resolve<NamedDependencyAttributeType>();

            // Assert
            Assert.AreEqual(result.Called, 3);
            Assert.IsInstanceOfType(result.Value, typeof(string));
            Assert.AreEqual(result.Value, Name);
        }


        [TestMethod]
        public void DependencyAttributePrivate()
        {
            // Act
            var result = Container.Resolve<DependencyAttributePrivateType>();

            // Assert
            Assert.AreEqual(result.Called, 8);
            Assert.IsNull(result.Value);
        }

        [TestMethod]
        public void DependencyAttributeProtected()
        {
            // Act
            var result = Container.Resolve<DependencyAttributeProtectedType>();

            // Assert
            Assert.AreEqual(result.Called, 9);
            Assert.IsNull(result.Value);
        }
    }
}
