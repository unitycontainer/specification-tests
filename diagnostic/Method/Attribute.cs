using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Method.Attribute.Validation
{
    public abstract partial class SpecificationTests : Unity.Specification.Method.Attribute.SpecificationTests
    {
        [TestInitialize]
        public override void Setup() => base.Setup();
    }
}
