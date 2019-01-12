using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Constructor.Parameters
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void RefParameter()
        {
            Container.Resolve<TypeWithConstructorWithRefParameter>();
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void OutParameter()
        {
            Container.Resolve<TypeWithConstructorWithOutParameter>();
        }
    }
}
