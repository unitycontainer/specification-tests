using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unity.Specification.Constructor.Injection
{
    public abstract partial class SpecificationTests : TestFixtureBase
    {
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();

            Container.RegisterInstance(TypeWithMultipleCtors.Four);
            Container.RegisterInstance(TypeWithMultipleCtors.Five, TypeWithMultipleCtors.Five);
        }
    }

    #region types_service

    public interface IService
    {
    }


    #region class_service
    public class Service : IService
    {
        public Service() => Ctor = 1;

        public Service(object arg) => Ctor = 2;

        public Service(IUnityContainer container) => Ctor = 3;

        public int Ctor { get; }    // Constructor called 
    }
    #endregion

    #region class_service_generic
    public class Service<T>
    {
        public Service() => Ctor = 1;

        public Service(T arg) => Ctor = 2;

        public int Ctor { get; }    // Constructor called 
    }
    #endregion

    public class ServiceOne : IService
    {
    }

    public class ServiceTwo : IService
    {
    }

    #endregion
}
