using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Injection;

namespace Unity.Specification.Issues.GitHub
{

    public abstract partial class SpecificationTests : TestFixtureBase
    {
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
        public void Unity_201()
        {
            var container = GetContainer();

            Assert.ThrowsException<InvalidOperationException>(() =>
                container.RegisterType<IService, OtherService>(
                    new InjectionFactory((c, t, n) => new OtherService())));
        }

        [TestMethod]
        public void Unity_177()
        {
            var container = GetContainer();

            container.RegisterType<OtherService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IService, OtherService>();
            container.RegisterType<IOtherService, OtherService>();


            Assert.AreSame(container.Resolve<IService>(), container.Resolve<IOtherService>());
        }

        [TestMethod]
        public void Unity_165()
        {
            var container = GetContainer();
            container.RegisterType<ILogger>( new HierarchicalLifetimeManager(),
                                             new InjectionFactory( c => new MockLogger()));

            Assert.AreSame(container.Resolve<ILogger>(), container.Resolve<ILogger>());
            Assert.AreNotSame(container.Resolve<ILogger>(), container.CreateChildContainer().Resolve<ILogger>());
        }

        [TestMethod]
        public void Unity_164()
        {
            var container = GetContainer();

            container.RegisterType<ILogger, MockLogger>();
            var foo2 = new MockLogger();

            container.RegisterType<ILogger>(new InjectionFactory(x => foo2));
            var result = container.Resolve<ILogger>();

            Assert.AreSame(result, foo2);
        }
        
        [TestMethod]
        public void Unity_156()
        {
            using (var container = GetContainer())
            {
                var td = new Service();

                container.RegisterType<Service>(new ContainerControlledLifetimeManager(), new InjectionFactory(_ => td));
                container.RegisterType<IService, Service>();

                Assert.AreSame(td, container.Resolve<IService>());
                Assert.AreSame(td, container.Resolve<Service>());
            }
            using (var container = GetContainer())
            {
                var td = new Service();

                container.RegisterType<Service>(new ContainerControlledLifetimeManager(), new InjectionFactory(_ => td));
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

            Assert.AreNotSame(container.Resolve<IService>(),
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

            Assert.AreSame(container.Resolve<IService>(),
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

        // Test types 
        public interface ITestClass
        { }

        public class TestClass : ITestClass
        {
            public TestClass()
            { }

            [InjectionConstructor]
            public TestClass(TestClass _) //1
            {
            }
        }
    }
}
