using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Specification.TestData;

namespace Unity.Specification.Injection.Constructor
{
    public abstract partial class SpecificationTests 
    {
        [TestMethod]
        public void Constructor_DefaultConstructor()
        {
            Container.RegisterType<ObjectWithAmbiguousConstructors>(new InjectionConstructor());
            Assert.AreEqual(ObjectWithAmbiguousConstructors.One, Container.Resolve<ObjectWithAmbiguousConstructors>().Signature);
        }

        [TestMethod]
        public void Constructor_IncorrectType()
        {
            Assert.ThrowsException<InvalidOperationException>(() =>
                Container.RegisterType<ObjectWithAmbiguousConstructors>(
                    new InjectionConstructor(typeof(int))));
        }

        [TestMethod]
        public void Constructor_IncorrectValue()
        {
            Assert.ThrowsException<InvalidOperationException>(() =>
                Container.RegisterType<ObjectWithAmbiguousConstructors>(
                    new InjectionConstructor(0)));
        }

        [TestMethod]
        public void Constructor_SelectByValues()
        {
            Container.RegisterType<ObjectWithAmbiguousConstructors>(new InjectionConstructor(0, string.Empty, 0.0f));
            Assert.AreEqual(ObjectWithAmbiguousConstructors.Two, Container.Resolve<ObjectWithAmbiguousConstructors>().Signature);
        }

        [TestMethod]
        public void Constructor_SelectByValueTypes()
        {
            Container.RegisterType<ObjectWithAmbiguousConstructors>(new InjectionConstructor(new InjectionParameter(typeof(string)), 
                                                                                              new InjectionParameter(typeof(string)), 
                                                                                              new InjectionParameter(typeof(int))));
            Assert.AreEqual(ObjectWithAmbiguousConstructors.Three, Container.Resolve<ObjectWithAmbiguousConstructors>().Signature);
        }


        [TestMethod]
        public void Constructor_SelectAndResolveByValue()
        {
            Container.RegisterInstance(ObjectWithAmbiguousConstructors.Four);
            Container.RegisterType<ObjectWithAmbiguousConstructors>(new InjectionConstructor(new ResolvedParameter(typeof(string)), 
                                                                                              string.Empty, 
                                                                                              string.Empty));
            Assert.AreEqual(ObjectWithAmbiguousConstructors.Four, Container.Resolve<ObjectWithAmbiguousConstructors>().Signature);
        }


        [TestMethod]
        public void Constructor_ResolveNamedTypeArgument()
        {
            Container.RegisterInstance(ObjectWithAmbiguousConstructors.Four);
            Container.RegisterInstance(ObjectWithAmbiguousConstructors.Five, ObjectWithAmbiguousConstructors.Five);

            Container.RegisterType<ObjectWithAmbiguousConstructors>(new InjectionConstructor(typeof(string), 
                                                                                              typeof(string), 
                                                                                              typeof(IUnityContainer)));
            Assert.AreEqual(ObjectWithAmbiguousConstructors.Five, Container.Resolve<ObjectWithAmbiguousConstructors>().Signature);
        }

        [TestMethod]
        public void Constructor_Generic_DefaultConstructor()
        {
            Container.RegisterType(null, typeof(InjectionTestCollection<>), null, null, new InjectionConstructor());

            var instance = Container.Resolve<InjectionTestCollection<object>>();
            Assert.IsNotNull(instance);
            Assert.AreEqual(typeof(InjectionTestCollection<>).Name, instance.CollectionName);
        }
    }
}
