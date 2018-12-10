using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Injection.Constructor
{
    public abstract partial class SpecificationTests 
    {
        [TestMethod]
        public void DefaultConstructor()
        {
            Container.RegisterType<TypeWithAmbiguousCtors>(Execute.Constructor());
            Assert.AreEqual(TypeWithAmbiguousCtors.One, Container.Resolve<TypeWithAmbiguousCtors>().Signature);
        }


        [TestMethod]
        public void DefaultConstructorGeneric()
        {
            Container.RegisterType(null, typeof(InjectionTestCollection<>), null, null, Execute.Constructor());

            var instance = Container.Resolve<InjectionTestCollection<object>>();
            Assert.IsNotNull(instance);
            Assert.AreEqual(typeof(InjectionTestCollection<>).Name, instance.CollectionName);
        }


        [TestMethod]
        public void SelectAndResolveByValue()
        {
            Container.RegisterInstance(TypeWithAmbiguousCtors.Four);
            Container.RegisterType<TypeWithAmbiguousCtors>(Execute.Constructor(Resolve.Parameter(typeof(string)), 
                                                                               string.Empty, 
                                                                               string.Empty));
            Assert.AreEqual(TypeWithAmbiguousCtors.Four, Container.Resolve<TypeWithAmbiguousCtors>().Signature);
        }


        [TestMethod]
        public void ResolveNamedTypeArgument()
        {
            Container.RegisterInstance(TypeWithAmbiguousCtors.Four);
            Container.RegisterInstance(TypeWithAmbiguousCtors.Five, TypeWithAmbiguousCtors.Five);

            Container.RegisterType<TypeWithAmbiguousCtors>(Execute.Constructor(typeof(string), 
                                                                               typeof(string), 
                                                                               typeof(IUnityContainer)));
            Assert.AreEqual(TypeWithAmbiguousCtors.Five, Container.Resolve<TypeWithAmbiguousCtors>().Signature);
        }
    }
}
