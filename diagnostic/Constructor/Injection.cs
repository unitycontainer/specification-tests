using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Constructor.Injection.Validation
{
    public abstract partial class SpecificationTests : Unity.Specification.Constructor.Injection.SpecificationTests
    {
        [TestInitialize]
        public override void Setup() => base.Setup();
    }
}
