using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Diagnostic.Constructor.Annotation
{
    public abstract partial class SpecificationTests : TestFixtureBase
    {
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
        }
    }

    #region class_service
    public class Service
    {
        public Service() => Ctor = 1;

        [InjectionConstructor]
        public Service(object arg) => Ctor = 2;

        public Service(IUnityContainer container) => Ctor = 3;

        [InjectionConstructor]
        public Service(object[] data) => Ctor = 4;

        public int Ctor { get; }    // Constructor called 
    }
    #endregion

    #region class_service_generic
    public class Service<T>
    {
        public Service() => Ctor = 1;

        [InjectionConstructor]
        public Service(T arg) => Ctor = 2;

        public Service(IUnityContainer container, T arg) => Ctor = 3;

        [InjectionConstructor]
        public Service(T arg, string data) => Ctor = 4;

        public int Ctor { get; }    // Constructor called 
    }
    #endregion
}
