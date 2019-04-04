using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unity.Specification.Diagnostic.Constructor.Injection
{
    public abstract partial class SpecificationTests : TestFixtureBase
    {
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
        }
    }

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

        public ObjectWithAmbiguousConstructors(string first, [Dependency(Five)]string second, IUnityContainer third)
        {
            Signature = second;
        }
    }

    #endregion
}
