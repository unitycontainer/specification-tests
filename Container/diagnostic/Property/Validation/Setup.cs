using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Diagnostic.Property.Validation
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

    public class DependencyInjectedTypeBase
    {
        private object PrivateProperty { get; set; }

        protected object ProtectedProperty { get; set; }

        public object ReadonlyProperty { get; }

        public static object StaticProperty { get; set; }

        public object this[int i]
        {
            get { return new object(); }
            set { }
        }
    }

    public class DependencyInjectedType : DependencyInjectedTypeBase
    {
        public object NormalProperty { get; set; }
    }

    public class DependencyAttributeStaticType
    {
        [Dependency]
        public static object Dependency { get; set; }
    }

    public class OptionalDependencyAttributeStaticType
    {
        [OptionalDependency]
        public static object Dependency { get; set; }
    }
    
    public class DependencyAttributeIndexType
    {
        [Dependency]
        public object this[int i]
        {
            get { return new object(); }
            set { }
        }
    }

    public class OptionalDependencyAttributeIndexType
    {
        [OptionalDependency]
        public object this[int i]
        {
            get { return new object(); }
            set { }
        }
    }

    public class DependencyAttributeReadOnlyType
    {
        [Dependency]
        public object Dependency { get; }
    }

    public class OptionalDependencyAttributeReadOnlyType
    {
        [OptionalDependency]
        public object Dependency { get; }
    }

#pragma warning disable 649

    public class DependencyAttributePrivateType
    {
        [Dependency]
        private object Dependency { get; set; }
    }

    public class OptionalDependencyAttributePrivateType
    {
        [OptionalDependency]
        private object Dependency { get; set; }
    }

#pragma warning restore 649

    public class DependencyAttributeProtectedType
    {
        [Dependency]
        protected object Dependency { get; set; }
    }

    public class OptionalDependencyAttributeProtectedType
    {
        [OptionalDependency]
        protected object Dependency { get; set; }
    }

    public class ObjectWithThreeProperties
    {
        [Dependency]
        public string Name { get; set; }

        public object Property { get; set; }

        [Dependency]
        public IUnityContainer Container { get; set; }
    }

    public class ObjectWithFourProperties : ObjectWithThreeProperties
    {
        public object SubProperty { get; set; }

        public object ReadOnlyProperty { get; }
    }

    #endregion
}
