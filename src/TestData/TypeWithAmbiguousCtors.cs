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

        public TypeWithAmbiguousCtors(int first, string second, float third)
        {
            Signature = Two;
        }

        public TypeWithAmbiguousCtors(Type first, Type second, Type third)
        {
            Signature = Three;
        }

        public TypeWithAmbiguousCtors(string first, string second, string third)
        {
            Signature = first;
        }

        public TypeWithAmbiguousCtors(string first, [Dependency(Five)]string second, IUnityContainer third)
        {
            Signature = second;
        }
    }
}
