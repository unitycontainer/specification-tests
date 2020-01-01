using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Constructor.Overrides.Validation
{
    public abstract partial class SpecificationTests : Unity.Specification.Constructor.Overrides.SpecificationTests
    {
        [TestInitialize]
        public override void Setup() => base.Setup();

    }
}
