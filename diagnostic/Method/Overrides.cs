using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Method.Overrides.Validation
{
    public abstract partial class SpecificationTests : Unity.Specification.Method.Overrides.SpecificationTests
    {
        [TestInitialize]
        public override void Setup() => base.Setup();

    }
}
