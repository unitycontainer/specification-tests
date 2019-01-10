using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Diagnostic.Method.Validation
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

    public class OpenGenericInjectionMethod
    {
        public void InjectMe<T>()
        {
        }
    }

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

    public class GenericService<T1, T2, T3>
    {
        public object Value { get; private set; }

        public int Called { get; private set; }

        public void Method(T1 value)
        {
            Value = value;
            Called = 1;
        }

        public void Method(T2 value)
        {
            Value = value;
            Called = 2;
        }

        public void Method(T3 value)
        {
            Value = value;
            Called = 3;
        }
    }


    #endregion
}
