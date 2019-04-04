using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Specification.TestData;

namespace Unity.Specification.Diagnostic.Constructor.Parameters
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void AmbuguousConstructor()
        {
            // Act
            var instance = Container.Resolve<TypeWithAmbiguousCtors>();
        }

    }
}
