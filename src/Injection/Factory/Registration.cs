using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Unity.Specification.Injection.Factory
{
    [SuppressMessage("ReSharper", "ExpressionIsAlwaysNull")]
    public abstract partial class SpecificationTests
    {

        [TestMethod]
        public void Factory_IsNotNull()
        {
            Container.RegisterType<IService>(Execute.Factory((c, t, n) => new Service()));
            Assert.IsNotNull(Container.Resolve<IService>());
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShortSignatureThrowsOnNull()
        {
            Func<IUnityContainer, object> factoryFunc = null;
            Container.RegisterType<IService>(Execute.Factory(factoryFunc));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void LongSignatureThrowsOnNull()
        {
            Func<IUnityContainer, Type, string, object> factoryFunc = null;
            Container.RegisterType<IService>(Execute.Factory(factoryFunc));
        }
    }
}
