using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Injection;
using Unity.Lifetime;
using Unity.Specification.Container.IsRegistered;

namespace Unity.Specification.Issues.GitHub
{

    public abstract partial class SpecificationTests : TestFixtureBase
    {
        [TestMethod]
        // https://github.com/unitycontainer/container/issues/210
        public void Unity_308()
        {
            // Arrange
            Container.RegisterSingleton(typeof(IFoo<>), typeof(Foo<>));

            IFoo<object> foo1 = null;
            IFoo<object> foo2 = null;

            // Act 
            using (var scope = Container.CreateChildContainer())
            {
                foo1 = scope.Resolve<IFoo<object>>();
            }

            using (var scope = Container.CreateChildContainer())
            {
                foo2 = scope.Resolve<IFoo<object>>();
            }

            // Validate
            Assert.IsNotNull(foo1);
            Assert.IsNotNull(foo2);
            Assert.AreSame(foo1, foo2);
        }

        [TestMethod]
        // https://github.com/unitycontainer/container/issues/206
        public void Unity_306()
        {
            // Arrange
            Container.RegisterType<IService, Service>("strategy1");
            Container.RegisterType<IService, OtherService>("strategy2");
            
            // Act / Validate
            Container.Resolve<Consumer>().Consume();
        }

        [TestMethod]
        public void Unity_211()
        {
            var container = GetContainer();

            container.RegisterType<IThing, Thing>();
            container.RegisterType<IThing, Thing>("SecondConstructor",
                new InjectionConstructor(typeof(int)));

            container.RegisterType<IGeneric<IThing>, Gen1>(nameof(Gen1));
            container.RegisterType<IGeneric<IThing>, Gen2>(nameof(Gen2));

            var things = container.ResolveAll(typeof(IGeneric<IThing>)); //Throws exception
            Assert.AreEqual(things.Count(), 2);
        }

        [TestMethod]
        public void unitycontainer_microsoft_dependency_injection_14()
        {
            var container = GetContainer();

            var c1 = container.CreateChildContainer();
            var c2 = container.CreateChildContainer();

            c1.RegisterType(typeof(IList<>), typeof(List<>), new ContainerControlledLifetimeManager(),
                                                             new InjectionConstructor());
            var t1 = c1.Resolve<IList<int>>();
            Assert.IsNotNull(t1);

            c2.RegisterType(typeof(IList<>), typeof(List<>), new ContainerControlledLifetimeManager(),
                                                             new InjectionConstructor());
            var t2 = c2.Resolve<IList<int>>();
            Assert.IsNotNull(t2);

            Assert.AreNotSame(t2, t1);

        }

        [TestMethod]
        public void Unity_177()
        {
            var container = GetContainer();

            container.RegisterType<OtherService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IService, OtherService>();
            container.RegisterType<IOtherService, OtherService>();


            Assert.AreSame<object>(container.Resolve<IService>(), container.Resolve<IOtherService>());
        }

        [TestMethod]
        public void Unity_165()
        {
            var container = GetContainer();
            container.RegisterFactory<ILogger>(c => new MockLogger(), FactoryLifetime.Hierarchical);

            Assert.AreSame(container.Resolve<ILogger>(), container.Resolve<ILogger>());
            Assert.AreNotSame(container.Resolve<ILogger>(), container.CreateChildContainer().Resolve<ILogger>());
        }

        [TestMethod]
        public void Unity_164()
        {
            var container = GetContainer();

            container.RegisterType<ILogger, MockLogger>();
            var foo2 = new MockLogger();

            container.RegisterFactory<ILogger>(x => foo2);
            var result = container.Resolve<ILogger>();

            Assert.AreSame(result, foo2);
        }
        
        [TestMethod]
        public void Unity_156()
        {
            using (var container = GetContainer())
            {
                var td = new Service();

                container.RegisterFactory<Service>(_ => td);
                container.RegisterType<IService, Service>();

                Assert.AreSame(td, container.Resolve<IService>());
                Assert.AreSame(td, container.Resolve<Service>());
            }
            using (var container = GetContainer())
            {
                var td = new Service();

                container.RegisterFactory<Service>(_ => td);
                container.RegisterType<IService, Service>();

                Assert.AreSame(td, container.Resolve<Service>());
                Assert.AreSame(td, container.Resolve<IService>());
            }
        }

        [TestMethod]
        public void Unity_154_2()
        {
            IUnityContainer container = GetContainer();
            container.RegisterType<OtherService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IService, OtherService>();
            container.RegisterType<IOtherService, OtherService>(new InjectionConstructor(container));

            Assert.AreNotSame<object>(container.Resolve<IService>(),
                              container.Resolve<IOtherService>());

            Assert.AreSame(container.Resolve<IService>(),
                           container.Resolve<OtherService>());
        }


        [TestMethod]
        public void Unity_154_1()
        {
            IUnityContainer container = GetContainer();
            container.RegisterType<OtherService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IService, OtherService>();
            container.RegisterType<IOtherService, OtherService>();

            Assert.AreSame<object>(container.Resolve<IService>(),
                           container.Resolve<IOtherService>());
        }


        [TestMethod]
        public void Unity_153()
        {
            IUnityContainer rootContainer = GetContainer();
            rootContainer.RegisterType<IService, Service>(new HierarchicalLifetimeManager());

            using (IUnityContainer childContainer = rootContainer.CreateChildContainer())
            {
                var a = childContainer.Resolve<IService>();
                var b = childContainer.Resolve<IService>();

                Assert.AreSame(a, b);
            }
        }

        [TestMethod]
        public void Unity_88()
        {
            using (var unityContainer = GetContainer())
            {
                unityContainer.RegisterInstance(true);
                unityContainer.RegisterInstance("true", true);
                unityContainer.RegisterInstance("false", false);

                var resolveAll = unityContainer.ResolveAll(typeof(bool));
                Assert.IsNotNull(resolveAll.Select(o => o.ToString()).ToArray());
            }
        }

        [TestMethod]
        public void Unity_35()
        {
            var container = GetContainer();

            container.RegisterType<IService, Service>(new ContainerControlledLifetimeManager());
            IService logger = container.Resolve<IService>();

            Assert.IsNotNull(logger);
            Assert.AreSame(container.Resolve<IService>(), logger);

            container.RegisterType<Service>(new TransientLifetimeManager());

            Assert.AreSame(container.Resolve<IService>(), logger);
        }
    }
}
