using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Unity.Lifetime;

namespace Unity.Specification.Lifetime
{
    public abstract partial class SpecificationTests
    {
        /// <summary>
        /// Registering var type twice with SetSingleton method. once with default and second with name.
        /// </summary>
        [TestMethod]
        public void CheckSetSingletonDoneTwice()
        {
            Container.RegisterType<Service>(TypeLifetime.PerContainer)
                     .RegisterType<Service>("hello", TypeLifetime.PerContainer);

            var obj = Container.Resolve<Service>();
            var obj1 = Container.Resolve<Service>("hello");
            
            Assert.AreNotSame(obj, obj1);
        }

        [TestMethod]
        public void CheckSingletonWithDependencies()
        {
            Container.RegisterType<ObjectWithOneDependency>(TypeLifetime.PerContainer);

            var result1 = Container.Resolve<ObjectWithOneDependency>();
            var result2 = Container.Resolve<ObjectWithOneDependency>();

            Assert.IsNotNull(result1);
            Assert.IsNotNull(result2);
            Assert.IsNotNull(result1.InnerObject);
            Assert.IsNotNull(result2.InnerObject);
            Assert.AreSame(result1, result2);
        }

        [TestMethod]
        public void CheckSingletonAsDependencies()
        {
            Container.RegisterType<ObjectWithOneDependency>(TypeLifetime.PerContainer);

            var result1 = Container.Resolve<ObjectWithTwoConstructorDependencies>();
            var result2 = Container.Resolve<ObjectWithTwoConstructorDependencies>();

            Assert.IsNotNull(result1);
            Assert.IsNotNull(result2);
            Assert.IsNotNull(result1.OneDep);
            Assert.IsNotNull(result2.OneDep);
            Assert.AreNotSame(result1, result2);
            Assert.AreSame(result1.OneDep, result2.OneDep);
        }

        /// <summary>
        /// Registering var type twice with SetSingleton method. once with default and second with name.
        /// </summary>
        [TestMethod]
        public void CheckRegisterInstanceDoneTwice()
        {
            var aInstance = new Service();
            Container.RegisterInstance<Service>(aInstance)
                     .RegisterInstance<Service>("hello", aInstance);

            var obj = Container.Resolve<Service>();
            var obj1 = Container.Resolve<Service>("hello");
            
            Assert.AreSame(obj, aInstance);
            Assert.AreSame(obj1, aInstance);
            Assert.AreSame(obj, obj1);
        }

        /// <summary>
        /// Registering var type as singleton and handling its lifetime. Using SetLifetime method.
        /// </summary>
        [TestMethod]
        public void SetLifetimeTwiceWithLifetimeHandle()
        {
            Container.RegisterType<Service>(TypeLifetime.PerContainer)
              .RegisterType<Service>("hello", new HierarchicalLifetimeManager());
            var obj = Container.Resolve<Service>();
            var obj1 = Container.Resolve<Service>("hello");
            
            Assert.AreNotSame(obj, obj1);
        }

        /// <summary>
        /// SetSingleton class A. Then register instance of class var twice. once by default second by name.
        /// </summary>
        [TestMethod]
        public void SetSingletonRegisterInstanceTwice()
        {
            var aInstance = new Service();
            Container.RegisterInstance<Service>(aInstance).RegisterInstance<Service>("hello", aInstance);
            var obj = Container.Resolve<Service>();
            var obj1 = Container.Resolve<Service>("hello");
            
            Assert.AreSame(obj, obj1);
        }

        /// <summary>
        /// SetLifetime class A. Then use GetOrDefault method to get the instances, once without name, second with name.
        /// </summary>
        [TestMethod]
        public void SetLifetimeGetTwice()
        {
            Container.RegisterType<Service>(TypeLifetime.PerContainer);
            var obj = Container.Resolve<Service>();
            var obj1 = Container.Resolve<Service>("hello");
         
            Assert.AreNotSame(obj, obj1);
        }

        /// <summary>
        /// SetSingleton class A. Then register instance of class var twice. once by default second by name. 
        /// Then SetLifetime once default and then by name.
        /// </summary>
        [TestMethod]
        public void SetSingletonRegisterInstanceTwiceSetLifetimeTwice()
        {
            var aInstance = new Service();

            Container.RegisterInstance(aInstance);
            Container.RegisterInstance("hello", aInstance);
            Container.RegisterType<Service>(TypeLifetime.PerContainer);
            Container.RegisterType<Service>("hello1", TypeLifetime.PerContainer);

            var obj = Container.Resolve<Service>();
            var obj1 = Container.Resolve<Service>("hello1");
            
            Assert.AreNotSame(obj, obj1);
        }

