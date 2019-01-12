using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unity.Specification.Parameter.Resolved
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void ResolveCorrespondingType()
        {
            // Setup
            Container
                .RegisterType(typeof(ICommand<>), typeof(ConcreteCommand<>),
                    Invoke.Method("Execute", Resolve.Parameter()));

            // Act
            var result = Container.Resolve<ICommand<Account>>();

            // Verify
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Executed, typeof(Account));
        }

        [TestMethod]
        public void ResolveCorrespondingTypeNamed()
        {
            // Setup
            Container
                .RegisterType(typeof(ICommand<>), typeof(ConcreteCommand<>),
                    Invoke.Method("Execute", Resolve.Parameter("1")));

            // Act
            var result = Container.Resolve<ICommand<string>>();

            // Verify
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Executed, Container.Resolve<string>("1"));
        }


        [TestMethod]
        public void NamedImplicitOpenGeneric()
        {
            // Setup
            Container
                .RegisterType(typeof(ICommand<>), typeof(ConcreteCommand<>), "inner")
                .RegisterType(typeof(ICommand<>), typeof(ConcreteCommand<>),
                    Invoke.Method("ChainedExecute", Resolve.Parameter("inner")));

            // Act
            var result = Container.Resolve<ICommand<Account>>();

            // Verify
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Chained, typeof(ICommand<Account>));
        }

        [TestMethod]
        public void NamedExplicitOpenGeneric()
        {
            // Setup
            Container
                .RegisterType(typeof(ICommand<>), typeof(ConcreteCommand<>), "inner")
                .RegisterType(typeof(ICommand<>), typeof(ConcreteCommand<>),
                    Invoke.Method("ChainedExecute", Resolve.Parameter(typeof(ICommand<>), "inner")));

            // Act
            var result = Container.Resolve<ICommand<Account>>();

            // Verify
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Chained, typeof(ICommand<Account>));
        }

        [TestMethod]
        public void GenericInjectionIsCalled()
        {
            // Setup
            Container
                .RegisterType(typeof(ICommand<>), typeof(LoggingCommand<>),
                    Invoke.Constructor(Resolve.Parameter(typeof(ICommand<>), "concrete")),
                    Invoke.Method("ChainedExecute", Resolve.Parameter(typeof(ICommand<>), "inner")))
                .RegisterType(typeof(ICommand<>), typeof(ConcreteCommand<>), "concrete")
                .RegisterType(typeof(ICommand<>), typeof(ConcreteCommand<>), "inner");

            // Act
            ICommand<Account> result = Container.Resolve<ICommand<Account>>();
            LoggingCommand<Account> lc = (LoggingCommand<Account>)result;

            // Verify
            Assert.IsTrue(lc.ChainedExecuteWasCalled);
        }
    }
}
