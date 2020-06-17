using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Constructor.Injection
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public virtual void NoDefaultConstructor()
        {
            // Arrange
            Container.RegisterType<ClassWithTreeConstructors>(Invoke.Constructor());

            // Act
            var instance = Container.Resolve<ClassWithTreeConstructors>();
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public virtual void NoBogusConstructor()
        {
            // Arrange
            Container.RegisterType<ClassWithTreeConstructors>(
                Invoke.Constructor(typeof(int), typeof(string)));

            // Act
            var instance = Container.Resolve<ClassWithTreeConstructors>();
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public virtual void NoBogusValuesConstructor()
        {
            // Arrange
            Container.RegisterType<ClassWithTreeConstructors>(
                Invoke.Constructor( 1, "test"));

            // Act
            var instance = Container.Resolve<ClassWithTreeConstructors>();
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
