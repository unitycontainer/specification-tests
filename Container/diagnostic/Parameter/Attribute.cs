using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Parameter.Attribute.Validation
{
    public abstract partial class SpecificationTests : Unity.Specification.Parameter.Attribute.SpecificationTests
    {
        [TestInitialize]
        public override void Setup() => base.Setup();
    }
}
