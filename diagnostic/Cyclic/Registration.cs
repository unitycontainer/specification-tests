using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Diagnostic.Cyclic
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        // https://github.com/unitycontainer/container/issues/122
        public void GitHub_Container_122()
        {
            Container.RegisterType<I1, C1>();
            Container.RegisterType<I2, C2>();

            //next line returns StackOverflowException
            Container.Resolve<I2>();
        }
    }
}
