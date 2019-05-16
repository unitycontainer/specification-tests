using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using Unity.Lifetime;

namespace Unity.Specification.Lifetime
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void LegacyCheckSetSingletonDoneTwice()
        {
            Container.RegisterType<Service>(new LegacyPerContainer())
                     .RegisterType<Service>("hello", new LegacyPerContainer());

            var obj = Container.Resolve<Service>();
            var obj1 = Container.Resolve<Service>("hello");

            Assert.AreNotSame(obj, obj1);
        }

        [TestMethod]
        public void LegacyCheckSingletonWithDependencies()
        {
            Container.RegisterType<ObjectWithOneDependency>(new LegacyPerContainer());

            var result1 = Container.Resolve<ObjectWithOneDependency>();
            var result2 = Container.Resolve<ObjectWithOneDependency>();

            Assert.IsNotNull(result1);
            Assert.IsNotNull(result2);
            Assert.IsNotNull(result1.InnerObject);
            Assert.IsNotNull(result2.InnerObject);
            Assert.AreSame(result1, result2);
        }

        [TestMethod]
        public void LegacyCheckSingletonAsDependencies()
        {
            Container.RegisterType<ObjectWithOneDependency>(new LegacyPerContainer());

            var result1 = Container.Resolve<ObjectWithTwoConstructorDependencies>();
            var result2 = Container.Resolve<ObjectWithTwoConstructorDependencies>();

            Assert.IsNotNull(result1);
            Assert.IsNotNull(result2);
            Assert.IsNotNull(result1.OneDep);
            Assert.IsNotNull(result2.OneDep);
            Assert.AreNotSame(result1, result2);
            Assert.AreSame(result1.OneDep, result2.OneDep);
        }

        [TestMethod]
        public void LegacyCheckRegisterInstanceDoneTwice()
        {
            var aInstance = new Service();
            Container.RegisterInstance<Service>(aInstance)
                     .RegisterInstance<Service>("hello", aInstance);

            var obj = Container.Resolve<Service>();
            var obj1 = Container.Resolve<Service>("hello");

            Assert.AreSame(obj, aInstance);
            Assert.AreSame(obj1, aInstance);
            Assert.AreSame(obj, obj1);
        }

        [TestMethod]
        public void LegacySetLifetimeTwiceWithLifetimeHandle()
        {
            Container.RegisterType<Service>(new LegacyPerContainer())
              .RegisterType<Service>("hello", new LegacyHierarchical());
            var obj = Container.Resolve<Service>();
            var obj1 = Container.Resolve<Service>("hello");

            Assert.AreNotSame(obj, obj1);
        }

        [TestMethod]
        public void LegacySetSingletonRegisterInstanceTwice()
        {
            var aInstance = new Service();
            Container.RegisterInstance<Service>(aInstance).RegisterInstance<Service>("hello", aInstance);
            var obj = Container.Resolve<Service>();
            var obj1 = Container.Resolve<Service>("hello");

            Assert.AreSame(obj, obj1);
        }

        [TestMethod]
        public void LegacySetLifetimeGetTwice()
        {
            Container.RegisterType<Service>(new LegacyPerContainer());
            var obj = Container.Resolve<Service>();
            var obj1 = Container.Resolve<Service>("hello");

            Assert.AreNotSame(obj, obj1);
        }

        [TestMethod]
        public void LegacySetSingletonRegisterInstanceTwiceSetLifetimeTwice()
        {
            var aInstance = new Service();

            Container.RegisterInstance(aInstance);
            Container.RegisterInstance("hello", aInstance);
            Container.RegisterType<Service>(new LegacyPerContainer());
            Container.RegisterType<Service>("hello1", new LegacyPerContainer());

            var obj = Container.Resolve<Service>();
            var obj1 = Container.Resolve<Service>("hello1");

            Assert.AreNotSame(obj, obj1);
        }

        [TestMethod]
        public void LegacySetSingletonByNameRegisterInstanceOnit()
        {
            var aInstance = new Service();
            Container.RegisterType<Service>("SetA", new LegacyPerContainer())
                .RegisterInstance<Service>(aInstance)
                .RegisterInstance<Service>("hello", aInstance);

            var obj = Container.Resolve<Service>("SetA");
            var obj1 = Container.Resolve<Service>();
            var obj2 = Container.Resolve<Service>("hello");

            Assert.AreSame(obj1, obj2);
            Assert.AreNotSame(obj, obj2);
        }

        [TestMethod]
        public void LegacyTestSetLifetime()
        {
            Container.RegisterType<Service>(new LegacyPerContainer())
               .RegisterType<Service>("hello", new LegacyPerContainer());

            var obj = Container.Resolve<Service>();
            var obj1 = Container.Resolve<Service>("hello");

            Assert.AreNotSame(obj, obj1);
        }

        [TestMethod]
        public void LegacyRegisterWithParentAndChild()
        {
            //register type UnityTestClass
            var child = Container.CreateChildContainer();

            Container.RegisterType<Service>(new LegacyPerContainer());
            child.RegisterType<Service>(new LegacyPerContainer());

            var mytestparent = Container.Resolve<Service>();
            var mytestchild = child.Resolve<Service>();

            Assert.AreNotSame(mytestparent, mytestchild);
        }

        [TestMethod]
        public void LegacyUseContainerControlledLifetime()
        {
            Container.RegisterType<Service>(new LegacyPerContainer());

            var parentinstance = Container.Resolve<Service>();
            var hash = parentinstance.GetHashCode();
            parentinstance = null;
            GC.Collect();

            var parentinstance1 = Container.Resolve<Service>();
            Assert.AreEqual(hash, parentinstance1.GetHashCode());
        }

        [TestMethod]
        public void LegacyTestStringEmpty()
        {
            Container.RegisterType<Service>(new LegacyPerContainer());
            Container.RegisterType<Service>(string.Empty, new LegacyPerContainer());
            Container.RegisterType<Service>(null, new LegacyPerContainer());

            Service a = Container.Resolve<Service>();
            Service b = Container.Resolve<Service>(string.Empty);
            Service c = Container.Resolve<Service>((string)null);

            Assert.AreNotEqual(a, b);
            Assert.AreNotEqual(b, c);
            Assert.AreEqual(a, c);
        }

        [TestMethod]
        public void LegacyThenResolvingInParentActsLikeContainerControlledLifetime()
        {
            Container.RegisterType<TestClass>(new LegacyHierarchical());

            var o1 = Container.Resolve<TestClass>();
            var o2 = Container.Resolve<TestClass>();
            Assert.AreSame(o1, o2);
        }

        [TestMethod]
        public void LegacyThenParentAndChildResolveDifferentInstances()
        {
            var child1 = Container.CreateChildContainer();
            Container.RegisterType<TestClass>(new LegacyHierarchical());

            var o1 = Container.Resolve<TestClass>();
            var o2 = child1.Resolve<TestClass>();
            Assert.AreNotSame(o1, o2);
        }

        [TestMethod]
        public void LegacyThenChildResolvesTheSameInstance()
        {
            var child1 = Container.CreateChildContainer();
            Container.RegisterType<TestClass>(new LegacyHierarchical());

            var o1 = child1.Resolve<TestClass>();
            var o2 = child1.Resolve<TestClass>();
            Assert.AreSame(o1, o2);
        }

        [TestMethod]
        public void LegacyThenSiblingContainersResolveDifferentInstances()
        {
            var child1 = Container.CreateChildContainer();
            var child2 = Container.CreateChildContainer();
            Container.RegisterType<TestClass>(new LegacyHierarchical());

            var o1 = child1.Resolve<TestClass>();
            var o2 = child2.Resolve<TestClass>();
            Assert.AreNotSame(o1, o2);
        }

        [TestMethod]
        public void LegacyThenDisposingOfChildContainerDisposesOnlyChildObject()
        {
            var child1 = Container.CreateChildContainer();
            Container.RegisterType<TestClass>(new LegacyHierarchical());

            var o1 = Container.Resolve<TestClass>();
            var o2 = child1.Resolve<TestClass>();

            child1.Dispose();
            Assert.IsFalse(o1.Disposed);
            Assert.IsTrue(o2.Disposed);
        }

        [TestMethod]
        public void LegacySameInstanceFromMultipleThreads()
        {
            Container.RegisterFactory<IService>((c, t, n) => new Service(), new LegacyPerContainer());

            object result1 = null;
            object result2 = null;

            Thread thread1 = new Thread(delegate ()
            {
                result1 = Container.Resolve<IService>();
            });

            Thread thread2 = new Thread(delegate ()
            {
                result2 = Container.Resolve<IService>();
            });

            thread1.Name = "1";
            thread2.Name = "2";

            thread1.Start();
            thread2.Start();

            thread2.Join();
            thread1.Join();

            Assert.IsNotNull(result1);
            Assert.AreSame(result1, result2);
        }

        [TestMethod]
        public void LegacyContainerControlledLifetimeDoesNotLeaveHangingLockIfBuildThrowsException()
        {
            int count = 0;
            bool fail = false;
            Func<IUnityContainer, Type, string, object> factory = (c, t, n) =>
            {
                fail = !fail;
                Interlocked.Increment(ref count);
                return !fail ? new Service() : throw new InvalidOperationException();
            };
            Container.RegisterFactory<IService>(factory, new LegacyPerContainer());

            object result1 = null;
            object result2 = null;
            bool thread2Finished = false;

            Thread thread1 = new Thread(
                delegate ()
                {
                    try
                    {
                        result1 = Container.Resolve<IService>();
                    }
                    catch (ResolutionFailedException)
                    {
                    }
                });

            Thread thread2 = new Thread(
                delegate ()
                {
                    result2 = Container.Resolve<IService>();
                    thread2Finished = true;
                });

            thread1.Start();
            thread1.Join();

            // Thread1 threw an exception. However, lock should be correctly freed.
            // Run thread2, and if it finished, we're ok.

            thread2.Start();
            thread2.Join(500);
            //thread2.Join();

            Assert.IsTrue(thread2Finished);
            Assert.IsNull(result1);
            Assert.IsNotNull(result2);
        }
    }
}
