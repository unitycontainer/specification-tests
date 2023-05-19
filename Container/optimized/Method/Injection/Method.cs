﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unity.Specification.Method.Injection
{
    public abstract partial class SpecificationTests 
    {
        [TestMethod]
        public void InjectedMethodIsCalled()
        {
            // Setup
            Container.RegisterType<LegalInjectionMethod>(
                Invoke.Method(nameof(LegalInjectionMethod.InjectMe)));

            // Act
            LegalInjectionMethod result = Container.Resolve<LegalInjectionMethod>();

            // Verify
            Assert.IsTrue(result.WasInjected);
        }

        // TODO: injection validation
        //[TestMethod]
        //[ExpectedException(typeof(InvalidOperationException))]
        //public void MethodWithOutParameter()
        //{
        //    // Act
        //    Container.RegisterType<OutParams>(Invoke.Method(nameof(OutParams.InjectMe), 12));
        //    var _ = Container.Resolve<OutParams>();
        //}

        //[TestMethod]
        //[ExpectedException(typeof(InvalidOperationException))]
        //public void MethodWithRefParameter()
        //{
        //    // Act
        //    Container.RegisterType<RefParams>(
        //            Invoke.Method(nameof(RefParams.InjectMe), 15));
        //    var _ = Container.Resolve<RefParams>();
        //}

        [TestMethod]
        public void InheritedMethod()
        {
            // Setup
            Container.RegisterType<InheritedClass>(
                Invoke.Method(nameof(InheritedClass.InjectMe)));

            // Act
            var result = Container.Resolve<InheritedClass>();

            // Verify
            Assert.IsTrue(result.WasInjected);
        }


        [TestMethod]
        public virtual void MethodPassingVoid()
        {
            // Setup
            Container.RegisterType(typeof(GuineaPig),
                Invoke.Method(nameof(GuineaPig.Inject1)));

            // Act
            GuineaPig pig = Container.Resolve<GuineaPig>();

            // Verify
            Assert.AreEqual("void", pig.StringValue);
        }

        [TestMethod]
        public virtual void ReturningVoid()
        {
            // Setup
            Container.RegisterType(typeof(GuineaPig),
                Invoke.Method(nameof(GuineaPig.Inject2), "Hello"));

            // Act
            GuineaPig pig = Container.Resolve<GuineaPig>();

            // Verify
            Assert.AreEqual("Hello", pig.StringValue);
        }

        [TestMethod]
        public virtual void ReturningInt()
        {
            // Setup
            Container.RegisterType(typeof(GuineaPig),
                Invoke.Method(nameof(GuineaPig.Inject3), 17));

            // Act
            GuineaPig pig = Container.Resolve<GuineaPig>();


            // Verify
            Assert.AreEqual(17, pig.IntValue);
        }

        [TestMethod]
        public virtual void MultipleMethods()
        {
            // Setup
            Container.RegisterType<GuineaPig>(
                    Invoke.Method(nameof(GuineaPig.Inject3), 37),
                    Invoke.Method(nameof(GuineaPig.Inject2), "Hi there"));

            // Act
            GuineaPig pig = Container.Resolve<GuineaPig>();


            // Verify
            Assert.AreEqual(37, pig.IntValue);
            Assert.AreEqual("Hi there", pig.StringValue);
        }

        [TestMethod]
        public virtual void StaticMethod()
        {
            // Act
            Container.Resolve<GuineaPig>();

            // Verify
            Assert.IsFalse(GuineaPig.StaticMethodWasCalled);
        }

        // TODO: injection validation
        //[TestMethod]
        //[ExpectedException(typeof(InvalidOperationException))]
        //public void InjectingStaticMethod()
        //{
        //    // Verify
        //    Container.RegisterType<GuineaPig>(
        //        Invoke.Method(nameof(GuineaPig.ShouldntBeCalled)));
        //}
    }
}
