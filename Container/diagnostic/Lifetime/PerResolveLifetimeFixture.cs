using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Diagnostic.Lifetime
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void ContainerCanBeConfiguredForPerBuildSingleton()
        {
            Container.RegisterType<IPresenter, MockPresenter>()
                     .RegisterType<IView, View>(TypeLifetime.PerResolve);
        }

        [TestMethod]
        public void ViewIsReusedAcrossGraph()
        {
            Container.RegisterType<IPresenter, MockPresenter>()
                     .RegisterType<IView, View>(TypeLifetime.PerResolve);

            var view = Container.Resolve<IView>();

            var realPresenter = (MockPresenter)view.Presenter;
            Assert.AreSame(view, realPresenter.View);
        }

        [TestMethod]
        public void ViewsAreDifferentInDifferentResolveCalls()
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
    }
}
