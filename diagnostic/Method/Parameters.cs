using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Specification.Method.Parameters;

namespace Unity.Specification.Diagnostic.Method.Parameters
{
    public abstract partial class SpecificationTests : Unity.Specification.Method.Parameters.SpecificationTests
    {
        [TestInitialize]
        public override void Setup() => base.Setup();

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void ChainedExecuteMethodBaseline()
        {
            // Setup
            Container
                .RegisterType(typeof(ICommand<>), typeof(ConcreteCommand<>),
                    Invoke.Method("ChainedExecute"));

            // Act
            var result = Container.Resolve<ICommand<Account>>();
            
            // Verify
            Assert.Fail();
        }
    }
}
