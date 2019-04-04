using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.BuildUp
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void BuildUpInterfaceWithDependency()
        {
            // Setup
            BarClass objBase = new BarClass();

            // Act
            Container.BuildUp(typeof(IFooInterface), objBase);

            // Verify
            Assert.IsNotNull(objBase.InterfaceProp);
        }

        [TestMethod]
        public void BuildUpInterfaceWithoutDependency()
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
