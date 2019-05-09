using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Diagnostic.Lifetime
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void Transient_Factory_Null()
        {
            // Arrange
            Container.RegisterFactory<IService>(c => null);

            // Act
            var instance = Container.Resolve<IService>();

            // Validate
            Assert.IsNull(instance);
        }
    }
}
