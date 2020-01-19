using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Field.Overrides.Validation
{
    public abstract partial class SpecificationTests : Unity.Specification.Field.Overrides.SpecificationTests
    {
        [TestInitialize]
        public override void Setup() => base.Setup();

    }
}
