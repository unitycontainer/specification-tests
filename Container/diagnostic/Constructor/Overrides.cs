using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Diagnostic.Constructor.Overrides
{
    public abstract partial class SpecificationTests : Unity.Specification.Constructor.Overrides.SpecificationTests
    {
        [TestInitialize]
        public override void Setup() => base.Setup();

    }
}
