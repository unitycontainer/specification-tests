using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading;

namespace Unity.Specification.Lifetime
{
    public abstract partial class SpecificationTests : TestFixtureBase
    {
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
        }


        private void ThreadProcedure(object o)
        {
            ThreadInformation info = o as ThreadInformation;

            info.SetThreadResult(Thread.CurrentThread, info.Container.Resolve<IService>());
        }

        public class ThreadInformation
        {
            private readonly object dictLock = new object();

            public ThreadInformation(IUnityContainer container)
            {
                Container = container;
                ThreadResults = new Dictionary<Thread, IService>();
            }

            public IUnityContainer Container { get; }

            public Dictionary<Thread, IService> ThreadResults { get; }

            public void SetThreadResult(Thread t, IService result)
            {
                lock (dictLock)
                {
                    ThreadResults.Add(t, result);
                }
            }
        }

    }

    #region Test Data

    public class Foo
    {
    }

    public interface IService { }

    public class Service : IService { }
    
    #endregion
}
