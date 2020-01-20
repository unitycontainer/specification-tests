using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification
{
    public abstract class ConfigFixtureBase : TestFixtureBase
    {
        protected abstract string Path { get; }

        [TestInitialize]
        public override void Setup()
        {
            base.Setup();


        }
    }
}
