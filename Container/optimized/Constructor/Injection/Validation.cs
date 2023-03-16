using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Unity.Specification.Registration.Extended;

namespace Unity.Specification.Constructor.Injection
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NoConstructor()
        {
            // Act
            Container.RegisterType<TypeWithAmbiguousCtors>(
                Invoke.Constructor(Resolve.Parameter()));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NoDefaultConstructor()
        {
            // Act
            Container.RegisterType<ClassWithTreeConstructors>(Invoke.Constructor());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NoBogusConstructor()
        {
            // Act
            Container.RegisterType<ClassWithTreeConstructors>(
                Invoke.Constructor(typeof(int), typeof(string)));
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NoBogusValuesConstructor()
        {
            // Act
            Container.RegisterType<ClassWithTreeConstructors>(
                Invoke.Constructor( 1, "test"));
        }


        #region Test Data

        public class ClassWithTreeConstructors
        {
            protected ClassWithTreeConstructors()
            {
                
            }

            public ClassWithTreeConstructors(IUnityContainer container)
            {
                Value = container;
            }

            public ClassWithTreeConstructors(string name)
            {
                Value = name;
            }

            public ClassWithTreeConstructors(object value)
            {
                Value = value;
            }

            public object Value { get; }
        }

        #endregion
    }
}
