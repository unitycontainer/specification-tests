using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Diagnostic.Constructor.Attribute
{
    public abstract partial class SpecificationTests : Specification.Constructor.Attribute.SpecificationTests
    {
        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public override void MultipleConstructorsAnnotated() => base.MultipleConstructorsAnnotated();
    }
}
