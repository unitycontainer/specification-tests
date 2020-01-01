using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Constructor.Attribute.Validation
{
    public abstract partial class SpecificationTests : Unity.Specification.Constructor.Attribute.SpecificationTests
    {
        [TestInitialize]
        public override void Setup() => base.Setup();
    }
}
