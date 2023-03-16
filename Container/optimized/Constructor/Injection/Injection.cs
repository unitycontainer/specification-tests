using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Unity.Specification.TestData;

namespace Unity.Specification.Constructor.Injection
{
    public abstract partial class SpecificationTests
    {

        [TestMethod]
        public virtual void MultipleConstructor()
        {
            // Arrange
            Container.RegisterType<TypeWithAmbiguousCtors>(
                Invoke.Constructor(),
                Invoke.Constructor());

            // Act
            var instance = Container.Resolve<TypeWithAmbiguousCtors>();

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(TypeWithAmbiguousCtors.One, instance.Signature);
        }


        [TestMethod]
        public void SelectByValueTypes()
        {
            Container.RegisterType<TypeWithMultipleCtors>(Invoke.Constructor(Inject.Parameter(typeof(string)),
                Inject.Parameter(typeof(string)),
                Inject.Parameter(typeof(int))));
            Assert.AreEqual(TypeWithMultipleCtors.Three, Container.Resolve<TypeWithMultipleCtors>().Signature);
        }
    }
}
