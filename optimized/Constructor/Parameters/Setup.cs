using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Constructor.Parameters
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

    #endregion

    #region type_dependency
    public class Unresolvable
    {
        private Unresolvable() { }

        public static Unresolvable Create() => new Unresolvable();
    }
    #endregion
}
