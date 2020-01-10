﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Unity.Injection;
using Unity.Lifetime;

namespace Unity.Specification.Issues.GitHub
{
    public abstract partial class SpecificationTests 
    {
        [TestMethod]
        // https://github.com/unitycontainer/container/issues/160
        public void Issue_Container_160()
        {
            for (var i = 0; i < 10000; i++)
            {
                var container = GetContainer()
                    .RegisterType<IFoo, Foo>()
                    .RegisterType<IBar, Bar>()
                    // It's important the name is random
                    .RegisterType<IBar, Bar>(Guid.NewGuid().ToString());

                var child = container
                    .CreateChildContainer()
                    .RegisterType<IFoo, Foo>(new ContainerControlledLifetimeManager());

                var registrations = child.Registrations
                    .Where(r => r.RegisteredType == typeof(IFoo))
                    .ToList();

                Assert.IsNotNull(
                    registrations.FirstOrDefault(r => r.LifetimeManager is ContainerControlledLifetimeManager),
                    "Singleton registration not found on iteration #" + i);

                // This check fails on random iteration, usually i < 300.
                // It passes for v.5.8.13 but fails for v.5.9.0 and later both for .NET Core and for Framework.
                var registration = registrations.FirstOrDefault(r => r.LifetimeManager is TransientLifetimeManager);
                Assert.IsNull(registration, "Transient registration found on iteration #" + i);
            }
        }

        [TestMethod]
        // https://github.com/unitycontainer/container/issues/140
        public void Issue_Container_140()
        {
            // Setup
            var noOverride = "default";
            var parOverride = "custom-via-parameteroverride";
            var depOverride = "custom-via-dependencyoverride";

            Container.RegisterType<Foo>(Invoke.Constructor(noOverride));
            // Act
            var defaultValue = Container.Resolve<Foo>().ToString();
            var depValue = Container.Resolve<Foo>(Override.Dependency<string>(depOverride))
                                       .ToString();
            var parValue = Container.Resolve<Foo>(Override.Parameter<string>(parOverride))
                                       .ToString();

            // Verify
            Assert.AreSame(noOverride, defaultValue);
            Assert.AreSame(parOverride, parValue);
            Assert.AreSame(noOverride, depValue);
        }

        [TestMethod]
        // https://github.com/unitycontainer/container/issues/136
        public void Container_136()
        {
            // Setup
            Container.RegisterType<IAnimal, Cat>();

            var child = Container.CreateChildContainer()
                                 .RegisterType<IAnimal, Dog>(); //this should overwrite previous registration

            // Act
            var zoo = child.Resolve<Zoo>();
            var animal = zoo.GetAnimal();

            // Verify
            Assert.IsNotNull(zoo);
            Assert.IsNotNull(animal);
            Assert.IsInstanceOfType(animal, typeof(Dog));
        }

        [TestMethod]
        // https://github.com/unitycontainer/container/issues/136
        public void Container_136_Ctor()
        {
            // Setup
            Container.RegisterType<IAnimal, Cat>();

            var child = Container.CreateChildContainer()
                                 .RegisterType<IAnimal, Dog>(); //this should overwrite previous registration
            // Act
            var zoo = child.Resolve<Zoo>();
            var animal = zoo.GetAnimal();

            // Verify
            Assert.IsNotNull(zoo);
            Assert.IsNotNull(animal);
            Assert.IsInstanceOfType(animal, typeof(Dog));
        }

        [TestMethod]
        // https://github.com/unitycontainer/container/issues/129
        public void Container_129()
        {
            var config = "production.sqlite";

            // Setup
            Container.RegisterType<IProctRepository, ProctRepository>("DEBUG");
            Container.RegisterType<IProctRepository, ProctRepository>("PROD", Invoke.Constructor(config));

            // Act
            var ur = Container.Resolve<ProctRepository>();
            var qa = Container.Resolve<IProctRepository>("DEBUG");
            var prod = Container.Resolve<IProctRepository>("PROD");

            // Verify
            Assert.AreEqual(ur.Value, "default.sqlite");
            Assert.AreEqual(prod.Value, config);
            Assert.AreNotEqual(qa.Value, config);
        }

        [TestMethod]
        // https://github.com/unitycontainer/container/issues/67
        public void Container_67()
        {
            Container.RegisterType<ILogger, MockLogger>(new TransientLifetimeManager());

            var child = Container.CreateChildContainer();

            child.RegisterType<OtherService>(new TransientLifetimeManager());

            Assert.IsTrue(child.IsRegistered<ILogger>());
            Assert.IsFalse(child.IsRegistered<MockLogger>());
            Assert.IsTrue(child.IsRegistered<OtherService>());

            Container.RegisterType<IOtherService, OtherService>();

            child = child.CreateChildContainer();

            Assert.IsTrue(child.IsRegistered<ILogger>());
            Assert.IsFalse(child.IsRegistered<MockLogger>());
            Assert.IsTrue(child.IsRegistered<IOtherService>());
            Assert.IsTrue(child.IsRegistered<OtherService>());
        }

    }
}
