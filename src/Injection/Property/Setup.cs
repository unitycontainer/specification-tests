using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Injection.Property
{
    public abstract partial class SpecificationTests : TestFixtureBase
    {
        [TestInitialize]
        public override void Setup()
        {
            Container = GetContainer();

            Container.RegisterInstance(Name);
        }


        public class ObjectWithThreeProperties
        {
            [Dependency]
            public string Name { get; set; }

            public object Property { get; set; }

            [Dependency]
            public IUnityContainer Container { get; set; }
        }
    }
}
