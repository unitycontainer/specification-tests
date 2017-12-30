using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Attributes;
using Unity.Builder;
using Unity.Builder.Strategy;
using Unity.Exceptions;
using Unity.Extension;
using Unity.Injection;
using Unity.Lifetime;
using Unity.Specification.TestData;
using Unity.Specification.Utility;

namespace Unity.Specification.Container
{
    public abstract partial class SpecificationTests : TestFixtureBase
    {
        [TestMethod]
        public void CanCreateObjectFromUnconfiguredContainer()
        {
            IUnityContainer container = GetContainer();

            object o = container.Resolve<object>();

            Assert.IsNotNull(o);
        }

        [TestMethod]
        public void ContainerResolvesRecursiveConstructorDependencies()
        {
            IUnityContainer container = GetContainer();
            ObjectWithOneDependency dep = container.Resolve<ObjectWithOneDependency>();

            Assert.IsNotNull(dep);
            Assert.IsNotNull(dep.InnerObject);
            Assert.AreNotSame(dep, dep.InnerObject);
        }

        [TestMethod]
        public void ContainerResolvesMultipleRecursiveConstructorDependencies()
        {
            IUnityContainer container = GetContainer();
            ObjectWithTwoConstructorDependencies dep = container.Resolve<ObjectWithTwoConstructorDependencies>();

            dep.Validate();
        }

        [TestMethod]
        public void CanResolveTypeMapping()
        {
            IUnityContainer container = GetContainer()
                .RegisterType<IService, Service>();

            IService logger = container.Resolve<IService>();

            Assert.IsNotNull(logger);
            AssertExtensions.IsInstanceOfType(logger, typeof(Service));
        }

        [TestMethod]
        public void CanRegisterTypeMappingsWithNames()
        {
            IUnityContainer container = GetContainer()
                .RegisterType<IService, Service>()
                .RegisterType<IService, OtherService>("special");

            IService defaultLogger = container.Resolve<IService>();
            IService OtherService = container.Resolve<IService>("special");

            Assert.IsNotNull(defaultLogger);
            Assert.IsNotNull(OtherService);

            AssertExtensions.IsInstanceOfType(defaultLogger, typeof(Service));
            AssertExtensions.IsInstanceOfType(OtherService, typeof(OtherService));
        }

        [TestMethod]
        public void ShouldDoPropertyInjection()
        {
            IUnityContainer container = GetContainer();

            ObjectWithTwoProperties obj = container.Resolve<ObjectWithTwoProperties>();

            obj.Validate();
        }

        [TestMethod]
        public void ShouldSkipIndexers()
        {
            IUnityContainer container = GetContainer();

            ObjectWithIndexer obj = container.Resolve<ObjectWithIndexer>();

            obj.Validate();
        }

        [TestMethod]
        public void ShouldSkipStaticProperties()
        {
            IUnityContainer container = GetContainer();
            container.RegisterInstance<object>(this);

            var obj = container.Resolve<ObjectWithStaticAndInstanceProperties>();

            obj.Validate();
        }

        [TestMethod]
        public void ShouldDoAllInjections()
        {
            IUnityContainer container = GetContainer()
                .RegisterType<ILogger, MockLogger>();

            ObjectWithLotsOfDependencies obj = container.Resolve<ObjectWithLotsOfDependencies>();

            Assert.IsNotNull(obj);
            obj.Validate();
        }

        [TestMethod]
        public void CanGetObjectsUsingNongenericMethod()
        {
            IUnityContainer container = GetContainer()
                .RegisterType(typeof(IService), typeof(Service));

            object logger = container.Resolve(typeof(IService));

            Assert.IsNotNull(logger);
            AssertExtensions.IsInstanceOfType(logger, typeof(Service));
        }

        [TestMethod]
        public void CanGetNamedObjectsUsingNongenericMethod()
        {
            IUnityContainer container = GetContainer()
                .RegisterType(typeof(IService), typeof(Service))
                .RegisterType(typeof(IService), typeof(OtherService), "special");

            IService defaultLogger = container.Resolve(typeof(IService)) as IService;
            IService OtherService = container.Resolve(typeof(IService), "special") as IService;

            Assert.IsNotNull(defaultLogger);
            Assert.IsNotNull(OtherService);

            AssertExtensions.IsInstanceOfType(defaultLogger, typeof(Service));
            AssertExtensions.IsInstanceOfType(OtherService, typeof(OtherService));
        }

