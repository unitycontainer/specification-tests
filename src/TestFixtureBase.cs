using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification
{
    public abstract class TestFixtureBase
    {
        public abstract IUnityContainer GetContainer();
    }
}
