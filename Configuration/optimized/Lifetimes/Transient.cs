using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Specification;

namespace Lifetimes
{
    [TestClass]
    public partial class Tests : ConfigFixtureBase
    {
        [TestMethod]
        public void IsTrue()
        {
            Assert.IsNotNull(Container);
        }
    }
}