        [TestMethod]
        public void AllInjectionsWorkFromNongenericMethods()
        {
            IUnityContainer container = GetContainer()
                .RegisterType(typeof(ILogger), typeof(MockLogger));

            ObjectWithLotsOfDependencies obj = (ObjectWithLotsOfDependencies)container.Resolve(typeof(ObjectWithLotsOfDependencies));
            obj.Validate();
        }

        [TestMethod]
        public void ContainerSupportsSingletons()
        {
            IUnityContainer container = GetContainer()
                .RegisterType<IService, Service>(new ContainerControlledLifetimeManager());

            IService logger1 = container.Resolve<IService>();
            IService logger2 = container.Resolve<IService>();

            AssertExtensions.IsInstanceOfType(logger1, typeof(Service));
            Assert.AreSame(logger1, logger2);
        }

        [TestMethod]
        public void CanCreatedNamedSingletons()
        {
            IUnityContainer container = GetContainer()
                .RegisterType<IService, Service>()
                .RegisterType<IService, OtherService>("special", new ContainerControlledLifetimeManager());

            IService logger1 = container.Resolve<IService>();
            IService logger2 = container.Resolve<IService>();
            IService logger3 = container.Resolve<IService>("special");
            IService logger4 = container.Resolve<IService>("special");

            Assert.AreNotSame(logger1, logger2);
            Assert.AreSame(logger3, logger4);
        }

        [TestMethod]
        public void CanRegisterSingletonsWithNongenericMethods()
        {
            IUnityContainer container = GetContainer()
                .RegisterType<IService, Service>(new ContainerControlledLifetimeManager())
                .RegisterType<IService, OtherService>("special", new ContainerControlledLifetimeManager());

            IService logger1 = container.Resolve<IService>();
            IService logger2 = container.Resolve<IService>();
            IService logger3 = container.Resolve<IService>("special");
            IService logger4 = container.Resolve<IService>("special");

            Assert.AreSame(logger1, logger2);
            Assert.AreSame(logger3, logger4);
            Assert.AreNotSame(logger1, logger3);
        }

        [TestMethod]
        public void DisposingContainerDisposesSingletons()
        {
            IUnityContainer container = GetContainer()
                .RegisterType<DisposableObject>(new ContainerControlledLifetimeManager());

            DisposableObject dobj = container.Resolve<DisposableObject>();

            Assert.IsFalse(dobj.WasDisposed);
            container.Dispose();

            Assert.IsTrue(dobj.WasDisposed);
        }

        [TestMethod]
        public void SingletonsRegisteredAsDefaultGetInjected()
        {
            IUnityContainer container = GetContainer()
                .RegisterType<ObjectWithOneDependency>(new ContainerControlledLifetimeManager());

            ObjectWithOneDependency dep = container.Resolve<ObjectWithOneDependency>();
            ObjectWithTwoConstructorDependencies dep2 =
                container.Resolve<ObjectWithTwoConstructorDependencies>();

            Assert.AreSame(dep, dep2.OneDep);
        }

        [TestMethod]
        public void CanDoInjectionOnExistingObjects()
        {
            IUnityContainer container = GetContainer();

            ObjectWithTwoProperties o = new ObjectWithTwoProperties();
            Assert.IsNull(o.Obj1);
            Assert.IsNull(o.Obj2);

            container.BuildUp(o);

            o.Validate();
        }

        [TestMethod]
        public void CanBuildupObjectWithExplicitInterface()
        {
            IUnityContainer container = GetContainer()
                .RegisterType<ILogger, MockLogger>();

            ObjectWithExplicitInterface o = new ObjectWithExplicitInterface();
            container.BuildUp<ISomeCommonProperties>(o);

            o.ValidateInterface();
        }

        [TestMethod]
        public void CanBuildupObjectWithExplicitInterfaceUsingNongenericMethod()
        {
            IUnityContainer container = GetContainer()
                .RegisterType<ILogger, MockLogger>();

            ObjectWithExplicitInterface o = new ObjectWithExplicitInterface();
            container.BuildUp(typeof(ISomeCommonProperties), o);

            o.ValidateInterface();
        }

        [TestMethod]
        public void CanUseInstanceAsSingleton()
        {
            Service logger = new Service();

            IUnityContainer container = GetContainer()
                .RegisterInstance(typeof(IService), "logger", logger, new ContainerControlledLifetimeManager());

            IService o = container.Resolve<IService>("logger");
            Assert.AreSame(logger, o);
        }

        [TestMethod]
        public void CanUseInstanceAsSingletonViaGenericMethod()
        {
            Service logger = new Service();

            IUnityContainer container = GetContainer()
                .RegisterInstance<IService>("logger", logger);

            IService o = container.Resolve<IService>("logger");
            Assert.AreSame(logger, o);
        }

