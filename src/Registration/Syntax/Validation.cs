using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Registration.Syntax
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void InvalidRegistration()
        {
            // Act
            Container.RegisterType<IService>();
        }
    }
}
