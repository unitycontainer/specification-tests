using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Method.Attribute
{
    public abstract partial class SpecificationTests : TestFixtureBase
    {
        [TestMethod]
        public void MethodWithNoParameters()
        {
            // Act
            var result = Container.Resolve<TypeNoParameters>();

            // Verify
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void MethodWithParameters()
        {
            // Act
            var result = Container.Resolve<TypeWithParameter>();

            // Verify
            Assert.IsNotNull(result);
            Assert.AreEqual(Name, result.Data);
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void MethodWithRefParameters()
        {
            // Act
            var result = Container.Resolve<TypeWithRefParameter>();

            // Verify
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void MethodWithOutParameters()
        {
            // Act
            var result = Container.Resolve<TypeWithOutParameter>();

            // Verify
            Assert.Fail();
        }
    }
}
