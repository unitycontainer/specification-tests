using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unity.Specification.Diagnostic.Field.Injection
{
    public abstract partial class SpecificationTests : Specification.Field.Injection.SpecificationTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public override void None() => base.None();
    }
}
