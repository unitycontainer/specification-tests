using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Specification;

namespace Lifetimes
{
    public partial class Tests
    {
        [TestMethod]
        public void IsTrue()
        {
            Assert.IsNotNull(Container);
        }
    }
}
