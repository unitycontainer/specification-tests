using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Diagnostic.Property.Validation
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void AttributeOnStatic()
        {
            // Act
            var result = Container.Resolve<DependencyAttributeStaticType>();
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void OptionalAttributeOnStatic()
        {
            // Act
            var result = Container.Resolve<OptionalDependencyAttributeStaticType>();
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void AttributeOnIndex()
        {
            // Act
            var result = Container.Resolve<DependencyAttributeIndexType>();
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void OptionalAttributeOnIndex()
        {
            // Act
            var result = Container.Resolve<OptionalDependencyAttributeIndexType>();
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void AttributeOnReadOnly()
        {
            // Act
            var result = Container.Resolve<DependencyAttributeReadOnlyType>();
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void OptionalAttributeOnReadOnly()
        {
            // Act
            var result = Container.Resolve<OptionalDependencyAttributeReadOnlyType>();
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void AttributeOnPrivate()
        {
            // Act
            var result = Container.Resolve<DependencyAttributePrivateType>();
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void AttributeOnProtected()
        {
            // Act
            var result = Container.Resolve<DependencyAttributeProtectedType>();
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void OptionalAttributeOnPrivate()
        {
            // Act
            var result = Container.Resolve<OptionalDependencyAttributePrivateType>();
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void OptionalAttributeOnProtected()
        {
            // Act
            var result = Container.Resolve<OptionalDependencyAttributeProtectedType>();
        }
    }
}
