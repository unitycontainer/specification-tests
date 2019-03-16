using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Injection;
using Unity.Resolution;

namespace Unity.Specification.Diagnostic.Override
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        [Ignore]
        public void DependencyLegacy()
        {
            // arrange
            Container.RegisterType<IController, TheController>()
                     .RegisterType<IMessageProvider, DefaultMessageProvider>()
                     .RegisterType<IMessageProvider, AlternativeMessageProvider>(nameof(AlternativeMessageProvider));

            var resolvedParameter = new ResolvedParameter<IMessageProvider>(nameof(AlternativeMessageProvider));
            var dependencyOverride = new DependencyOverride<IMessageProvider>(resolvedParameter);

            // act
            var actual = Container.Resolve<IController>(dependencyOverride);

            // assert
            Assert.AreEqual(actual.GetMessage(), "Goodbye cruel world!");
        }
    }
}
