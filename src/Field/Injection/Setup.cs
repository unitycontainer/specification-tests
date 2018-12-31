using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Field.Injection
{
    public abstract partial class SpecificationTests : TestFixtureBase
    {
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();

            Container.RegisterInstance(Name);
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

    public class ObjectWithDependency
    {
        public ObjectWithDependency(ObjectWithThreeFields obj)
        {
            Dependency = obj;
        }

        public ObjectWithThreeFields Dependency { get; }

    }

    #endregion

}