        [TestMethod]
        public void DisposingContainerDisposesOwnedInstances()
        {
            DisposableObject o = new DisposableObject();
            IUnityContainer container = GetContainer()
                .RegisterInstance(typeof(object), o);

            container.Dispose();
            Assert.IsTrue(o.WasDisposed);
        }

        [TestMethod]
        public void DisposingContainerDoesNotDisposeUnownedInstances()
        {
            DisposableObject o = new DisposableObject();
            IUnityContainer container = GetContainer()
                .RegisterInstance(typeof(object), o, new ExternallyControlledLifetimeManager());

            container.Dispose();
            Assert.IsFalse(o.WasDisposed);
            GC.KeepAlive(o);
        }

        [TestMethod]
        public void ContainerDefaultsToInstanceOwnership()
        {
            DisposableObject o = new DisposableObject();
            IUnityContainer container = GetContainer()
                .RegisterInstance(typeof(object), o);
            container.Dispose();
            Assert.IsTrue(o.WasDisposed);
        }

        [TestMethod]
        public void ContainerDefaultsToInstanceOwnershipViaGenericMethod()
        {
            DisposableObject o = new DisposableObject();
            IUnityContainer container = GetContainer()
                .RegisterInstance(typeof(DisposableObject), o);
            container.Dispose();
            Assert.IsTrue(o.WasDisposed);
        }

        [TestMethod]
        public void InstanceRegistrationWithoutNameRegistersDefault()
        {
            Service l = new Service();
            IUnityContainer container = GetContainer()
                .RegisterInstance(typeof(IService), l);

            IService o = container.Resolve<IService>();
            Assert.AreSame(l, o);
        }

        [TestMethod]
        public void InstanceRegistrationWithoutNameRegistersDefaultViaGenericMethod()
        {
            Service l = new Service();
            IUnityContainer container = GetContainer()
                .RegisterInstance<IService>(l);

            IService o = container.Resolve<IService>();
            Assert.AreSame(l, o);
        }

        [TestMethod]
        public void CanRegisterDefaultInstanceWithoutLifetime()
        {
            DisposableObject o = new DisposableObject();

            IUnityContainer container = GetContainer()
                .RegisterInstance(typeof(object), o, new ExternallyControlledLifetimeManager());

            object result = container.Resolve<object>();
            Assert.IsNotNull(result);
            Assert.AreSame(o, result);

            container.Dispose();
            Assert.IsFalse(o.WasDisposed);
            GC.KeepAlive(o);
        }

        [TestMethod]
        public void CanRegisterDefaultInstanceWithoutLifetimeViaGenericMethod()
        {
            DisposableObject o = new DisposableObject();

            IUnityContainer container = GetContainer()
                .RegisterInstance<object>(o, new ExternallyControlledLifetimeManager());

            object result = container.Resolve<object>();
            Assert.IsNotNull(result);
            Assert.AreSame(o, result);

            container.Dispose();
            Assert.IsFalse(o.WasDisposed);
            GC.KeepAlive(o);
        }

        [TestMethod]
        public void CanSpecifyInjectionConstructorWithDefaultDependencies()
        {
            string sampleString = "Hi there";
            IUnityContainer container = GetContainer()
                .RegisterInstance(sampleString);

            ObjectWithInjectionConstructor o = container.Resolve<ObjectWithInjectionConstructor>();

            Assert.IsNotNull(o.ConstructorDependency);
            Assert.AreSame(sampleString, o.ConstructorDependency);
        }

        [TestMethod]
        public void CanGetInstancesOfAllRegisteredTypes()
        {
            IUnityContainer container = GetContainer()
                .RegisterType<IService, Service>("mock")
                .RegisterType<IService, OtherService>("special")
                .RegisterType<IService, Service>("another");

            List<IService> loggers = new List<IService>(
                container.ResolveAll<IService>());

            Assert.AreEqual(3, loggers.Count);
            AssertExtensions.IsInstanceOfType(loggers[0], typeof(Service));
            AssertExtensions.IsInstanceOfType(loggers[1], typeof(OtherService));
            AssertExtensions.IsInstanceOfType(loggers[2], typeof(Service));
        }

        [TestMethod]
        public void GetAllDoesNotReturnTheDefault()
        {
            IUnityContainer container = GetContainer()
                .RegisterType<IService, OtherService>("special")
                .RegisterType<IService, Service>();

            List<IService> loggers = new List<IService>(
                container.ResolveAll<IService>());
            Assert.AreEqual(1, loggers.Count);
            AssertExtensions.IsInstanceOfType(loggers[0], typeof(OtherService));
        }

