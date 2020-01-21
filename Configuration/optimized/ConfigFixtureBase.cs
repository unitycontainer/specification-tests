using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using System.IO;

namespace Unity.Specification
{
    public abstract class ConfigFixtureBase : TestFixtureBase
    {
        protected System.Configuration.Configuration Configuration { get; private set; }

        [TestInitialize]
        public override void Setup()
        {
            base.Setup();

            var type = GetType();
            string assemblyPath = Path.GetDirectoryName(type.Assembly.Location);
            var configFileName = Path.Combine(assemblyPath, type.Namespace, "setup.config");

            var fileMap = new ExeConfigurationFileMap
            {
                ExeConfigFilename = configFileName
            };

            Configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
        }

        protected void LoadSection(string name = "unity")
        {
            var section = (Microsoft.Practices.Unity.Configuration.UnityConfigurationSection)Configuration.GetSection(name);
            section.Configure(Container);
        }
    }
}
