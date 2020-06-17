using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unity.Specification.Diagnostic.Field.Validation
{
    public abstract partial class SpecificationTests
    {
        [Ignore]
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NoReuse()
        {
            // Arrange
            var field = Inject.Field(nameof(DependencyInjectedType.NormalField), "test");

            // Act
            Container.RegisterType<DependencyInjectedType>("1", field)
                     .RegisterType<DependencyInjectedType>("2", field);
        }

        [Ignore]
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void InjectReadOnlyField()
        {
            // Act
            Container.RegisterType<DependencyInjectedType>(
                Inject.Field(nameof(DependencyInjectedType.ReadonlyField), "test"));
        }

        [Ignore]
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void InjectPrivateField()
        {
            // Act
            Container.RegisterType<DependencyInjectedType>(
                Inject.Field("PrivateField", "test"));
        }

        [Ignore]
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void InjectProtectedField()
        {
            // Act
            Container.RegisterType<DependencyInjectedType>(
                Inject.Field("ProtectedField", "test"));
        }

        [Ignore]
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void InjectStaticField()
        {
            // Act
            Container.RegisterType<DependencyInjectedType>(
                Inject.Field(nameof(DependencyInjectedType.StaticField), "test"));
        }
    }
}
