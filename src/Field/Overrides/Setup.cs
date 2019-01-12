using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Field.Overrides
{
    public abstract partial class SpecificationTests : TestFixtureBase
    {
        private const string Name1 = "name1";
        private const string Name2 = "name2";

        [TestInitialize]
        public override void Setup()
        {
            base.Setup();

            Container.RegisterInstance(Name);
            Container.RegisterInstance(Name, Name);
            Container.RegisterInstance(Name1, Name1);
            Container.RegisterInstance(Name2, Name2);
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

    public class ObjectWithAttributes
    {
        [Dependency("name1")]
        public string Dependency;

        [OptionalDependency("other")]
        public string Optional;
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
