using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unity.Specification.Diagnostic.Method.Validation
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GenericInjectionMethod()
        {
            // Act
            Container.RegisterType<OpenGenericInjectionMethod>(
                Invoke.Method(nameof(OpenGenericInjectionMethod.InjectMe)));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void MethodWithRefParameter()
        {
            // Act
            Container.RegisterType<TypeWithMethodWithInvalidParameter>(
                Invoke.Method(nameof(TypeWithMethodWithInvalidParameter.MethodWithRefParameter)));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void MethodWithOutParameter()
        {
            // Act
            Container.RegisterType<TypeWithMethodWithInvalidParameter>(
                Invoke.Method(nameof(TypeWithMethodWithInvalidParameter.MethodWithOutParameter)));
        }

    }
}
