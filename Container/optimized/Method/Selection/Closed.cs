using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Method.Selection
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void Method_Called()
        {
            // Act
            var result = Container.Resolve<InjectedMethodTest>();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.ExecutedVoid);
            Assert.AreEqual((object) Name, result.Executed);
        }

        [TestMethod]
        public void Method_Called_Mapped()
        {
            // Act
            var result = Container.Resolve<IInjectedMethodTest>();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual((object) Name, result.Executed);
        }
    }
}
