using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unity.Specification.Diagnostic.Method.Injection
{
    public abstract partial class SpecificationTests : Specification.Method.Injection.SpecificationTests
    {
        [ExpectedException(typeof(ResolutionFailedException))]
        public override void InjectingStaticMethod() => base.InjectingStaticMethod();

        [ExpectedException(typeof(ResolutionFailedException))]
        public override void MethodPassingVoid() => base.MethodPassingVoid();

        [ExpectedException(typeof(ResolutionFailedException))]
        public override void ReturningInt() => base.ReturningInt();

        [ExpectedException(typeof(ResolutionFailedException))]
        public override void ReturningVoid() => base.ReturningVoid();

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public override void StaticIsIgnoredInOptimized() => base.StaticIsIgnoredInOptimized();

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public override void InjectTypeWithAnnotatdStatic() => base.InjectTypeWithAnnotatdStatic();
    }
}
