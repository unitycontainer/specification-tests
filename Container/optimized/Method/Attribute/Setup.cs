using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Method.Attribute
{
    public abstract partial class SpecificationTests : TestFixtureBase
    {
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
            Container.RegisterInstance(Name);
        }
    }

    #region Test Data

    public class TypeNoParameters
    {
        [InjectionMethod]
        public void InjectedMethod()
        {
            Count += 1;
        }

        public int Count { get; set; }
    }

    public class TypeWithParameter
    {
        [InjectionMethod]
        public void InjectedMethod(string data)
        {
            Data = data;
        }

        public string Data { get; set; }
    }

    public class TypeWithRefParameter
    {
        [InjectionMethod]
        public void InjectedMethod(ref string data)
        {
            Data = data;
        }

        public string Data { get; set; }
    }

    public class TypeWithOutParameter
    {
        [InjectionMethod]
        public void InjectedMethod(out string data)
        {
            data = null;
        }
    }

    #endregion
}
