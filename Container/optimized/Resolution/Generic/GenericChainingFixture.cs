using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Injection;
using Unity.Lifetime;

namespace Unity.Specification.Resolution.Generic
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void CanSpecializeGenericTypes()
        {
            Container.RegisterType(typeof(ICommand<>), typeof(ConcreteCommand<>));
            ICommand<User> cmd = Container.Resolve<ICommand<User>>();
            Assert.IsInstanceOfType(cmd, typeof(ConcreteCommand<User>));
        }

        [TestMethod]
        public void ConfiguringConstructorThatTakesOpenGenericTypeDoesNotThrow()
        {
            Container.RegisterType(typeof(LoggingCommand<>),
                    new InjectionConstructor(new ResolvedParameter(typeof(ICommand<>), "concrete")));
        }

        [TestMethod]
        public void CanConfigureGenericMethodInjectionInContainer()
        {
            Container.RegisterType(typeof(ICommand<>), typeof(LoggingCommand<>),
                    new InjectionConstructor(new ResolvedParameter(typeof(ICommand<>), "concrete")),
                    new InjectionMethod("ChainedExecute", new ResolvedParameter(typeof(ICommand<>), "inner")))
                .RegisterType(typeof(ICommand<>), typeof(ConcreteCommand<>), "concrete")
                .RegisterType(typeof(ICommand<>), typeof(ConcreteCommand<>), "inner");
        }

        [TestMethod]
        public void CanConfigureInjectionForNonGenericMethodOnGenericClass()
        {
            Container.RegisterType(typeof(ICommand<>), typeof(LoggingCommand<>),
                new InjectionConstructor(),
                new InjectionMethod("InjectMe"));

            ICommand<Account> result = Container.Resolve<ICommand<Account>>();
            LoggingCommand<Account> logResult = (LoggingCommand<Account>)result;

            Assert.IsTrue(logResult.WasInjected);
        }

        [TestMethod]
        public void CanCallDefaultConstructorOnGeneric()
        {
            Container.RegisterType(typeof(ICommand<>), typeof(LoggingCommand<>),  new InjectionConstructor())
                     .RegisterType(typeof(ICommand<>), typeof(ConcreteCommand<>), "inner");

            ICommand<User> result = Container.Resolve<ICommand<User>>();

            Assert.IsInstanceOfType(result, typeof(LoggingCommand<User>));
        }

        [TestMethod]
        public void CanConfigureInjectionForGenericProperty()
        {
            Container.RegisterType(typeof(ICommand<>), typeof(LoggingCommand<>),
                    new InjectionConstructor(),
                    new InjectionProperty("Inner",
                        new ResolvedParameter(typeof(ICommand<>), "inner")))
                .RegisterType(typeof(ICommand<>), typeof(ConcreteCommand<>), "inner");
        }

        [TestMethod]
        public void CanInjectNonGenericPropertyOnGenericClass()
        {
            Container.RegisterType(typeof(ICommand<>), typeof(ConcreteCommand<>),
                    new InjectionProperty("NonGenericProperty"));

            ConcreteCommand<User> result = (ConcreteCommand<User>)(Container.Resolve<ICommand<User>>());
            Assert.IsNotNull(result.NonGenericProperty);
        }

        [TestMethod]
        public void ContainerControlledOpenGenericsAreDisposed()
        {
            Container.RegisterType(typeof(ICommand<>), typeof(DisposableCommand<>), new ContainerControlledLifetimeManager());

            var accountCommand = Container.Resolve<ICommand<Account>>();
            var userCommand = Container.Resolve<ICommand<User>>();

            Container.Dispose();

            Assert.IsTrue(((DisposableCommand<Account>)accountCommand).Disposed);
            Assert.IsTrue(((DisposableCommand<User>)userCommand).Disposed);
        }
    }
}

