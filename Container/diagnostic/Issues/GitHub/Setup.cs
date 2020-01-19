using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Unity.Specification.Diagnostic.Issues.GitHub
{
    public abstract partial class SpecificationTests : Unity.Specification.Issues.GitHub.SpecificationTests
    {
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
        }
    }

    #region Test Data

    public interface IInterface
    {
    }

    public class Class1 : IInterface
    {
        public void MyCompletelyUnambiguousInitializeMethod(bool arg)
        {
            Console.WriteLine($"Initialized: {arg}");
        }
    }

    public class Class2 : IInterface
    {
        public void AmbiguousInitializeMethod1(bool arg)
        {
            Console.WriteLine($"Initialized 1: {arg}");
        }
        public void AmbiguousInitializeMethod2(bool arg)
        {
            Console.WriteLine($"Initialized 2: {arg}");
        }
    }

    public class ATestClass
    {
        public ATestClass(IEnumerable<IInterface> interfaces)
        {
            Value = interfaces;
        }

        public IEnumerable<IInterface> Value { get; }
    }

    public class TestClass
    {
        public TestClass(string field)
        {
            this.Field = field;
        }
        public string Field { get; }
    }

    #endregion
}
