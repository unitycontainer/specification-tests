using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unity.Specification.Diagnostic.Method.Injection
{
    public abstract partial class SpecificationTests : Specification.Method.Injection.SpecificationTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public override void InjectingStaticMethod() => base.InjectingStaticMethod();

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public override void MethodWithOutParameter() => base.MethodWithOutParameter();

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public override void MethodWithRefParameter() => base.MethodWithRefParameter();
    }
}
