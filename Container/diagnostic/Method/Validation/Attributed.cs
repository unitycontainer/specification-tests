using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Diagnostic.Method.Validation
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void AttributeOnStatic()
        {
            // Act
            var result = Container.Resolve<AttributeStaticType>();
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void AttributeOnPrivate()
        {
            // Act
            var result = Container.Resolve<AttributePrivateType>();
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void AttributeOnProtected()
        {
            // Act
            var result = Container.Resolve<AttributeProtectedType>();
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void AttributeOnOpenGeneric()
        {
            // Act
            var result = Container.Resolve<AttributeOpenGenericType>();
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void AttributeOnOutParam()
        {
            // Act
            var result = Container.Resolve<AttributeOutParamType>();
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void AttributeOnRefParam()
        {
            // Act
            var result = Container.Resolve<AttributeRefParamType>();
        }
    }
}
