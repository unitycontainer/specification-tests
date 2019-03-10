using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unity.Specification.Constructor.Injection
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void ByCountFirst()
        {
            // Arrange
            #region inject_count_first_arrange

            Container.RegisterType<SampleType>( Invoke.Constructor( Resolve.Parameter() ));

            #endregion

            // Act
            #region inject_count_first_ctor_act

            var instance = Container.Resolve<SampleType>();

            // 1 == instance.Ctor

            #endregion

            // Validate
            Assert.AreEqual(1, instance.Ctor);
        }

        [TestMethod]
        public void ByCountFirstGeneric()
        {
            // Arrange
            #region inject_count_first_arrange_generic

            Container.RegisterType(typeof(SampleType<>), Invoke.Constructor( Resolve.Parameter() ));

            #endregion

            // Act
            #region inject_count_first_ctor_act_generic

            var instance = Container.Resolve<SampleType<object>>();

            // 1 == instance.Ctor

            #endregion

            // Validate
            Assert.AreEqual(1, instance.Ctor);
        }

        [TestMethod]
        public void ByCountNamedGeneric()
        {
            // Arrange
            #region inject_count_named_generic
            Container.RegisterType<IService, Service>()
                     .RegisterType<IService, ServiceOne>("one")
                     .RegisterType<IService, ServiceTwo>("two");

            Container.RegisterType(typeof(SampleType<>),
                Invoke.Constructor(
                    Resolve.Parameter(),
                    Resolve.Parameter()));

            var instance = Container.Resolve<SampleType<object>>();

            // 2 == instance.Ctor
            // typeof(Service) == instance.Service.GetType()

            #endregion

            // Validate
            Assert.AreEqual(2, instance.Ctor);
            Assert.IsTrue(typeof(Service) == instance.Service.GetType());
        }

        [TestMethod]
        public void ByCountNameOverrideGeneric()
        {
            // Arrange
            Container.RegisterType<IService, ServiceOne>("one")
                     .RegisterType<IService, ServiceTwo>("two");

            #region inject_count_name_override_generic

            Container.RegisterType(typeof(SampleType<>),
                Invoke.Constructor(
                    Resolve.Parameter("two"),
                    Resolve.Parameter()));

            var instance = Container.Resolve<SampleType<object>>();

            // 2 == instance.Ctor
            // typeof(ServiceTwo) == instance.Service.GetType()

            #endregion

            // Validate
            Assert.AreEqual(2, instance.Ctor);
            Assert.IsTrue(typeof(ServiceTwo) == instance.Service.GetType());
        }
    }

    #region class_sample_type
    public class SampleType
    {
        public SampleType(object arg) => Ctor = 1;

        public SampleType(IService service, object arg) => Ctor = 2;

        public SampleType(IService service, object arg, Type type) => Ctor = 3;

        public int Ctor { get; } // Constructor called 
    }
    #endregion

    #region class_sample_type_generic
    public class SampleType<T>
    {
        public SampleType(T arg)
        {
            Ctor = 1;
            Value = arg;
        }

        public SampleType([Dependency("one")]IService service, T arg)
        {
            Ctor = 2;
            Service = service;
            Value = arg;
        }

        public SampleType(IService service, T arg, object obj)
        {
            Ctor = 3;
            Service = service;
            Value = arg;
        }

        public int Ctor { get; }        // Called Constructor
        public IService Service { get; }// Service passed in
        public T Value { get; }         // Generic argument value
    }
    #endregion

}
