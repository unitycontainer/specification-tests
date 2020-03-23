using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Unity.Specification.Container.Hierarchy
{
    public abstract partial class SpecificationTests : TestFixtureBase
    {
        [TestInitialize]
        public override void Setup()
        {
            Container = GetContainer();
        }
    }

    #region Test Data

    public class IUnityContainerInjectionClass
    {
        [Dependency]
        public IUnityContainer Container { get; set; }
    }

    public interface ITemporary
    {
    }

    public class Temp : ITemporary
    {
    }

    public class Temporary : ITemporary
    {
    }

    public class SpecialTemp : ITemporary //Second level
    {
    }

    #pragma warning disable CA1063 // Implement IDisposable Correctly
    public class MyDisposableObject : IDisposable
    {
        private bool wasDisposed = false;

        public bool WasDisposed
        {
            get { return wasDisposed; }
            set { wasDisposed = value; }
        }

        public void Dispose()
        {
            wasDisposed = true;
        }
    }
    #pragma warning restore CA1063 // Implement IDisposable Correctly

    #endregion
}
