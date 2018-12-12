using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unity.Specification.Injection.Constructor
{
    public abstract partial class SpecificationTests : TestFixtureBase
    {
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();

            Container.RegisterInstance(TypeWithAmbiguousCtors.Four);
            Container.RegisterInstance(TypeWithAmbiguousCtors.Five, TypeWithAmbiguousCtors.Five);
        }

        [DataTestMethod]
        [DynamicData(nameof(ConstructorSelectionTestData))]
        public void Selection(string name, Type typeFrom, Type typeTo, Type typeToResolve, object[] parameters, Func<object, bool> validator)
        {
            // Setup
            Container.RegisterType(typeFrom, typeTo, name, null, Invoke.Constructor(parameters));

            // Act
            var result = Container.Resolve(typeToResolve, name);

            // Verify
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeToResolve);
            Assert.IsTrue(validator?.Invoke(result) ?? true);
        }

        [DataTestMethod]
        [DynamicData(nameof(RegistrationFailedTestData))]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Validation(Type typeFrom, Type typeTo, string name, object[] parameters)
        {
            // Act
            Container.RegisterType(typeFrom, typeTo, name, null, Invoke.Constructor(parameters));
        }


        [DataTestMethod]
        [DynamicData(nameof(DefaultConstructorTestData))]
        public void Default(Type typeFrom, Type typeTo, string name, Type typeToResolve)
        {
            // Setup
            Container.RegisterType(typeFrom, typeTo, name, null, Invoke.Constructor());

            // Act
            var result = Container.Resolve(typeToResolve, name);

            // Verify
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeToResolve);
        }


        [DataTestMethod]
        [DynamicData(nameof(DefaultConstructorTestDataFailed))]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void DefaultCtorValidation(Type type, string name)
        {
            // Setup
            Container.RegisterType(null, type, name, null, Invoke.Constructor());

            // Act
            var result = Container.Resolve(type, name);
        }
    }
}
