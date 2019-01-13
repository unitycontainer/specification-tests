using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Diagnostic.Exceptions
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

    public class SecondLevel
    {
        public SecondLevel(IUnityContainer container, ClassWithStringDependency dependency)
        {

        }
    }

    public class ClassWithStringDependency
    {
        public ClassWithStringDependency(string unresolved)
        {

        }
    }

    #endregion
}
