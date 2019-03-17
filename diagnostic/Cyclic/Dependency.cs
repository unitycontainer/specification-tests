using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Diagnostic.Cyclic
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void DependencyOverride()
        {
            // Arrange
            Container.RegisterType<I0, G0>()
                     .RegisterType<I1, G1>();

            //next line throws StackOverflowException
            Container.Resolve<G1>(
                Override.Dependency<I0>(
                    Resolve.Dependency<I1>()));
        }
    }
}