        /// <summary>
        /// SetSingleton class A. Then register instance of class var once by default second by name and
        /// again register instance by another name with lifetime control as false.
        /// Then SetLifetime once default and then by name.
        /// </summary>
        [TestMethod]
        public void SetSingletonNoNameRegisterInstanceDiffNames()
        {
            var aInstance = new Service();
            Container.RegisterInstance<Service>(aInstance)
                .RegisterInstance<Service>("hello", aInstance)
                .RegisterInstance<Service>("hi", aInstance, new ExternallyControlledLifetimeManager());

            var obj = Container.Resolve<Service>();
            var obj1 = Container.Resolve<Service>("hello");
            var obj2 = Container.Resolve<Service>("hi");

            Assert.AreSame(obj, obj1);
            Assert.AreSame(obj1, obj2);
        }

        /// <summary>
        /// SetLifetime class A. Then register instance of class var once by default second by name and
        /// again register instance by another name with lifetime control as false.
        /// Then SetLifetime once default and then by name.
        /// </summary>
        [TestMethod]
        public void SetLifetimeNoNameRegisterInstanceDiffNames()
        {
            var aInstance = new Service();
            Container.RegisterType<Service>(TypeLifetime.PerContainer)
                .RegisterInstance<Service>(aInstance)
                .RegisterInstance<Service>("hello", aInstance)
                .RegisterInstance<Service>("hi", aInstance, new ExternallyControlledLifetimeManager());

            var obj = Container.Resolve<Service>();
            var obj1 = Container.Resolve<Service>("hello");
            var obj2 = Container.Resolve<Service>("hi");
            
            Assert.AreSame(obj, obj1);
            Assert.AreSame(obj1, obj2);
        }

        /// <summary>
        /// SetSingleton class var with name. Then register instance of class var once by default second by name and
        /// again register instance by another name with lifetime control as false.
        /// Then SetLifetime once default and then by name.
        /// </summary>
        [TestMethod]
        public void SetSingletonWithNameRegisterInstanceDiffNames()
        {
            var aInstance = new Service();
            Container.RegisterType<Service>("set", TypeLifetime.PerContainer)
                .RegisterInstance<Service>(aInstance)
                .RegisterInstance<Service>("hello", aInstance)
                .RegisterInstance<Service>("hi", aInstance, new ExternallyControlledLifetimeManager());

            var obj = Container.Resolve<Service>("set");
            var obj1 = Container.Resolve<Service>("hello");
            var obj2 = Container.Resolve<Service>("hi");
            
            Assert.AreNotSame(obj, obj1);
            Assert.AreSame(obj1, obj2);
            Assert.AreSame(aInstance, obj1);
        }

        /// <summary>
        /// SetLifetime class var with name. Then register instance of class var once by default second by name and
        /// again register instance by another name with lifetime control as false.
        /// Then SetLifetime once default and then by name.
        /// </summary>
        [TestMethod]
        public void SetLifetimeWithNameRegisterInstanceDiffNames()
        {
            var aInstance = new Service();
            Container.RegisterType<Service>("set", TypeLifetime.PerContainer)
                .RegisterInstance<Service>(aInstance)
                .RegisterInstance<Service>("hello", aInstance)
                .RegisterInstance<Service>("hi", aInstance, new ExternallyControlledLifetimeManager());

            var obj = Container.Resolve<Service>("set");
            var obj1 = Container.Resolve<Service>("hello");
            var obj2 = Container.Resolve<Service>("hi");
            
            Assert.AreNotSame(obj, obj1);
            Assert.AreSame(aInstance, obj1);
            Assert.AreSame(obj1, obj2);
        }

        /// <summary>
        /// SetSingleton class A. Then register instance of class var once by default second by name and
        /// lifetime as true. Then again register instance by another name with lifetime control as true
        /// then register.
        /// Then SetLifetime once default and then by name.
        /// </summary>
        [TestMethod]
        public void SetSingletonClassARegisterInstanceOfAandBWithSameName()
        {
            var aInstance = new Service();
            var bInstance = new OtherService();
            Container.RegisterType<Service>(TypeLifetime.PerContainer)
                .RegisterInstance<Service>(aInstance)
                .RegisterInstance<Service>("hello", aInstance)
                .RegisterInstance<OtherService>("hi", bInstance)
                .RegisterInstance<OtherService>("hello", bInstance, new ExternallyControlledLifetimeManager());

            var obj = Container.Resolve<Service>();
            var obj1 = Container.Resolve<Service>("hello");
            var obj2 = Container.Resolve<OtherService>("hello");
            var obj3 = Container.Resolve<OtherService>("hi");
            
            Assert.AreSame(obj, obj1);
            Assert.AreNotSame(obj, obj2);
            Assert.AreNotSame(obj1, obj2);
            Assert.AreSame(obj2, obj3);
        }

