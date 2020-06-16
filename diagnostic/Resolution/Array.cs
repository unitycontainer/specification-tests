using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unity.Specification.Diagnostic.Resolution.Array
{
    public abstract partial class SpecificationTests : Specification.Resolution.Array.SpecificationTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public override void AppropriateExceptionIsThrownWhenNoMatchingConstructorCanBeFound() => 
            base.AppropriateExceptionIsThrownWhenNoMatchingConstructorCanBeFound();
    }
}
