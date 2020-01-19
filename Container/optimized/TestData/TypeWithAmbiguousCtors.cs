using System;

namespace Unity.Specification.TestData
{
    public class TypeWithAmbiguousCtors
    {
        public const string One =   "1";
        public const string Two =   "2";
        public const string Three = "3";
        public const string Four =  "4";
        public const string Five =  "5";

        public string Signature { get; }

        public TypeWithAmbiguousCtors()
        {
            Signature = One;
        }

        public TypeWithAmbiguousCtors(object first, object second)
        {
            Signature = Two;
        }

        public TypeWithAmbiguousCtors(IUnityContainer first, object second)
        {
            Signature = Three;
        }

        public TypeWithAmbiguousCtors(object first, IUnityContainer second)
        {
            Signature = Four;
        }

        [Dependency]
        public IUnityContainer Container { get; set; }
    }
}
