using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Injection.Constructor
{
    public abstract partial class SpecificationTests
    {

        public static IEnumerable<object[]> RegistrationFailedTestData
        {

            // Format: Type typeFrom, Type typeTo, string name, Type typeToResolve, object[] parameters, Func<object, bool> validator
            get
            {
                //
                //yield return new object[]
                //{
                //    null,                                       //  Type typeFrom, 
                //    typeof(object),                             //  Type typeTo, 
                //    "",                                         //  string name, 
                //    new object[] {}                             //  object[] parameters, 
                //};

                // IncorrectType
                yield return new object[]
                {
                    null,                                       //  Type typeFrom, 
                    typeof(TypeWithAmbiguousCtors),             //  Type typeTo, 
                    "IncorrectType",                            //  string name, 
                    new object[] { typeof(int) }                //  object[] parameters, 
                };

                // IncorrectValue
                yield return new object[]
                {
                    null,                                       //  Type typeFrom, 
                    typeof(TypeWithAmbiguousCtors),             //  Type typeTo, 
                    "IncorrectValue",                           //  string name, 
                    new object[] { 0 }                          //  object[] parameters, 
                };

            }
        }



        [TestMethod]
        public void SelectByValueTypes()
        {
            Container.RegisterType<TypeWithAmbiguousCtors>(Execute.Constructor(Inject.Parameter(typeof(string)),
                Inject.Parameter(typeof(string)),
                Inject.Parameter(typeof(int))));
            Assert.AreEqual(TypeWithAmbiguousCtors.Three, Container.Resolve<TypeWithAmbiguousCtors>().Signature);
        }

    }
}
