using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Diagnostic.Field.Validation
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

#pragma warning disable 169
#pragma warning disable 649
#pragma warning disable IDE0051 // Remove unused private members
#pragma warning disable IDE0044 // Add readonly modifier

    public class DependencyInjectedTypeBase
    {
        private object PrivateField;

        protected object ProtectedField;

        public readonly object ReadonlyField;

        public static object StaticField;
    }

    public class DependencyInjectedType : DependencyInjectedTypeBase
    {
        public object NormalField;
    }

    public class DependencyAttributeStaticType
    {
        [Dependency]
        public static object Dependency;
    }

    public class OptionalDependencyAttributeStaticType
    {
        [OptionalDependency]
        public static object Dependency;
    }

    public class DependencyAttributeReadOnlyType
    {
        [Dependency]
        public readonly object Dependency;
    }

    public class OptionalDependencyAttributeReadOnlyType
    {
        [OptionalDependency]
        public readonly object Dependency;
    }


    public class DependencyAttributePrivateType
    {
        [Dependency]
        private object Dependency;
    }

    public class OptionalDependencyAttributePrivateType
    {
        [OptionalDependency]
        private object Dependency;
    }


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

#pragma warning restore 169
#pragma warning restore 649
#pragma warning restore IDE0044 // Add readonly modifier
#pragma warning restore IDE0051 // Remove unused private members

    public class ObjectWithThreeFields
    {
        [Dependency]
        public string Name;

        public object Field;

        [Dependency]
        public IUnityContainer Container;
    }

    public class ObjectWithFourFields : ObjectWithThreeFields
    {
        public object SubField;

        public readonly object ReadOnlyField;
    }

    #endregion
}
