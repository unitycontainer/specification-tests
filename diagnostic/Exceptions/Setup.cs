using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Diagnostic.Exceptions
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

    public interface IService { }

    public class Service : IService { }

    public interface IProvider { }

    public class Provider : IProvider
    {
        public Provider(IService service)
        {

        }
    }

    public class DependOnProvider
    {
        public DependOnProvider(IProvider provider)
        {

        }
    }

    public class DependsOnDependOnProvider
    {
        public DependsOnDependOnProvider(DependOnProvider depend)
        {

        }
    }

    public class ClassWithOutMethod
    {
        [InjectionMethod]
        public void InjectedMethod(out string outParam)
        {
            outParam = null;
        }
    }

    public class ClassWithRefMethod
    {
        [InjectionMethod]
        public void InjectedMethod(ref string outParam)
        {
        }
    }

    public class ClassWithMethod
    {
        [InjectionMethod]
        public void InjectedMethod(string unresolved)
        { }
    }

    public class ClassDependingOnMethod
    {
        public ClassDependingOnMethod(ClassWithMethod unresolved)
        {

        }
    }

    public class ClassWithStringField
    {
        [Dependency]
        public string Field;
    }

    public class ClassWithNamedStringField
    {
        [Dependency("name")]
        public string Field { get; set; }
    }

    public class ClassWithStringFieldDependency
    {
        [Dependency]
        public ClassWithStringField Dependency;
    }

    public class ClassWithNamedStringProperty
    {
        [Dependency("name")]
        public string Property { get; set; }
    }

    public class ClassWithStringProperty
    {
        [Dependency]
        public string Property { get; set; }
    }

    public class ClassWithStringPropertyDependency
    {
        [Dependency]
        public ClassWithStringProperty Dependency { get; set; }
    }

    public class ClassWithOtherDependency
    {
        public ClassWithOtherDependency(IUnityContainer container, ClassWithStringDependency dependency)
        {

        }
    }

    public class ClassWithStringDependency
    {
        public ClassWithStringDependency(string unresolved)
        {

        }
    }

    #endregion
}
