using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Diagnostic.Constructor.Validation
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void NoInjectableConstructor()
        {
            // Setup
            Container.RegisterType<ObjectWithAmbiguousConstructors>();

            // Act
            var result = Container.Resolve<ObjectWithAmbiguousConstructors>();

            // Validate
            Assert.Fail();
        }

    }
}
