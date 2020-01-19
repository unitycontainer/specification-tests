using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Method.Injection.Validation
{
    public abstract partial class SpecificationTests : Unity.Specification.Method.Injection.SpecificationTests
    {
        [TestInitialize]
        public override void Setup() => base.Setup();


        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public override void MultipleMethods() => base.MultipleMethods();

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public override void ReturningInt() => base.ReturningInt();

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public override void MethodPassingVoid() => base.MethodPassingVoid();

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public override void ReturningVoid() => base.ReturningVoid();

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public override void StaticMethod() => base.StaticMethod();
    }
}
