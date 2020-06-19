using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unity.Specification.Diagnostic.Method.Validation
{
    public abstract partial class SpecificationTests
    {
        [Ignore]
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AnonymousTypeForGenericFails()
        {
            // Act
            Container.RegisterType(typeof(GenericService<,,>),
                Invoke.Method("Method", Resolve.Parameter()));
        }
    }
}
