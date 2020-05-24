using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unity.Specification.Parameter.Overrides
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

            public void OptionalDependencyAttributeMissing([OptionalDependency]IDisposable value)
            {
                Value = value;
                Called = 6;
            }

            public void OptionalNamedDependencyAttributeMissing([OptionalDependency(Name)]IDisposable value)
            {
                Value = value;
                Called = 7;
            }

            public void NoAttributeWithDefault(string value = DefaultString)
            {
                Value = value;
                Called = 8;
            }

            public void NoAttributeWithDefaultInt(int value = DefaultInt)
            {
                Value = value;
                Called = 9;
            }

            public void DependencyAttributeWithDefaultInt([Dependency]int value = DefaultInt)
            {
                Value = value;
                Called = 10;
            }

            public void NamedDependencyAttributeWithDefaultInt([Dependency(Name)]int value = DefaultInt)
            {
                Value = value;
                Called = 11;
            }

            public void OptionalDependencyAttributeWithDefaultInt([OptionalDependency]int value = DefaultInt)
            {
                Value = value;
                Called = 12;
            }

            public void OptionalNamedDependencyAttributeWithDefaultInt([OptionalDependency(Name)]int value = DefaultInt)
            {
                Value = value;
                Called = 13;
            }

            public void NoAttributeWithDefaultUnresolved(long value = 100)
            {
                Value = value;
                Called = 14;
            }

            public void WithDefaultDisposableUnresolved(IDisposable value = null)
            {
                Value = value;
                Called = 15;
            }

            public void DependencyAttributeWithDefaultNullUnresolved([Dependency]IDisposable value = null)
            {
                Value = value;
                Called = 16;
            }
        }

        #endregion
    }
}
