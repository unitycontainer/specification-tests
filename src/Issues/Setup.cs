﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;

namespace Unity.Specification.Issues
{

    public abstract partial class SpecificationTests : TestFixtureBase
    {
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
        }


        public interface ILogger
        {
        }

        public class MockLogger : ILogger
        {
        }

        public interface IService
        {
        }

        public interface IGenericService<T>
        {
        }

        public class Service : IService, IDisposable
        {
            public string ID { get; } = Guid.NewGuid().ToString();

            public static int Instances = 0;

            public Service()
            {
                Interlocked.Increment(ref Instances);
            }

            public bool Disposed = false;

            public void Dispose()
            {
                Disposed = true;
            }
        }

        public interface IOtherService
        {
        }

        public class OtherService : IService, IOtherService, IDisposable
        {
            [InjectionConstructor]
            public OtherService()
            {

            }

            public OtherService(IUnityContainer container)
            {

            }


            public bool Disposed = false;
            public void Dispose()
            {
                Disposed = true;
            }
        }

    }
}