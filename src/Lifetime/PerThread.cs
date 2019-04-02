using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading;

namespace Unity.Specification.Lifetime
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void PerThread_Type_SameThread()
        {
            Container.RegisterType<IService, Service>(TypeLifetime.PerThread);

            var a = Container.Resolve<IService>();
            var b = Container.Resolve<IService>();

            Assert.AreSame(a, b);
        }

        [TestMethod]
        public void PerThread_Type_DifferentThreads()
        {
            Container.RegisterType<IService, Service>(TypeLifetime.PerThread);

            Thread t1 = new Thread(new ParameterizedThreadStart(ThreadProcedure));
            Thread t2 = new Thread(new ParameterizedThreadStart(ThreadProcedure));

            ThreadInformation info = new ThreadInformation(Container);

            t1.Start(info);
            t2.Start(info);
            t1.Join();
            t2.Join();

            var a = new List<IService>(info.ThreadResults.Values)[0];
            var b = new List<IService>(info.ThreadResults.Values)[1];

            Assert.AreNotSame(a, b);
        }

        [TestMethod]
        public void PerThread_Factory_SameThread()
        {
            Container.RegisterFactory<IService>((c, t, n) => new Service(), FactoryLifetime.PerThread);

            var a = Container.Resolve<IService>();
            var b = Container.Resolve<IService>();

            Assert.AreSame(a, b);
        }

        [TestMethod]
        public void PerThread_Factory_DifferentThreads()
        {
            Container.RegisterFactory<IService>((c, t, n) => new Service(), FactoryLifetime.PerThread);

            Thread t1 = new Thread(new ParameterizedThreadStart(ThreadProcedure));
            Thread t2 = new Thread(new ParameterizedThreadStart(ThreadProcedure));

            ThreadInformation info = new ThreadInformation(Container);

            t1.Start(info);
            t2.Start(info);
            t1.Join();
            t2.Join();

            var a = new List<IService>(info.ThreadResults.Values)[0];
            var b = new List<IService>(info.ThreadResults.Values)[1];

            Assert.AreNotSame(a, b);
        }
    }
}
