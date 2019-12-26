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
            public const string DefaultString = "default";
            public const int DefaultInt = 111;

            #region Properties

            public int Called { get; private set; } = -1;

            public object Value { get; private set; }

            #endregion


            public void NoParameters() => Called = 0;

            public void NoAttributeParameter(object value)
            {
                Value = value;
                Called = 1;
            }

            public void DependencyAttribute([Dependency]object value)
            {
                Value = value;
                Called = 2;
            }

            public void NamedDependencyAttribute([Dependency(Name)]string value)
            {
                Value = value;
                Called = 3;
            }

            public void OptionalDependencyAttribute([OptionalDependency]object value)
            {
                Value = value;
                Called = 4;
            }

            public void OptionalNamedDependencyAttribute([OptionalDependency(Name)]string value)
            {
                Value = value;
                Called = 5;
            }
        }

        public class OtherService
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
