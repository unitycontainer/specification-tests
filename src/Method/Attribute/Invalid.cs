using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unity.Specification.Method.Attribute
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void MethodWithRefParameter()
        {
            // Act
            Container.RegisterType<Parameters.TypeWithMethodWithInvalidParameter>(
                Invoke.Method(nameof(Parameters.TypeWithMethodWithInvalidParameter.MethodWithRefParameter)));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void MethodWithOutParameter()
        {
            // Act
            Container.RegisterType<Parameters.TypeWithMethodWithInvalidParameter>(
                Invoke.Method(nameof(Parameters.TypeWithMethodWithInvalidParameter.MethodWithOutParameter)));
        }
    }
}
