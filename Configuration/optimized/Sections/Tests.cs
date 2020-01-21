using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Specification;

namespace Sections
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
