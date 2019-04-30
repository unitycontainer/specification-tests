using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Registration.Extended
{
    public abstract partial class SpecificationTests : TestFixtureBase
    {
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
        }
    }

    public interface IService { }
    public class Service : IService { }

    public class TypeWithAmbiguousCtors
    {
        public const string One = "1";
        public const string Two = "2";
        public const string Three = "3";
        public const string Four = "4";
        public const string Five = "5";

        public string Signature { get; }

        public TypeWithAmbiguousCtors()
        {
            Signature = One;
        }

        public TypeWithAmbiguousCtors(object first)
        {
            Signature = Two;
        }

        public TypeWithAmbiguousCtors(Type first, Type second, Type third)
        {
            Signature = Three;
        }

        public TypeWithAmbiguousCtors(string first, string second, string third)
        {
            Signature = Four;
        }

        public TypeWithAmbiguousCtors(string first, [Dependency(Five)]string second, IUnityContainer third)
        {
            Signature = Five;
        }

        [Dependency]
        public IUnityContainer Container { get; set; }
    }
}
