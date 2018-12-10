using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Injection.Constructor
{
    public abstract partial class SpecificationTests
    {

        [TestMethod]
        public void SelectByValues()
        {
            Container.RegisterType<TypeWithAmbiguousCtors>(Execute.Constructor(0, string.Empty, 0.0f));
            Assert.AreEqual(TypeWithAmbiguousCtors.Two, Container.Resolve<TypeWithAmbiguousCtors>().Signature);
        }

    }
}
