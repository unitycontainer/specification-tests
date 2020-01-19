using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Method.Overrides
{
    public abstract partial class SpecificationTests : TestFixtureBase
    {
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
        }
    }
}
