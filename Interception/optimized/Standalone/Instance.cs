using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Interception.Specification;

namespace Standalone
{
    [TestClass]
    public class Instance_Interception : TestFixtureBase
    {
        [TestMethod]
        public void CanInterceptTargetWithInstanceInterceptor()
        {
            Assert.IsNotNull(Container);
        }
    }
}
