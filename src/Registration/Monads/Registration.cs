using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.ComponentModel;
using System.Runtime.Versioning;
using Unity;

namespace Unity.Specification.Registration.Monads
{
    public abstract partial class SpecificationTests
    {

        [EditorBrowsable(EditorBrowsableState.Never)]
        public interface IFluentInterface
        {
            [EditorBrowsable(EditorBrowsableState.Never)]
            Type GetType();

            [EditorBrowsable(EditorBrowsableState.Never)]
            int GetHashCode();

            [EditorBrowsable(EditorBrowsableState.Never)]
            string ToString();

            [EditorBrowsable(EditorBrowsableState.Never)]
            bool Equals(object obj);
        }


        public class Test : IFluentInterface
        {
            [EditorBrowsable(EditorBrowsableState.Never)]
            [ResourceExposure(ResourceScope.None)]
            [ResourceConsumption(ResourceScope.Process, ResourceScope.Process)]
            public void TestMethod() { }


            [Obsolete("This is an override of Object.Equals(). Call Assert.Equal() instead.", true)]
            [EditorBrowsable(EditorBrowsableState.Never)]
            public new static bool Equals(object a, object b)
            {
                throw new InvalidOperationException("Assert.Equals should not be used");
            }



            /// <summary>Do not call this method.</summary>
            [Obsolete("This is an override of Object.ReferenceEquals(). Call Assert.Same() instead.", true)]
            [EditorBrowsable(EditorBrowsableState.Never)]
            public new static bool ReferenceEquals(object a, object b)
            {
                throw new InvalidOperationException("Assert.ReferenceEquals should not be used");
            }
        }

        [TestMethod]
        public void Registration_ShowsUpInRegistrationsSequence()
        {
            //Container.Type<Service>("name")
            //         .Invoke.Constructor()
            //                .Method()
            //                .Factory((c) => c)
                            
            //         //       .Method("Test", 1, 2, 3)
            //         //       .Method("Another", 1, Resolve.Parameter(), 3)
            //         //.Resolve.Field("Field")
            //         //        .Property("Property")
            //         //.Inject.Field("111")
            //         //       .Field("222")
            //         //.Invoke.Constructor()
            //         //.Lifetime.Hierarchical
            //         //.RegisterAs<IService>();
            //         ;
            //Container.Type<object>("Name");

            //var test = new Test();

            //test.TestMethod();

            //Assert .IsTrue(true);
        }
    }



}
