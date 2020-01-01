using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Parameter.Overrides.Validation
{
    public abstract partial class SpecificationTests : Unity.Specification.Parameter.Overrides.SpecificationTests
    {
        [TestInitialize]
        public override void Setup() => base.Setup();

    }
}
