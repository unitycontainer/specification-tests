using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unity.Specification.Diagnostic.Constructor.Injection
{
    public abstract partial class SpecificationTests : Specification.Constructor.Injection.SpecificationTests
    {
        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public override void NoBogusConstructor() => base.NoBogusConstructor();

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public override void NoBogusValuesConstructor() => base.NoBogusValuesConstructor();

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public override void NoDefaultConstructor() => base.NoDefaultConstructor();

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public override void NoConstructor() => base.NoConstructor();

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public override void MultipleConstructor() => base.MultipleConstructor();

        [DataTestMethod]
        [DynamicData(nameof(ConstructorSelectionTestData), typeof(Specification.Constructor.Injection.SpecificationTests))]
        public override void Selection(string name, Type typeFrom, Type typeTo, Type typeToResolve, object[] parameters, Func<object, bool> validator) => 
            base.Selection(name, typeFrom, typeTo, typeToResolve, parameters, validator);

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public override void AmbiguousCtorInGraph() => base.AmbiguousCtorInGraph();
    }
}
