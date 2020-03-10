using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace Unity.Specification.Configuration
{
    public class ConfigurationFixture
    {
        #region Fields

        public const string SectionName = "unity";
        public const string SectionType = "UnityConfigurationSection";
        public const string SectionAssembly = "Unity.Configuration";

        #endregion


        #region Implementation

        public static ConfigurationSection LoadSection(string name, TestVariant variant)
        {
            var file = $"{name?.GetHashCode().ToString() ?? "null"}-{variant.Type.GetHashCode().ToString()}-{variant.Namespace?.GetHashCode().ToString() ?? "null"}.config";
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file);

            if (!File.Exists(path))
            {
                XDocument embedded = null;

                if (null != name)
                {
                    var currentAssembly = Assembly.GetExecutingAssembly();
                    var resourceNames = currentAssembly.GetManifestResourceNames();
                    var resource = resourceNames.FirstOrDefault(it => it.EndsWith(name));

                    if (null == resource)
                    {
                        throw new Exception($"Can not locate embedded resource '{name}'. \nAvailable choices are: \n{string.Join("\n", resourceNames)}");
                    }

                    using (Stream resourceStream = currentAssembly.GetManifestResourceStream(resource))
                    {
                        embedded = XDocument.Load(resourceStream);
                    }
                }

                var content = null == embedded ? new XElement[0] : embedded.Root.Descendants();

                XDocument doc = new XDocument(
                    new XElement("configuration",
                        new XElement("configSections",
                            new XElement("section",
                                new XAttribute("name", SectionName),
                                new XAttribute("type", $"{variant.Type}.{SectionType}, {SectionAssembly}"))),
                        null == variant.Namespace
                              ? new XElement(SectionName, content)
                              : new XElement(variant.Namespace + SectionName, content)))
                {
                    Declaration = new XDeclaration("1.0", "utf-8", "true")
                };

                doc.Save(path);
            }

            var fileMap = new ExeConfigurationFileMap
            {
                ExeConfigFilename = path
            };

            return ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None)
                                       .GetSection(SectionName);
        }


        public static IEnumerable<object[]> TestVariants()
        {
            yield return new object[] { new TestVariant( "Unity.Configuration",                     null ) };
            yield return new object[] { new TestVariant( "Microsoft.Practices.Unity.Configuration", null ) };
            yield return new object[] { new TestVariant( "Unity.Configuration",                     "http://schemas.microsoft.com/practices/2010/unity" ) };
            yield return new object[] { new TestVariant( "Microsoft.Practices.Unity.Configuration", "http://schemas.microsoft.com/practices/2010/unity" ) };
            yield return new object[] { new TestVariant( "Unity.Configuration",                     "http://unitycontainer.org/schemas/2020/unity" ) };
            yield return new object[] { new TestVariant( "Microsoft.Practices.Unity.Configuration", "http://unitycontainer.org/schemas/2020/unity") };
        }

        #endregion


    }

    public class TestVariant
    {
        public TestVariant(string type, string @namespace)
        {
            Type = type;
            Namespace = @namespace;
        }

        public string Type { get; }

        public XNamespace Namespace { get; }

        public override string ToString()
        {
            var @namespace = null == Namespace ? string.Empty : $"({Namespace})";
            return $"{Type}{@namespace}";
        }
    }
}