        [TestMethod]
        public void CanGetAllWithNonGenericMethod()
        {
            IUnityContainer container = GetContainer()
                .RegisterType<IService, Service>("mock")
                .RegisterType<IService, OtherService>("special")
                .RegisterType<IService, Service>("another");

            List<object> loggers = new List<object>(
                container.ResolveAll(typeof(IService)));

            Assert.AreEqual(3, loggers.Count);
            AssertExtensions.IsInstanceOfType(loggers[0], typeof(Service));
            AssertExtensions.IsInstanceOfType(loggers[1], typeof(OtherService));
            AssertExtensions.IsInstanceOfType(loggers[2], typeof(Service));
        }

        [TestMethod]
        public void GetAllReturnsRegisteredInstances()
        {
            Service l = new Service();

            IUnityContainer container = GetContainer()
                .RegisterType<IService, Service>("normal")
                .RegisterType<IService, OtherService>("special")
                .RegisterInstance<IService>("instance", l);

            List<IService> loggers = new List<IService>(
                container.ResolveAll<IService>());

            Assert.AreEqual(3, loggers.Count);
            AssertExtensions.IsInstanceOfType(loggers[0], typeof(Service));
            AssertExtensions.IsInstanceOfType(loggers[1], typeof(OtherService));
            Assert.AreSame(l, loggers[2]);
        }

        [TestMethod]
        public void CanRegisterLifetimeAsSingleton()
        {
            IUnityContainer container = GetContainer()
                .RegisterType<IService, Service>()
                .RegisterType<IService, OtherService>("special", new ContainerControlledLifetimeManager());

            IService logger1 = container.Resolve<IService>();
            IService logger2 = container.Resolve<IService>();
            IService logger3 = container.Resolve<IService>("special");
            IService logger4 = container.Resolve<IService>("special");

            Assert.AreNotSame(logger1, logger2);
            Assert.AreSame(logger3, logger4);
        }

        [TestMethod]
        public void ShouldThrowIfAttemptsToResolveUnregisteredInterface()
        {
            IUnityContainer container = GetContainer();

            AssertExtensions.AssertException<ResolutionFailedException>(() =>
                {
                    container.Resolve<IService>();
                });
        }

        [TestMethod]
        public void CanBuildSameTypeTwice()
        {
            IUnityContainer container = GetContainer();

            container.Resolve<ObjectWithTwoConstructorDependencies>();
            container.Resolve<ObjectWithTwoConstructorDependencies>();
        }

        [TestMethod]
        public void CanRegisterMultipleStringInstances()
        {
            IUnityContainer container = GetContainer();
            string first = "first";
            string second = "second";

            container.RegisterInstance(first)
                .RegisterInstance(second);

            string result = container.Resolve<string>();

            Assert.AreEqual(second, result);
        }

        [TestMethod]
        public void GetReasonableExceptionWhenRegisteringNullInstance()
        {
            IUnityContainer container = GetContainer();
            AssertExtensions.AssertException<ArgumentNullException>(() =>
                {
                    container.RegisterInstance<SomeType>(null);
                });
        }

        [TestMethod]
        public void CanRegisterGenericTypesAndResolveThem()
        {
            Dictionary<string, string> myDict = new Dictionary<string, string>();
            myDict.Add("One", "two");
            myDict.Add("Two", "three");

            IUnityContainer container = GetContainer()
                .RegisterInstance<IDictionary<string, string>>(myDict)
                .RegisterType(typeof(IDictionary<,>), typeof(Dictionary<,>));

            IDictionary<string, string> result = container.Resolve<IDictionary<string, string>>();
            Assert.AreSame(myDict, result);
        }

        [TestMethod]
        public void CanSpecializeGenericsViaTypeMappings()
        {
            IUnityContainer container = GetContainer()
                .RegisterType(typeof(IRepository<>), typeof(MockRespository<>))
                .RegisterType<IRepository<SomeType>, SomeTypRepository>();

            IRepository<string> generalResult = container.Resolve<IRepository<string>>();
            IRepository<SomeType> specializedResult = container.Resolve<IRepository<SomeType>>();

            AssertExtensions.IsInstanceOfType(generalResult, typeof(MockRespository<string>));
            AssertExtensions.IsInstanceOfType(specializedResult, typeof(SomeTypRepository));
        }

