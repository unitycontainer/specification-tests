using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unity.Specification.Constructor.Injection
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public virtual void NoDefaultConstructor()
        {
            // Act
            Container.RegisterType<ClassWithTreeConstructors>(Invoke.Constructor());
        }

        [TestMethod]
        public virtual void NoBogusConstructor()
        {
            // Act
            Container.RegisterType<ClassWithTreeConstructors>(
                Invoke.Constructor(typeof(int), typeof(string)));
        }

        [TestMethod]
        public virtual void NoBogusValuesConstructor()
        {
            // Act
            Container.RegisterType<ClassWithTreeConstructors>(
                Invoke.Constructor( 1, "test"));
        }

        [TestMethod]
        public void SelectByValueTypes()
        {
            Container.RegisterType<TypeWithMultipleCtors>(Invoke.Constructor(Inject.Parameter(typeof(string)),
                Inject.Parameter(typeof(string)),
                Inject.Parameter(typeof(int))));
            Assert.AreEqual(TypeWithMultipleCtors.Three, Container.Resolve<TypeWithMultipleCtors>().Signature);
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
