using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Attributes;
using Unity.Exceptions;
using Unity.Lifetime;
using Unity.Registration;
using Unity.Specification.TestData;

namespace Unity.Specification.Issues
{
    public abstract partial class ReportedIssuesTests
    {
        [TestMethod]
        public void unitycontainer_container_67()
        {
            var container = GetContainer();

            container.RegisterType<ILogger, MockLogger>(new TransientLifetimeManager());

            var child = container.CreateChildContainer();

            child.RegisterType<OtherService>(new TransientLifetimeManager());

            Assert.IsTrue(child.IsRegistered<ILogger>());
            Assert.IsFalse(child.IsRegistered<MockLogger>());
            Assert.IsTrue(child.IsRegistered<OtherService>());

            container.RegisterType<IOtherService, OtherService>();

            child = child.CreateChildContainer();

            Assert.IsTrue(child.IsRegistered<ILogger>());
            Assert.IsFalse(child.IsRegistered<MockLogger>());
            Assert.IsTrue(child.IsRegistered<IOtherService>());
            Assert.IsTrue(child.IsRegistered<OtherService>());
        }

        [TestMethod]
        public void unitycontainer_unity_201()
        {
            var container = GetContainer();

            Assert.ThrowsException<InvalidOperationException>(() =>
                container.RegisterType<IService, OtherService>(
                    new InjectionFactory((c, t, n) => new OtherService())));
        }

        [TestMethod]
        public void unitycontainer_unity_177()
        {
            var container = GetContainer();

            container.RegisterType<OtherService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IService, OtherService>();
            container.RegisterType<IOtherService, OtherService>();


            Assert.AreSame(container.Resolve<IService>(), container.Resolve<IOtherService>());
        }

        [TestMethod]
        public void unitycontainer_unity_165()
        {
            var container = GetContainer();
            container.RegisterType<ILogger>( new HierarchicalLifetimeManager(),
                                             new InjectionFactory( c => new MockLogger()));

            Assert.AreSame(container.Resolve<ILogger>(), container.Resolve<ILogger>());
            Assert.AreNotSame(container.Resolve<ILogger>(), container.CreateChildContainer().Resolve<ILogger>());
        }

        [TestMethod]
        public void unitycontainer_unity_164()
        {
            var container = GetContainer();

            container.RegisterType<ILogger, MockLogger>();
            var foo2 = new MockLogger();

            container.RegisterType<ILogger>(new InjectionFactory(x => foo2));
            var result = container.Resolve<ILogger>();

            Assert.AreSame(result, foo2);
        }
        
        [TestMethod]
        public void unitycontainer_unity_156()
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
        public void unitycontainer_unity_154_2()
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
        public void unitycontainer_unity_154_1()
        {
            IUnityContainer container = GetContainer();
            container.RegisterType<OtherService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IService, OtherService>();
            container.RegisterType<IOtherService, OtherService>();

            Assert.AreSame(container.Resolve<IService>(),
                           container.Resolve<IOtherService>());
        }


        [TestMethod]
        public void unitycontainer_unity_153()
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
        public void unitycontainer_unity_88()
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
        public void unitycontainer_unity_54() 
        {
            using (IUnityContainer container = GetContainer())
            {
                container.RegisterType(typeof(ITestClass), typeof(TestClass));
                container.RegisterInstance(new TestClass());
                var instance = container.Resolve<ITestClass>(); //0
                Assert.IsNotNull(instance);
            }

            using (IUnityContainer container = GetContainer())
            {
                container.RegisterType(typeof(ITestClass), typeof(TestClass));
                container.RegisterType<TestClass>(new ContainerControlledLifetimeManager());

                try
                {
                    var instance = container.Resolve<ITestClass>(); //2
                    Assert.IsNull(instance, "Should threw an exception");
                }
                catch (ResolutionFailedException e)
                {
                    Assert.IsInstanceOfType(e, typeof(ResolutionFailedException));
                }

            }
        }

        [TestMethod]
        public void unitycontainer_unity_35()
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
