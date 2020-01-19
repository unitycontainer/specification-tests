using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Injection;
using Unity.Lifetime;

namespace Unity.Specification.Diagnostic.Registration.Types
{
    public abstract partial class SpecificationTests
    {
        public static IEnumerable<object[]> ArgumetTestData
        {
            get
            {
                yield return new object[] { typeof(IService), typeof(Service),  null, null,                      typeof(TransientLifetimeManager) };
                yield return new object[] { typeof(object),   typeof(object),   null, null,                      typeof(TransientLifetimeManager) };
                yield return new object[] { null,             typeof(object),   null, null,                      typeof(TransientLifetimeManager) };
                yield return new object[] { typeof(object),   null,             null, null,                      typeof(TransientLifetimeManager) };
                yield return new object[] { typeof(object),   typeof(object),   Name, null,                      typeof(TransientLifetimeManager) };
                yield return new object[] { null,             typeof(object),   Name, null,                      typeof(TransientLifetimeManager) };
                yield return new object[] { typeof(object),   null,             Name, null,                      typeof(TransientLifetimeManager) };
                yield return new object[] { typeof(object),   typeof(object),   null, TypeLifetime.PerContainer, typeof(ContainerControlledLifetimeManager) };
                yield return new object[] { null,             typeof(object),   null, TypeLifetime.PerContainer, typeof(ContainerControlledLifetimeManager) };
                yield return new object[] { typeof(object),   null,             null, TypeLifetime.PerContainer, typeof(ContainerControlledLifetimeManager) };
                yield return new object[] { typeof(object),   typeof(object),   Name, TypeLifetime.PerContainer, typeof(ContainerControlledLifetimeManager) };
                yield return new object[] { null,             typeof(object),   Name, TypeLifetime.PerContainer, typeof(ContainerControlledLifetimeManager) };
                yield return new object[] { typeof(object),   null,             Name, TypeLifetime.PerContainer, typeof(ContainerControlledLifetimeManager) };
            }
        }

        public static IEnumerable<object[]> ArgumetTestDataFailing
        {
            get
            {
                yield return new object[] { typeof(Service),  typeof(IService), null, null,                      typeof(ArgumentException) };
                yield return new object[] { null,             null,             null, null,                      typeof(ArgumentException) };
                yield return new object[] { null,             null,             Name, null,                      typeof(ArgumentException) };
                yield return new object[] { null,             null,             null, TypeLifetime.PerContainer, typeof(ArgumentException) };
                yield return new object[] { null,             null,             Name, TypeLifetime.PerContainer, typeof(ArgumentException) };
            }
        }


        [DataTestMethod]
        [DynamicData(nameof(ArgumetTestData))]
        public void ArgumentValidationDiagnostic(Type typeFrom, Type typeTo, string name, ITypeLifetimeManager lifetimeManager, Type manager)
        {
            // Act
            Container.RegisterType(typeFrom, typeTo, name, lifetimeManager);

            var registeredType = typeFrom ?? typeTo;
            var registration = Container.Registrations.FirstOrDefault(r => r.RegisteredType == registeredType && r.Name == name);

            Assert.IsNotNull(registration);
            Assert.IsInstanceOfType(registration.LifetimeManager, manager);
        }


        [DataTestMethod]
        [DynamicData(nameof(ArgumetTestDataFailing))]
        public void ArgumentValidationDiagnosticFailing(Type typeFrom, Type typeTo, string name, ITypeLifetimeManager lifetimeManager, Type exception)
        {
            try
            {
                // Act
                Container.RegisterType(typeFrom, typeTo, name, lifetimeManager);
                Assert.Fail("Did not throw and exception of type {exception?.Name}");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, exception);
            }
        }
    }
}
