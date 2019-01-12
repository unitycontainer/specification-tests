using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Diagnostic.Constructor.Types
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void BuildInterfacePropertyInjectTest1()
        {
            // Setup
            BarClass objBase = new BarClass();

            // Act
            Container.BuildUp(typeof(IFooInterface), objBase);

            // Verify
            Assert.IsNotNull(objBase.InterfaceProp);
        }

        [TestMethod]
        public void BuildInterfacePropertyInjectTest2()
        {
            // Setup
            BarClass2 objBase = new BarClass2();

            // Act
            Container.BuildUp(typeof(IFooInterface2), objBase);

            // Verify
            Assert.IsNull(objBase.InterfaceProp);
        }
    }
}
