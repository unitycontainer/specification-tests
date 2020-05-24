using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unity.Specification.Registration.Syntax
{
    public abstract partial class SpecificationTests : TestFixtureBase
    {
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
        }
    }

    #region Test Data

    public interface IService { }

    public class Service : IService
    {
        string _id = Guid.NewGuid().ToString();
    }

    #endregion
}
