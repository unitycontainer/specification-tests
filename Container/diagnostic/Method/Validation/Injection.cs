using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unity.Specification.Diagnostic.Method.Validation
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NoReuse()
        {
            // Arrange
            var method = Invoke.Method(nameof(InjectedType.NormalMethod));

            // Act
            Container.RegisterType<InjectedType>("1", method)
                     .RegisterType<InjectedType>("2", method);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void InjectPrivateMethod()
        {
            // Act
            Container.RegisterType<InjectedType>(
                Invoke.Method("PrivateMethod"));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void InjectProtectedMethod()
        {
            // Act
            Container.RegisterType<InjectedType>(
                Invoke.Method("ProtectedMethod"));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void InjectStaticMethod()
        {
            // Act
            Container.RegisterType<InjectedType>(
                Invoke.Method(nameof(InjectedType.StaticMethod)));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void InjectOpenGenericMethod()
        {
            // Act
            Container.RegisterType<InjectedType>(
                Invoke.Method(nameof(InjectedType.OpenGenericMethod)));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void InjectOutParamMethod()
        {
            // Act
            Container.RegisterType<InjectedType>(
                Invoke.Method(nameof(InjectedType.OutParamMethod)));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void InjectRefParamMethod()
        {
            // Act
            Container.RegisterType<InjectedType>(
                Invoke.Method(nameof(InjectedType.RefParamMethod)));
        }
    }
}
