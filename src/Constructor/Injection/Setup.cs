using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unity.Specification.Constructor.Injection
{
    public abstract partial class SpecificationTests : TestFixtureBase
    {
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();

            Container.RegisterInstance(TypeWithMultipleCtors.Four);
            Container.RegisterInstance(TypeWithMultipleCtors.Five, TypeWithMultipleCtors.Five);
        }
    }
}
