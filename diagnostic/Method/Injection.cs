using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unity.Specification.Diagnostic.Method.Injection
{
    public abstract partial class SpecificationTests : Specification.Method.Injection.SpecificationTests
    {
        [Ignore]
        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public override void InjectingStaticMethod() => base.InjectingStaticMethod();

        [Ignore]
        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public override void MethodWithOutParameter() => base.MethodWithOutParameter();

        [Ignore]
        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public override void MethodWithRefParameter() => base.MethodWithRefParameter();

        [Ignore]
        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public override void StaticMethod() => base.StaticMethod();
    }
}
