using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Property.Injection
{
    public abstract partial class SpecificationTests : TestFixtureBase
    {
        private const string Other = "other";

        [TestInitialize]
        public override void Setup()
        {
            base.Setup();

            Container.RegisterInstance(Name);
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

    public class ObjectWithDependency
    {
        public ObjectWithDependency(ObjectWithThreeProperties obj)
        {
            Dependency = obj;
        }

        public ObjectWithThreeProperties Dependency { get; }

    }

    #endregion
}
