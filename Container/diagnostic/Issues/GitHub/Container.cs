using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Injection;
using Unity.Resolution;

namespace Unity.Specification.Diagnostic.Issues.GitHub
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        // https://github.com/unitycontainer/container/issues/180
        public void Issue_180()
        {
            Container.RegisterType<Class1>(TypeLifetime.PerContainer, new InjectionMethod(nameof(Class1.MyCompletelyUnambiguousInitializeMethod), true));
            Container.RegisterType<Class2>("1", TypeLifetime.PerContainer, new InjectionMethod(nameof(Class2.AmbiguousInitializeMethod1), true));
            Container.RegisterType<Class2>("2", TypeLifetime.PerContainer, new InjectionMethod(nameof(Class2.AmbiguousInitializeMethod2), true));

            var res1 = Container.Resolve<Class1>();
            var res2 = Container.Resolve<Class1>("1");
            var res3 = Container.Resolve<Class1>("2");
        }


        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        // https://github.com/unitycontainer/container/issues/149
        public void Issue_149()
        {
            Container.RegisterInstance("123");
            var instance = Container.Resolve<TestClass>(new DependencyOverride<string>(new OptionalParameter<string>()));
            Assert.AreEqual("123", instance.Field);
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        // https://github.com/unitycontainer/container/issues/149
        public void Issue_149_Negative()
        {
            // BUG: StackOverflow happens here since 5.9.0
            var instance = Container.Resolve<TestClass>(new DependencyOverride<string>(new OptionalParameter<string>()));
            Assert.IsNull(instance.Field);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        // https://github.com/unitycontainer/container/issues/119
        public void Issue_119()
        {
            // Setup
            Container.RegisterType<IInterface, Class1>();
            Container.RegisterType<IInterface, Class1>(nameof(Class1));
            Container.RegisterType<IInterface, Class2>(nameof(Class2));
            Container.RegisterType<IEnumerable<IInterface>, IInterface[]>();
            Container.RegisterType<ATestClass>();

            // Act
            var instance = Container.Resolve<ATestClass>();

            Assert.IsNotNull(instance);
            Assert.AreEqual(2, instance.Value.Count());
        }
    }
}
