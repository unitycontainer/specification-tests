using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Container
{
    public abstract partial class SpecificationTests : TestFixtureBase
    {
        private IUnityContainer _container;

        [TestInitialize]
        public void Setup()
        {
            _container = GetContainer();
        }

    }
}