        /// <summary>defect
        /// SetSingleton class var with name. then register instance of var twice. Once by name, second by default.       
        /// </summary>
        [TestMethod]
        public void SetSingletonByNameRegisterInstanceOnit()
        {
            var aInstance = new Service();
            Container.RegisterType<Service>("SetA", TypeLifetime.PerContainer)
                .RegisterInstance<Service>(aInstance)
                .RegisterInstance<Service>("hello", aInstance);

            var obj = Container.Resolve<Service>("SetA");
            var obj1 = Container.Resolve<Service>();
            var obj2 = Container.Resolve<Service>("hello");
            
            Assert.AreSame(obj1, obj2);
            Assert.AreNotSame(obj, obj2);
        }

        /// <summary>
        /// Use SetLifetime twice, once with parameter, and without parameter
        /// </summary>
        [TestMethod]
        public void TestSetLifetime()
        {
            Container.RegisterType<Service>(TypeLifetime.PerContainer)
               .RegisterType<Service>("hello", TypeLifetime.PerContainer);

            var obj = Container.Resolve<Service>();
            var obj1 = Container.Resolve<Service>("hello");
            
            Assert.AreNotSame(obj, obj1);
        }

        /// <summary>
        /// Register class var as singleton then use RegisterInstance to register instance 
        /// of class A.
        /// </summary>
        [TestMethod]
        public void SetSingletonDefaultNameRegisterInstance()
        {
            var aInstance = new Service();

            Container.RegisterType((Type)null, typeof(Service), null, TypeLifetime.PerContainer, null);
            Container.RegisterType((Type)null, typeof(Service), "SetA", TypeLifetime.PerContainer, null);
            Container.RegisterInstance(aInstance);
            Container.RegisterInstance("hello", aInstance);
            Container.RegisterInstance("hello", aInstance, new ExternallyControlledLifetimeManager());

            var obj =  Container.Resolve<Service>();
            var obj1 = Container.Resolve<Service>("SetA");
            var obj2 = Container.Resolve<Service>("hello");

            Assert.AreNotSame(obj, obj1);
            Assert.AreSame(obj, obj2);
        }

        /// <summary>
        /// Registering var type in both parent as well as child. Now trying to Resolve from both
        /// check if same or diff instances are returned.
        /// </summary>
        [TestMethod]
        public void RegisterWithParentAndChild()
        {
            //register type UnityTestClass
            var child = Container.CreateChildContainer();

            Container.RegisterType<Service>(TypeLifetime.PerContainer);
                child.RegisterType<Service>(TypeLifetime.PerContainer);

            var mytestparent = Container.Resolve<Service>();
            var mytestchild = child.Resolve<Service>();

            Assert.AreNotSame(mytestparent, mytestchild);
        }

        /// <summary>
        /// Verify WithLifetime managers. When registered using container controlled and freed, even then
        /// same instance is returned when asked for Resolve.
        /// </summary>
        [TestMethod]
        public void UseContainerControlledLifetime()
        {
            Container.RegisterType<Service>(TypeLifetime.PerContainer);

            var parentinstance = Container.Resolve<Service>();
            var hash = parentinstance.GetHashCode();
            parentinstance = null;
            GC.Collect();

            var parentinstance1 = Container.Resolve<Service>();
            Assert.AreEqual(hash, parentinstance1.GetHashCode());
        }

        [TestMethod]
        public void TestStringEmpty()
        {
            Container.RegisterType<Service>(null, TypeLifetime.PerContainer);
            Container.RegisterType<Service>(string.Empty, TypeLifetime.PerContainer);

            Service a = Container.Resolve<Service>();
            Service b = Container.Resolve<Service>(string.Empty);
            Service c = Container.Resolve<Service>((string)null);

            Assert.AreNotEqual(a, b);
            Assert.AreNotEqual(b, c);
            Assert.AreEqual(a, c);
        }
    }
}