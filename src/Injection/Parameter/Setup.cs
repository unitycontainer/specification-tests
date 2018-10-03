using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Injection.Parameter
{
    public abstract partial class SpecificationTests : TestFixtureBase
    {
        [TestInitialize]
        public override void Setup()
        {
            Container = GetContainer();
        }
    }
}
