using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Diagnostic.Property.Validation
{
    public abstract partial class SpecificationTests : TestFixtureBase
    {
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
        }
    }

    #region Test Data

    public class ObjectWithThreeProperties
    {
        [Dependency]
        public string Name { get; set; }

        public object Property { get; set; }

        [Dependency]
        public IUnityContainer Container { get; set; }
    }

    public class ObjectWithFourProperties : ObjectWithThreeProperties
    {
        public object SubProperty { get; set; }

        public object ReadOnlyProperty { get; }
    }

    #endregion
}
