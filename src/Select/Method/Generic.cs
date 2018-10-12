using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Select.Method
{
    public abstract partial class SpecificationTests
    {

        [TestMethod]
        public void Method_Called_Generic()
        {
            // Act
            var result = Container.Resolve<GenericInjectedMethodTest<string>>();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.ExecutedVoid);
            Assert.AreEqual(Name, result.ExecutedGeneric);
        }

        [TestMethod]
        public void Method_Called_Mapped_Generic()
        {
            // Act
            var result = Container.Resolve<IGenericInjectedMethodTest<string>>();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(Name, result.ExecutedGeneric);
        }
    }
}
