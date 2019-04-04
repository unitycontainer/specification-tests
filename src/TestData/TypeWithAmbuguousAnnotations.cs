namespace Unity.Specification.TestData
{
    public class TypeWithAmbuguousAnnotations
    {
        public TypeWithAmbuguousAnnotations() => Ctor = 1;

        [InjectionConstructor]
        public TypeWithAmbuguousAnnotations(object arg) => Ctor = 2;

        public TypeWithAmbuguousAnnotations(IUnityContainer container) => Ctor = 3;

        [InjectionConstructor]
        public TypeWithAmbuguousAnnotations(object[] data) => Ctor = 4;

        public int Ctor { get; }    // Constructor called 

        [Dependency]
        public IUnityContainer Container { get; set; }
    }
}
