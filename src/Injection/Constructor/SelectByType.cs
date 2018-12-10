using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Injection.Constructor
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void SelectByValueTypes()
        {
            Container.RegisterType<TypeWithAmbiguousCtors>(Execute.Constructor(Inject.Parameter(typeof(string)),
                Inject.Parameter(typeof(string)),
                Inject.Parameter(typeof(int))));
            Assert.AreEqual(TypeWithAmbiguousCtors.Three, Container.Resolve<TypeWithAmbiguousCtors>().Signature);
        }
    }
}
