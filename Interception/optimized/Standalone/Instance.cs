using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Interception.Specifications
{
    public partial class Standalone
    {
        [TestMethod]
        public void CanInterceptTargetWithInstanceInterceptor()
        {
            Assert.IsNotNull(Container);
        }
    }
}
