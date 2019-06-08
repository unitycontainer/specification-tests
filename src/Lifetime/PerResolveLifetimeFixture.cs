using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace Unity.Specification.Lifetime
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void PerResolveCanBeConfigured()
        {
            Container.RegisterType<IPresenter, MockPresenter>()
                     .RegisterType<IView, View>(TypeLifetime.PerResolve);
        }

        [TestMethod]
        public void PerResolveViewIsReusedAcrossGraph()
        {
            Container.RegisterType<IPresenter, MockPresenter>()
                     .RegisterType<IView, View>(TypeLifetime.PerResolve);

            var view = Container.Resolve<IView>();

            var realPresenter = (MockPresenter)view.Presenter;
            Assert.AreSame(view, realPresenter.View);
        }

        [TestMethod]
        public void PerResolveViewsAreDifferentInDifferentResolveCalls()
        {
            Container.RegisterType<IPresenter, MockPresenter>()
                     .RegisterType<IView, View>(TypeLifetime.PerResolve);

            var view1 = Container.Resolve<IView>();
            var view2 = Container.Resolve<IView>();

            Assert.AreNotSame(view1, view2);
        }

        [TestMethod]
        public void PerResolveLifetimeIsHonoredWhenUsingFactory()
        {
            Container.RegisterFactory<SomeService>(c => new SomeService(), FactoryLifetime.PerResolve);

            var rootService = Container.Resolve<AService>();
            Assert.AreSame(rootService.SomeService, rootService.OtherService.SomeService);
        }

        [TestMethod]
        public void PerResolveFromMultipleThreads()
        {
            Container.RegisterType<IPresenter, MockPresenter>()
                     .RegisterType<IView, View>(TypeLifetime.PerResolve);

            object result1 = null;
            object result2 = null;

            Thread thread1 = new Thread(delegate ()
            {
                result1 = Container.Resolve<IView>();
            });

            Thread thread2 = new Thread(delegate ()
            {
                result2 = Container.Resolve<IView>();
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
    }
}
