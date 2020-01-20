
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification
{
    public abstract class TestFixtureBase
    {
        #region Constants

        public const string Name = "name";
        public const string Legacy = "legacy";

        #endregion


        #region Container

        protected IUnityContainer Container;

        public virtual IUnityContainer GetContainer() => new UnityContainer();

        #endregion


        #region Setup

        [TestInitialize]
        public virtual void Setup()
        {
            Container = GetContainer();
        }

        #endregion
    }
}