        [TestMethod]
        public void ContainerResolvesItself()
        {
            IUnityContainer container = GetContainer();

            Assert.AreSame(container, container.Resolve<IUnityContainer>());
        }

        [TestMethod]
        public void ContainerResolvesItselfEvenAfterGarbageCollect()
        {
            IUnityContainer container = GetContainer();
            container.AddNewExtension<GarbageCollectingExtension>();

            Assert.IsNotNull(container.Resolve<IUnityContainer>());
        }

        public class GarbageCollectingExtension : UnityContainerExtension
        {
            protected override void Initialize()
            {
                Context.Strategies.Add(new GarbageCollectingStrategy(), UnityBuildStage.Setup);
            }

            public class GarbageCollectingStrategy : BuilderStrategy
            {
                public override object PreBuildUp(IBuilderContext context)
                {
                    GC.Collect();
                    return null;
                }
            }
        }

        [TestMethod]
        public void ChildContainerResolvesChildNotParent()
        {
            IUnityContainer parent = GetContainer();
            IUnityContainer child = parent.CreateChildContainer();

            Assert.AreSame(child, child.Resolve<IUnityContainer>());
        }

        [TestMethod]
        public void ParentContainerResolvesParentNotChild()
        {
            IUnityContainer parent = GetContainer();
            IUnityContainer child = parent.CreateChildContainer();

            Assert.AreSame(parent, parent.Resolve<IUnityContainer>());
        }

        [TestMethod]
        public void ResolvingOpenGenericGivesInnerInvalidOperationException()
        {
            IUnityContainer container = GetContainer()
                .RegisterType(typeof(List<>), new InjectionConstructor(10));

            AssertExtensions.AssertException<ResolutionFailedException>(
                () => { container.Resolve(typeof(List<>)); },
                e => { AssertExtensions.IsInstanceOfType(e.InnerException, typeof(ArgumentException)); });
        }

        [TestMethod]
        public void ResovingObjectWithPrivateSetterGivesUsefulException()
        {
            IUnityContainer container = GetContainer();

            AssertExtensions.AssertException<ResolutionFailedException>(
                () => { container.Resolve<ObjectWithPrivateSetter>(); },
                e => { AssertExtensions.IsInstanceOfType(e.InnerException, typeof(InvalidOperationException)); });
        }

        [TestMethod]
        public void ResolvingUnconfiguredPrimitiveDependencyGivesReasonableException()
        {
            ResolvingUnconfiguredPrimitiveGivesResonableException<string>();
            ResolvingUnconfiguredPrimitiveGivesResonableException<bool>();
            ResolvingUnconfiguredPrimitiveGivesResonableException<char>();
            ResolvingUnconfiguredPrimitiveGivesResonableException<float>();
            ResolvingUnconfiguredPrimitiveGivesResonableException<double>();
            ResolvingUnconfiguredPrimitiveGivesResonableException<byte>();
            ResolvingUnconfiguredPrimitiveGivesResonableException<short>();
            ResolvingUnconfiguredPrimitiveGivesResonableException<int>();
            ResolvingUnconfiguredPrimitiveGivesResonableException<long>();
            ResolvingUnconfiguredPrimitiveGivesResonableException<IntPtr>();
            ResolvingUnconfiguredPrimitiveGivesResonableException<UIntPtr>();
            ResolvingUnconfiguredPrimitiveGivesResonableException<ushort>();
            ResolvingUnconfiguredPrimitiveGivesResonableException<uint>();
            ResolvingUnconfiguredPrimitiveGivesResonableException<ulong>();
            ResolvingUnconfiguredPrimitiveGivesResonableException<sbyte>();
        }

        private void ResolvingUnconfiguredPrimitiveGivesResonableException<T>()
        {
            IUnityContainer container = GetContainer();
            try
            {
                container.Resolve<TypeWithPrimitiveDependency<T>>();
            }
            catch (ResolutionFailedException e)
            {
                AssertExtensions.IsInstanceOfType(e.InnerException, typeof(InvalidOperationException));
                return;
            }
            Assert.Fail("Expected exception did not occur");
        }

        internal class SomeType
        {
        }

        public interface IRepository<TEntity>
        {
        }

        public class MockRespository<TEntity> : IRepository<TEntity>
        {
        }

        public class SomeTypRepository : IRepository<SomeType>
        {
        }

        public class ObjectWithPrivateSetter
        {
            [Dependency]
            public object Obj1 { get; private set; }
        }

        public class TypeWithPrimitiveDependency<T>
        {
            public TypeWithPrimitiveDependency(T dependency)
            {
            }
        }

    }
}
