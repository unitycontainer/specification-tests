using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unity.Specification.Diagnostic.Parameter.Resolved
{
    public abstract partial class SpecificationTests : Specification.Parameter.Resolved.SpecificationTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public override void ProvidingConcreteTypeForGenericFails() => base.ProvidingConcreteTypeForGenericFails();
    }
}
