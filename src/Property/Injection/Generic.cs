using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Property.Injection
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void GenericPropertyIsActuallyInjected()
        {
            // Setup
            Container
                .RegisterType(typeof(ICommand<>), typeof(LoggingCommand<>),
                    Invoke.Constructor(),
                    Inject.Property("Inner", Resolve.Parameter(typeof(ICommand<>), "inner")))
                .RegisterType(typeof(ICommand<>), typeof(ConcreteCommand<>), "inner");
            
            // Act
            ICommand<Account> result = Container.Resolve<ICommand<Account>>();

            // Verify
            LoggingCommand<Account> actualResult = (LoggingCommand<Account>)result;
            Assert.IsNotNull(actualResult.Inner);
            Assert.IsInstanceOfType(actualResult.Inner, typeof(ConcreteCommand<Account>));
        }

        [TestMethod]
        public void CanInjectNestedGenerics()
        {
            // Setup
            Container.RegisterType(typeof(ICommand<>), typeof(LoggingCommand<>),
                         Invoke.Constructor(Resolve.Parameter(typeof(ICommand<>), "concrete")))
                     .RegisterType(typeof(ICommand<>), typeof(ConcreteCommand<>), "concrete");

            // Act
            var cmd = Container.Resolve<ICommand<Customer?>>();
            var logCmd = (LoggingCommand<Customer?>)cmd;

            // Verify
            Assert.IsNotNull(logCmd.Inner);
            Assert.IsInstanceOfType(logCmd.Inner, typeof(ConcreteCommand<Customer?>));
        }


        [TestMethod]
        public void CanChainGenericTypes()
        {
            // Setup
            Container.RegisterType(typeof(ICommand<>), typeof(LoggingCommand<>), 
                        Invoke.Constructor(Resolve.Parameter(typeof(ICommand<>), "concrete")))
                     .RegisterType(typeof(ICommand<>), typeof(ConcreteCommand<>), "concrete");

            // Act
            var md = Container.Resolve<ICommand<User>>("concrete");
            ICommand<User> cmd = Container.Resolve<ICommand<User>>();
            LoggingCommand<User> logCmd = (LoggingCommand<User>)cmd;

            // Verify
            Assert.IsNotNull(logCmd.Inner);
            Assert.IsInstanceOfType(logCmd.Inner, typeof(ConcreteCommand<User>));
        }

        [TestMethod]
        public void CanChainGenericTypesViaRegisterTypeMethod()
        {
            // Setup
            Container
                .RegisterType(typeof(ICommand<>), typeof(LoggingCommand<>), 
                    Invoke.Constructor(Resolve.Parameter(typeof(ICommand<>), "concrete")))
                .RegisterType(typeof(ICommand<>), typeof(ConcreteCommand<>), "concrete");

            // Act
            ICommand<User> cmd = Container.Resolve<ICommand<User>>();
            LoggingCommand<User> logCmd = (LoggingCommand<User>)cmd;

            // Verify
            Assert.IsNotNull(logCmd.Inner);
            Assert.IsInstanceOfType(logCmd.Inner, typeof(ConcreteCommand<User>));
        }
    }
}
