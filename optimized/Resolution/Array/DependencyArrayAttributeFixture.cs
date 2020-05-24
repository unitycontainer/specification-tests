using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Resolution.Array
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void CanResolveArrayForConstructorParameter()
        {
            ILogger o1 = new MockLogger();
            ILogger o2 = new SpecialLogger();

            Container.RegisterInstance<ILogger>("o1", o1)
                     .RegisterInstance<ILogger>("o2", o2);

            var resolved = Container.Resolve<TypeWithArrayConstructorParameter>();

            Assert.IsNotNull(resolved.Loggers);
            Assert.AreEqual(2, resolved.Loggers.Length);
            Assert.AreSame(o1, resolved.Loggers[0]);
            Assert.AreSame(o2, resolved.Loggers[1]);
        }

        [TestMethod]
        public void CanResolveArrayForProperty()
        {
            ILogger o1 = new MockLogger();
            ILogger o2 = new SpecialLogger();

            Container.RegisterInstance<ILogger>("o1", o1)
                     .RegisterInstance<ILogger>("o2", o2);

            var resolved = Container.Resolve<TypeWithArrayProperty>();

            Assert.IsNotNull(resolved.Loggers);
            Assert.AreEqual(2, resolved.Loggers.Length);
            Assert.AreSame(o1, resolved.Loggers[0]);
            Assert.AreSame(o2, resolved.Loggers[1]);
        }

        [TestMethod]
        public void CanResolveArrayForConstructorParameterOnClosedGenericType()
        {
            ILogger o1 = new MockLogger();
            ILogger o2 = new SpecialLogger();

            Container.RegisterInstance<ILogger>("o1", o1)
                     .RegisterInstance<ILogger>("o2", o2);

            var resolved = Container.Resolve<GenericTypeWithArrayConstructorParameter<ILogger>>();

            Assert.IsNotNull(resolved.Values);
            Assert.AreEqual(2, resolved.Values.Length);
            Assert.AreSame(o1, resolved.Values[0]);
            Assert.AreSame(o2, resolved.Values[1]);
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void BindingDependencyArrayToArrayParameterWithRankOverOneThrows()
        {
            Container.Resolve<TypeWithArrayConstructorParameterOfRankTwo>();
        }
    }
}
