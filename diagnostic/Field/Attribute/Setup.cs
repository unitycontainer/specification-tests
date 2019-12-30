using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Field.Attribute.Validation
{
    public abstract partial class SpecificationTests : Unity.Specification.Field.Attribute.SpecificationTests
    {
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
        }
    }

}
