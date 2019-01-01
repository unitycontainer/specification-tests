using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Method.Parameters
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

    public class TypeWithMethodWithInvalidParameter
    {
        public void MethodWithRefParameter(ref string ignored)
        {
        }

        public void MethodWithOutParameter(out string ignored)
        {
            ignored = null;
        }
    }

    public class TypeWithMethodWithRefParameter
    {
        [InjectionMethod]
        public void MethodWithRefParameter(ref string ignored)
        {
        }

        public int Property { get; set; }
    }

    public class TypeWithMethodWithOutParameter
    {
        [InjectionMethod]
        public void MethodWithOutParameter(out string ignored)
        {
            ignored = null;
        }

        public int Property { get; set; }
    }

    public interface I1 { }
    public interface I2 { }

    public class C1 : I1 { public C1(I2 i2) { } }

    public class C2 : I2 { public C2(I1 i1) { } }

    #endregion
}
