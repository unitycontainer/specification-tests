using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Injection;
using Unity.Specification.TestData;

namespace Unity.Specification.Injection
{
    public abstract partial class SpecificationTests 
    {
        [TestMethod]
        public void Specification_Injection_Constructor_DefaultConstructor()
        {
            _container.RegisterType<ObjectWithAmbiguousConstructors>(new InjectionConstructor());
            Assert.AreEqual(ObjectWithAmbiguousConstructors.One, _container.Resolve<ObjectWithAmbiguousConstructors>().Signature);
        }

        [TestMethod]
        public void Specification_Injection_Constructor_IncorrectType()
        {
            Assert.ThrowsException<InvalidOperationException>(() =>
                _container.RegisterType<ObjectWithAmbiguousConstructors>(
                    new InjectionConstructor(typeof(int))));
        }

        [TestMethod]
        public void Specification_Injection_Constructor_IncorrectValue()
        {
            Assert.ThrowsException<InvalidOperationException>(() =>
                _container.RegisterType<ObjectWithAmbiguousConstructors>(
                    new InjectionConstructor(0)));
        }

        [TestMethod]
        public void Specification_Injection_Constructor_SelectByValues()
        {
            _container.RegisterType<ObjectWithAmbiguousConstructors>(new InjectionConstructor(0, string.Empty, 0.0f));
            Assert.AreEqual(ObjectWithAmbiguousConstructors.Two, _container.Resolve<ObjectWithAmbiguousConstructors>().Signature);
        }

        [TestMethod]
        public void Specification_Injection_Constructor_SelectByValueTypes()
        {
            _container.RegisterType<ObjectWithAmbiguousConstructors>(new InjectionConstructor(new InjectionParameter(typeof(string)), 
                                                                                              new InjectionParameter(typeof(string)), 
                                                                                              new InjectionParameter(typeof(int))));
            Assert.AreEqual(ObjectWithAmbiguousConstructors.Three, _container.Resolve<ObjectWithAmbiguousConstructors>().Signature);
        }


        [TestMethod]
        public void Specification_Injection_Constructor_SelectAndResolveByValue()
        {
            _container.RegisterInstance(ObjectWithAmbiguousConstructors.Four);
            _container.RegisterType<ObjectWithAmbiguousConstructors>(new InjectionConstructor(new ResolvedParameter(typeof(string)), 
                                                                                              string.Empty, 
                                                                                              string.Empty));
            Assert.AreEqual(ObjectWithAmbiguousConstructors.Four, _container.Resolve<ObjectWithAmbiguousConstructors>().Signature);
        }


        [TestMethod]
        public void Specification_Injection_Constructor_ResolveNamedTypeArgument()
        {
            _container.RegisterInstance(ObjectWithAmbiguousConstructors.Four);
            _container.RegisterInstance(ObjectWithAmbiguousConstructors.Five, ObjectWithAmbiguousConstructors.Five);

            _container.RegisterType<ObjectWithAmbiguousConstructors>(new InjectionConstructor(typeof(string), 
                                                                                              typeof(string), 
                                                                                              typeof(IUnityContainer)));
            Assert.AreEqual(ObjectWithAmbiguousConstructors.Five, _container.Resolve<ObjectWithAmbiguousConstructors>().Signature);
        }

        [TestMethod]
        public void Specification_Injection_Constructor_Generic_DefaultConstructor()
        {
            _container.RegisterType(null, typeof(InjectionTestCollection<>), null, null, new InjectionConstructor());

            var instance = _container.Resolve<InjectionTestCollection<object>>();
            Assert.IsNotNull(instance);
            Assert.AreEqual(typeof(InjectionTestCollection<>).Name, instance.CollectionName);
        }

        [TestMethod]
        [Ignore]
        public void Specification_Injection_Constructor_Generic_ByType()
        {
            _container.RegisterType(typeof(InjectionTestCollection<>), new InjectionConstructor(typeof(string), typeof(IGenericService<>)));
            var instance = _container.Resolve<InjectionTestCollection<object>>();
            Assert.IsNotNull(instance);
            Assert.AreEqual(typeof(InjectionTestCollection<>).Name, instance.CollectionName);
        }


    }
}
