using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unity.Specification.Diagnostic.Constructor.Injection
{
    public abstract partial class SpecificationTests : Specification.Constructor.Injection.SpecificationTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public override void NoBogusConstructor() => base.NoBogusConstructor();

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public override void NoBogusValuesConstructor() => base.NoBogusValuesConstructor();

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public override void NoDefaultConstructor() => base.NoDefaultConstructor();

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public override void NoConstructor() => base.NoConstructor();

        // TODO: enable
        //[TestMethod]
        //[ExpectedException(typeof(InvalidOperationException))]
        //public void NoReuse()
        //{
        //    // Arrange
        //    var ctor = Invoke.Constructor();

        //    // Act
        //    Container.RegisterType<TypeWithAmbiguousCtors>("1", ctor)
        //             .RegisterType<TypeWithAmbiguousCtors>("2", ctor);
        //}

        //[TestMethod]
        //[ExpectedException(typeof(InvalidOperationException))]
        //public void NoConstructor()
        //{
        //    // Act
        //    Container.RegisterType<TypeWithAmbiguousCtors>(
        //        Invoke.Constructor(Resolve.Parameter()));
        //}

        //[TestMethod]
        //[ExpectedException(typeof(ResolutionFailedException))]
        //public void MultipleConstructor()
        //{
        //    // Arrange
        //    Container.RegisterType<TypeWithAmbiguousCtors>(
        //        Invoke.Constructor(),
        //        Invoke.Constructor());

        //    // Act
        //    Container.Resolve<TypeWithAmbiguousCtors>();
        //}

        #region Test Data

        public class ObjectWithAmbiguousConstructors
        {
            public const string One = "1";
            public const string Two = "2";
            public const string Three = "3";
            public const string Four = "4";
            public const string Five = "5";

            public string Signature { get; }

            public ObjectWithAmbiguousConstructors(int first, string second, float third)
            {
                Signature = Two;
            }

            public ObjectWithAmbiguousConstructors(Type first, Type second, Type third)
            {
                Signature = Three;
            }

            public ObjectWithAmbiguousConstructors(string first, string second, string third)
            {
                Signature = first;
            }

            public ObjectWithAmbiguousConstructors(string first, [Dependency(Five)] string second, IUnityContainer third)
            {
                Signature = second;
            }
        }

        #endregion
    }
}
