using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Diagnostic.Cyclic
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

    public interface I0 { }

    public interface I1 : I0 { }
    public interface I2 : I0 { }

    public class B1 : I1 { public B1(I1 i1) { } }

    public class C1 : I1 { public C1(I2 i2) { } }

    public class C2 : I2 { public C2(I1 i1) { } }

    public class D1 : I1
    {
        [Dependency]
        public I1 Field;
    }

    public class E1 : I1
    {
        [Dependency]
        public I1 Property { get; set; }
    }

    public class F1 : I1
    {
        [InjectionMethod]
        public void Method(I1 i1) { }
    }

    public class G0 : I0 { }
    public class G1 : I1 { public G1(I0 i0) { } }

    #endregion
}
