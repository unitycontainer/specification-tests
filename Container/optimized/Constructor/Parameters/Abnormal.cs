using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Constructor.Parameters
{
    public abstract partial class SpecificationTests
    {

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void UnresolvableParameter()
        {
            // Act
            var instance = Container.Resolve<Unresolvable>();

            // Validate
            Assert.IsNotNull(instance);
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void RefParameter()
        {
            Container.Resolve<TypeWithRefParameter>();
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void OutParameter()
        {
            Container.Resolve<TypeWithOutParameter>();
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void StructParameter()
        {
            // Act
            var instance = Container.Resolve<TypeWithStructParameter>();

            // Validate
            Assert.IsNotNull(instance);
        }

        [TestMethod]
        public void DynamicParameter()
        {
            // Act
            var instance = Container.Resolve<TypeWithDynamicParameter>();

            // Validate
            Assert.IsNotNull(instance);
        }

        [TestMethod]
        public void NamedDynamicParameter()
        {
            // Act
            var instance = Container.Resolve<NamedTypeWithDynamicParameter>();

            // Validate
            Assert.IsNotNull(instance);
        }
    }
}
