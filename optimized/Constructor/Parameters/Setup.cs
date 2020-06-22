using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Constructor.Parameters
{
    public abstract partial class SpecificationTests : TestFixtureBase
    {
        public const string DefaultString = "default";
        public const int DefaultInt = 111;

        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
        }
    }

    #region Test Data

    public class NoParametersCtor : BaseType
    {
        public NoParametersCtor() { }

    }

    public class NoAttributeParameterCtor : BaseType
    {
        public NoAttributeParameterCtor(string value)
        {
            Value = value;
        }
    }

    public class NoAttributeWithDefaultCtor : BaseType
    {
        public NoAttributeWithDefaultCtor(string value = SpecificationTests.DefaultString)
        {
            Value = value;
        }
    }

    public class NoAttributeWithDefaultValueCtor : BaseType
    {
        public NoAttributeWithDefaultValueCtor(int value = SpecificationTests.DefaultInt)
        {
            Value = value;
        }
    }

    public class NoAttributeWithDefaultNullCtor : BaseType
    {
        public NoAttributeWithDefaultNullCtor(string value = null)
        {
            Value = value;
        }
    }

    public class DependencyParameterCtor : BaseType
    {
        public DependencyParameterCtor([Dependency]string value)
        {
            Value = value;
        }
    }

    public class DependencyNamedParameterCtor : BaseType
    {
        public DependencyNamedParameterCtor([Dependency(TestFixtureBase.Name)]string value)
        {
            Value = value;
        }

    }

    public class DependencyWithDefaultCtor : BaseType
    {
        public DependencyWithDefaultCtor([Dependency]string value = SpecificationTests.DefaultString)
        {
            Value = value;
        }
    }

    public class DependencyNamedWithDefaultCtor : BaseType
    {
        public DependencyNamedWithDefaultCtor([Dependency(TestFixtureBase.Name)]string value = SpecificationTests.DefaultString)
        {
            Value = value;
        }
    }

    public class DependencyWithDefaultValueCtor : BaseType
    {
        public DependencyWithDefaultValueCtor([Dependency]int value = SpecificationTests.DefaultInt)
        {
            Value = value;
        }
    }

    public class DependencyWithDefaultNullCtor : BaseType
    {
        public DependencyWithDefaultNullCtor([Dependency]string value = null)
        {
            Value = value;
        }
    }

    public class OptionalParameterCtor : BaseType
    {
        public OptionalParameterCtor([OptionalDependency]string value)
        {
            Value = value;
        }
    }

    public class OptionalWithDefaultValueCtor : BaseType
    {
        public OptionalWithDefaultValueCtor([OptionalDependency]string value = SpecificationTests.DefaultString)
        {
            Value = value;
        }
    }

    public class OptionalNamedParameterCtor : BaseType
    {
        public OptionalNamedParameterCtor([OptionalDependency(TestFixtureBase.Name)]string value)
        {
            Value = value;
        }
    }

    public class OptionalNamedWithDefaultCtor : BaseType
    {
        public OptionalNamedWithDefaultCtor([OptionalDependency(TestFixtureBase.Name)]string value = SpecificationTests.DefaultString)
        {
            Value = value;
        }
    }

    public class OptionalWithDefaultNullCtor : BaseType
    {
        public OptionalWithDefaultNullCtor([OptionalDependency]string value = null)
        {
            Value = value;
        }
    }

    public class BaseType
    {
        public object Value { get; protected set; } = "none";
    }

    public struct TestStruct
    {
    }

    public class TypeWithStructParameter
    {
        public TypeWithStructParameter(TestStruct data)
        {
            Data = data;
        }

        public TestStruct Data { get; set; }
    }

    public class TypeWithDynamicParameter
    {
        public TypeWithDynamicParameter(dynamic data)
        {
            Data = data;
        }

        public dynamic Data { get; set; }
    }

    public class NamedTypeWithDynamicParameter
    {
        public NamedTypeWithDynamicParameter([Dependency(TestFixtureBase.Name)]dynamic data)
        {
            Data = data;
        }

        public dynamic Data { get; set; }
    }

    public class TypeWithRefParameter
    {
        public TypeWithRefParameter(ref string ignored)
        {
        }

        public int Property { get; set; }
    }

    public class TypeWithOutParameter
    {
        public TypeWithOutParameter(out string ignored)
        {
            ignored = null;
        }

        public int Property { get; set; }
    }

    public class TypeWithUnresolvableParameter
    {
        public TypeWithUnresolvableParameter(Unresolvable data)
        {
            Data = data;
        }
        public dynamic Data { get; set; }
    }

    public class Unresolvable
    {
        private Unresolvable() { }

        public static Unresolvable Create() => new Unresolvable();
    }

    #endregion
}
