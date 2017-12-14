using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Registration
{
    public abstract partial class SpecificationTests : TestFixtureBase
    {
        private IUnityContainer _container;

        [TestInitialize]
        public void Setup()
        {
            _container = GetContainer();
        }

    }
}
