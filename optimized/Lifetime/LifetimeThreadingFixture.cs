using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;

namespace Unity.Specification.Lifetime
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void SameInstanceFromMultipleThreads()
        {
            Container.RegisterFactory<IService>((c, t, n) => new Service());

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
            Assert.IsNotNull(result2);
            Assert.AreNotSame(result1, result2);
        }

        [TestMethod]
        public void SameInstanceFromMultipleThreadsGeneric()
        {
            Container.RegisterFactory<IService>((c, t, n) => new Service())
                     .RegisterType(typeof(IFoo<>), typeof(Foo<>));

            object result1 = null;
            object result2 = null;

            Thread thread1 = new Thread(delegate ()
            {
                result1 = Container.Resolve<IFoo<IService>>();
            });

            Thread thread2 = new Thread(delegate ()
            {
                result2 = Container.Resolve<IFoo<IService>>();
            });

            thread1.Name = "1";
            thread2.Name = "2";

            thread1.Start();
            thread2.Start();

            thread2.Join();
            thread1.Join();

            Assert.IsNotNull(result1);
            Assert.IsNotNull(result2);
            Assert.AreNotSame(result1, result2);
        }

        [TestMethod]
        public void SameInstanceFromMultipleThreadsPerContainer()
        {
            Container.RegisterFactory<IService>((c, t, n) => new Service(), FactoryLifetime.PerContainer);

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
        public void SameInstanceFromMultipleThreadsPerContainerGeneric()
        {
            Container.RegisterFactory<IService>((c, t, n) => new Service(), FactoryLifetime.PerContainer)
                     .RegisterType(typeof(IFoo<>), typeof(Foo<>), TypeLifetime.PerContainer);

            object result1 = null;
            object result2 = null;

            Thread thread1 = new Thread(delegate ()
            {
                result1 = Container.Resolve<IFoo<IService>>();
            });

            Thread thread2 = new Thread(delegate ()
            {
                result2 = Container.Resolve<IFoo<IService>>();
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
        public void ContainerControlledLifetimeDoesNotLeaveHangingLockIfBuildThrowsException()
        {
            int count = 0;
            bool fail = false;
            Func<IUnityContainer, Type, string, object> factory = (c, t, n) =>
            {
                fail = !fail;
                Interlocked.Increment(ref count);
                return !fail ? new Service() : throw new InvalidOperationException();
            };
            Container.RegisterFactory<IService>(factory, FactoryLifetime.PerContainer);

            object result1 = null;
            object result2 = null;
            bool thread2Finished = false;

            Thread thread1 = new Thread(
                delegate()
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
                delegate()
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
