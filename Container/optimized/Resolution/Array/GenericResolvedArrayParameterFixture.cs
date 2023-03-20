﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Unity.Injection;

namespace Unity.Specification.Resolution.Array
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void ContainerAutomaticallyResolvesAllWhenInjectingGenericArrays()
        {
           // Arrange
           ILogger[] expected = new ILogger[] { new MockLogger(), new SpecialLogger() };
            Container.RegisterInstance("one", expected[0])
                     .RegisterInstance("two", expected[1])
                     .RegisterType(typeof(GenericTypeWithArrayProperty<>),
                        new InjectionProperty("Prop"));

            var result = Container.Resolve<GenericTypeWithArrayProperty<ILogger>>();
            Assert.AreSame(expected[0], result.Prop[0]);
            Assert.AreSame(expected[1], result.Prop[1]);
        }

        [TestMethod]
        public void CanCallConstructorTakingGenericParameterArray()
        {
            Container.RegisterType(typeof(ClassWithOneArrayGenericParameter<>),
                                   new InjectionConstructor(new GenericParameter("T[]")));

            Service a0 = new Service();
            Service a1 = new Service();
            Service a2 = new Service();

            Container.RegisterInstance<Service>("a0", a0)
                     .RegisterInstance<Service>("a1", a1)
                     .RegisterInstance<Service>(a2);

            var result = Container.Resolve<ClassWithOneArrayGenericParameter<Service>>();

            Assert.IsFalse(result.DefaultConstructorCalled);
            Assert.AreEqual(2, result.InjectedValue.Length);
            Assert.AreSame(a0, result.InjectedValue[0]);
            Assert.AreSame(a1, result.InjectedValue[1]);
        }

        [TestMethod]
        public void CanCallConstructorTakingGenericParameterArrayWithValues()
        {
            Container.RegisterType( typeof(ClassWithOneArrayGenericParameter<>),
                new InjectionConstructor(
                    new GenericResolvedArrayParameter(
                        "T",
                        new GenericParameter("T", "a2"),
                        new GenericParameter("T", "a1"))));

            Service a0 = new Service();
            Service a1 = new Service();
            Service a2 = new Service();

            Container.RegisterInstance<Service>("a0", a0)
                     .RegisterInstance<Service>("a1", a1)
                     .RegisterInstance<Service>("a2", a2);

            var result = Container.Resolve<ClassWithOneArrayGenericParameter<Service>>();

            Assert.IsFalse(result.DefaultConstructorCalled);
            Assert.AreEqual(2, result.InjectedValue.Length);
            Assert.AreSame(a2, result.InjectedValue[0]);
            Assert.AreSame(a1, result.InjectedValue[1]);
        }

        [TestMethod]
        public void CanSetPropertyWithGenericParameterArrayType()
        {
            Container.RegisterType(typeof(ClassWithOneArrayGenericParameter<>),
                new InjectionConstructor(),
                new InjectionProperty("InjectedValue", new GenericParameter("T()")));

            Service a0 = new Service();
            Service a1 = new Service();
            Service a2 = new Service();

            Container.RegisterInstance<Service>("a1", a0)
                     .RegisterInstance<Service>("a2", a1)
                     .RegisterInstance<Service>(a2);

            var result = Container.Resolve<ClassWithOneArrayGenericParameter<Service>>();

            Assert.IsTrue(result.DefaultConstructorCalled);
            Assert.AreEqual(2, result.InjectedValue.Length);
            Assert.AreSame(a0, result.InjectedValue[0]);
            Assert.AreSame(a1, result.InjectedValue[1]);
        }

        [Ignore("Injection validation")]
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AppropriateExceptionIsThrownWhenNoMatchingConstructorCanBeFound()
        {
            Container.RegisterType(typeof(GenericTypeWithArrayProperty<>),
                    new InjectionConstructor(new GenericResolvedArrayParameter("T")));
        }

        [TestMethod]
        public void ResolvesMixedOpenClosedGenericsAsArray()
        {
            // Arrange
            var instance = new Foo<IService>(new OtherService());
            Container.RegisterType<IService, Service>();

            Container.RegisterType<IFoo<IService>, Foo<IService>>("1");
            Container.RegisterType(typeof(IFoo<>), typeof(Foo<>), "fa");
            Container.RegisterInstance<IFoo<IService>>("Instance", instance);

            // Act
            var enumerable = Container.Resolve<IFoo<IService>[]>();

            // Assert
            Assert.AreEqual(3, enumerable.Length);
            Assert.IsNotNull(enumerable[0]);
            Assert.IsNotNull(enumerable[1]);
            Assert.IsNotNull(enumerable[2]);
        }

        [TestMethod]
        public void HandlesGenerics()
        {
            // Arrange
            Container.RegisterInstance(typeof(IService), new OtherService());
            Container.RegisterInstance(typeof(IFoo<IService>),    "service", new Foo<IService>(new Service()));
            Container.RegisterType(typeof(IFoo<>), typeof(Foo<>), "foo");

            // Act
            var array = Container.Resolve<IFoo<IService>[]>();

            // Assert
            Assert.IsNotNull(array);

            Assert.AreEqual(2, array.Length);
            Assert.IsNotNull(array.FirstOrDefault(e => e.Value is Service));
            Assert.IsNotNull(array.FirstOrDefault(e => e.Value is OtherService));
        }

        [TestMethod]
        public void HandlesConstraintViolation()
        {
            // Arrange
            Container.RegisterType(typeof(IService), typeof(Service));
            Container.RegisterType(typeof(IConstrained<>), typeof(Constrained<>), "foo");

            // Act
            var array = Container.Resolve<IConstrained<IService>[]>();

            // Assert
            Assert.IsNotNull(array);

            Assert.AreEqual(0, array.Length);
        }
    }
}
