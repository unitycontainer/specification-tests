namespace Unity.Specification.TestData
{
    public class TypeWithAmbiguousAnnotations
    {
        public TypeWithAmbiguousAnnotations() => Ctor = 1;

        [InjectionConstructor]
        public TypeWithAmbiguousAnnotations(object arg) => Ctor = 2;

        public TypeWithAmbiguousAnnotations(IUnityContainer container) => Ctor = 3;

        [InjectionConstructor]
        public TypeWithAmbiguousAnnotations(object[] data) => Ctor = 4;

        public int Ctor { get; }    // Constructor called 

        [Dependency]
        public IUnityContainer Container { get; set; }
    }
}
