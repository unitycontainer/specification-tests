using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Unity.Interception.Specification
{
    [TestClass]
    public abstract class TestFixtureBase
    {
        public const string Name = "name";

        protected IUnityContainer Container;


        [TestInitialize]
        public virtual void Setup()
        {
            Container = new UnityContainer(); 
        }
    }
}
