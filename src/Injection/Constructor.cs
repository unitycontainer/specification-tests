using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Registration;
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
        public void Specification_Injection_Constructor_ByValueTypes()
        {
            _container.RegisterType<ObjectWithAmbiguousConstructors>(new InjectionConstructor(new InjectionParameter(typeof(string)), 
                                                                                              new InjectionParameter(typeof(string)), 
                                                                                              new InjectionParameter(typeof(int))));
            Assert.AreEqual(ObjectWithAmbiguousConstructors.Three, _container.Resolve<ObjectWithAmbiguousConstructors>().Signature);
        }

        [TestMethod]
        public void Specification_Injection_Constructor_ByValue()
        {
            _container.RegisterType<ObjectWithAmbiguousConstructors>(new InjectionConstructor(0, string.Empty, 0.0f));
            Assert.AreEqual(ObjectWithAmbiguousConstructors.Two, _container.Resolve<ObjectWithAmbiguousConstructors>().Signature);
        }

        [TestMethod]
        public void Specification_Injection_Constructor_ByValue_WithResolve()
        {
            _container.RegisterInstance(ObjectWithAmbiguousConstructors.Four);
            _container.RegisterType<ObjectWithAmbiguousConstructors>(new InjectionConstructor(new ResolvedParameter(typeof(string)), 
                                                                                              string.Empty, 
                                                                                              string.Empty));
            Assert.AreEqual(ObjectWithAmbiguousConstructors.Four, _container.Resolve<ObjectWithAmbiguousConstructors>().Signature);
        }


        [TestMethod]
        public void Specification_Injection_Constructor_ByType()
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
            _container.RegisterType(typeof(InjectionTestCollection<>), new InjectionConstructor());

            var instance = _container.Resolve<InjectionTestCollection<object>>();
            Assert.IsNotNull(instance);
            Assert.AreEqual(typeof(InjectionTestCollection<>).Name, instance.CollectionName);
        }

        [TestMethod]
        public void Specification_Injection_Constructor_Generic_ByType()
        {
            _container.RegisterInstance(string.Empty) 
                      .RegisterType(typeof(InjectionTestCollection<>), new InjectionConstructor(typeof(string), typeof(IGenericService<>)));

            var instance = _container.Resolve<InjectionTestCollection<object>>();
            Assert.IsNotNull(instance);
            Assert.AreEqual(typeof(InjectionTestCollection<>).Name, instance.CollectionName);
        }

        [TestMethod]
        public void Specification_Injection_Constructor_Generic_ByType_Generics()
        {
            _container.RegisterInstance(string.Empty)
                      .RegisterInstance("0", "0")
                      .RegisterInstance("1", "1")
                      .RegisterInstance("2", "2")
                      .RegisterInstance<IService>(new Service())
                      .RegisterInstance<IService>("0", new Service())
                      .RegisterInstance<IService>("1", new Service())
                      .RegisterInstance<IService>("2", new Service())
                      .RegisterType(typeof(GenericInjectionTestClass<,,>),
                                    new InjectionConstructor(typeof(string),
                                                             typeof(IGenericService<>),
                                                             typeof(Array),
                                                             typeof(IEnumerable<>)));

            var instance = _container.Resolve<GenericInjectionTestClass<IUnityContainer, string, IService>>();

            Assert.IsNotNull(instance);
            Assert.AreEqual(typeof(GenericInjectionTestClass<,,>).Name, instance.CollectionName);
        }

        [TestMethod]
        public void Specification_Injection_Constructor_Generic_ByType_MultiGenerics()
        {
            _container.RegisterInstance(string.Empty)
                      .RegisterInstance("0", "0")
                      .RegisterInstance("1", "1")
                      .RegisterInstance("2", "2")
                      .RegisterInstance<IService>(new Service())
                      .RegisterInstance<IService>("0", new Service())
                      .RegisterInstance<IService>("1", new Service())
                      .RegisterInstance<IService>("2", new Service())
                      .RegisterType(typeof(GenericInjectionTestClass<,,>),
                                    new InjectionConstructor(typeof(GenericDependencyClass<,>)));

            var instance = _container.Resolve<GenericInjectionTestClass<IUnityContainer, string, IService>>();

            Assert.IsNotNull(instance);
            Assert.AreEqual(typeof(GenericInjectionTestClass<,,>).Name, instance.CollectionName);
        }

        // TODO: Add test cases for Array<> dependencies
    }
}
