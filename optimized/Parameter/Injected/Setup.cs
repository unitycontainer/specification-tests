using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Parameter.Injected
{
    public abstract partial class SpecificationTests : TestFixtureBase
    {
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();

            Container.RegisterInstance("other");
            Container.RegisterInstance(Name, Name);
        }


        #region Test Data

        public class Service
        {
            #region Properties

            public object Value { get; private set; }

            public object ValueOne { get; private set; }

            public object ValueTwo { get; private set; }

            #endregion

            [InjectionMethod]
            public void Method(object value)
            {
                Value = value;
            }

            public void MethodOne(object value)
            {
                ValueOne = value;
            }

            public void MethodTwo(object one, object two)
            {
                ValueOne = one;
                ValueTwo = two;
            }
        }

        #endregion
    }
}
