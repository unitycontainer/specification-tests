using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Resolution.Array
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void AsConstructorParameter()
        {
            // Setup
            ILogger o1 = new MockLogger();
            ILogger o2 = new SpecialLogger();

            Container.RegisterInstance("o1", o1)
                     .RegisterInstance("o2", o2);

            // Act
            var resolved = Container.Resolve<TypeWithArrayConstructorParameter>();

            // Verify
            Assert.IsNotNull(resolved.Loggers);
            Assert.AreEqual(2, resolved.Loggers.Length);
            Assert.AreSame(o1, resolved.Loggers[0]);
            Assert.AreSame(o2, resolved.Loggers[1]);
        }

        [TestMethod]
        public void AsConstructorParameterOnClosedGenericType()
        {
            // Setup
            ILogger o1 = new MockLogger();
            ILogger o2 = new SpecialLogger();

            Container.RegisterInstance("o1", o1)
                     .RegisterInstance("o2", o2);

            // Act
            var resolved = Container.Resolve<GenericTypeWithArrayConstructorParameter<ILogger>>();

            // Verify
            Assert.IsNotNull(resolved.Values);
            Assert.AreEqual(2, resolved.Values.Length);
            Assert.AreSame(o1, resolved.Values[0]);
            Assert.AreSame(o2, resolved.Values[1]);
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void AsArrayParameterWithRankOverOne()
        {
            // Act
            Container.Resolve<TypeWithArrayConstructorParameterOfRankTwo>();
        }
    }
}
