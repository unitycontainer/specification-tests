using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Constructor.Attribute
{
    public abstract partial class SpecificationTests : TestFixtureBase
    {
        private string _data = "data";

        [TestInitialize]
        public override void Setup() => base.Setup();
    }

    #region class_ctor_with_dependency
    public class CtorWithDependency
    {
        public CtorWithDependency([Dependency]string data)
        {
            Data = data;
        }

        public string Data { get; }
    }
    #endregion

    #region class_ctor_with_named_dependency
    public class CtorWithNamedDependency
    {
        public CtorWithNamedDependency()
        {
            Data = null;
        }

        public CtorWithNamedDependency([Dependency("name")]string data)
        {
            Data = data;
        }

        public string Data { get; }
    }
    #endregion

    #region class_ctor_with_optional_dependency
    public class CtorWithOptionalDependency
    {
        public CtorWithOptionalDependency([OptionalDependency]string data = null)
        {
            Data = data;
        }

        public string Data { get; }
    }
    #endregion

    #region class_ctor_with_named_optional_dependency
    public class CtorWithOptionalNamedDependency
    {
        public CtorWithOptionalNamedDependency([OptionalDependency("name")]string data = null)
        {
            Data = data;
        }

        public string Data { get; }
    }
    #endregion

    #region class_service
    public class Service 
    {
        public Service() => Ctor = 1;

        [InjectionConstructor]
        public Service(object arg) => Ctor = 2;

        public Service(IUnityContainer container) => Ctor = 3;

        public Service(object[] data) => Ctor = 4;

        public int Ctor { get; }    // Constructor called 
    }
    #endregion

    #region class_service_generic
    public class Service<T>
    {
        public Service() => Ctor = 1;

        public Service(T arg) => Ctor = 2;

        public Service(IUnityContainer container, T arg) => Ctor = 3;

        public Service(T arg, string data) => Ctor = 4;

        public int Ctor { get; }    // Constructor called 
    }
    #endregion

}
