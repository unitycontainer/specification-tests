using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Diagnostic.Field.Validation
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

    public class ObjectWithThreeFields
    {
        [Dependency]
        public string Name;

        public object Field;

        [Dependency]
        public IUnityContainer Container;
    }

    public class ObjectWithFourFields : ObjectWithThreeFields
    {
        public object SubField;

        public readonly object ReadOnlyField;
    }


    #endregion
}
