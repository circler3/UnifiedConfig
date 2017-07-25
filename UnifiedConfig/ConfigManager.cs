using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

[assembly: CLSCompliant(true)]
namespace UnifiedConfig
{
    /// <summary>
    /// Initialize the config which represents a single file.
    /// </summary>
    public class ConfigManager : IConfig
    {
        private XmlConfig config;
        /// <summary>
        /// The config name of the config manager. 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Loads main configuration file. The filename extension must have be supported.
        /// </summary>
        /// <exception cref="InvalidOperationException">Throw when file type is not supported</exception>
        /// <param name="filePath">File path with the extension.</param>
        /// <param name="configName">the config name</param>
        public ConfigManager(string filePath, string configName)
            : this(filePath)
        {
            Name = configName;
        }
        /// <summary>
        /// Loads main configuration file. Default config name is the filename.
        /// </summary>
        /// <exception cref="InvalidOperationException">Throw when file type is not supported</exception>
        /// <param name="filePath">File path with the extension.</param>
        public ConfigManager(string filePath)
        {
            string label = filePath.ToLower();
            if (label.EndsWith("xml"))
            {
                config = new XmlConigManager(filePath);
            }
            else if (label.EndsWith("ini"))
            {
                config = new IniConfigManager(filePath);
            }
            else if (label.EndsWith("json"))
            {
                config = new JsonConfigManager(filePath);
            }
            else
            {
                //use reflection to enumerate the config classes.
                config = TypeInference(filePath);
            }
            if (config == null)
                throw new InvalidOperationException("Unexpected file type!");
            Name = System.IO.Path.GetFileNameWithoutExtension(filePath);
        }
        /// <summary>
        /// Get a config manager class with the provided configbase.
        /// </summary>
        /// <param name="xmlconfig"></param>
        public ConfigManager(XmlConfig xmlconfig)
        {
            config = xmlconfig;
        }

        private XmlConfig TypeInference(string filePath)
        {
            XmlConfig con = null;
            var asm = typeof(ConfigManager).GetTypeInfo().Assembly;
            foreach (var item in asm.DefinedTypes)
            {
                if (item.BaseType == typeof(XmlConfig))
                {
                    foreach (var cons in item.DeclaredConstructors)
                    {
                        try
                        {
                            con = cons.Invoke(new object[] { filePath }) as XmlConfig;
                            break;
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
            }
            return con;
        }

        /// <summary>
        /// <para>Get or set node value by XPath</para>   
        /// <para>e.g. "/config/general/interval" : Starting from root, find the node value of interval node inside general node in config node</para>
        /// <para>    "//config/tick[@type='origin']" : Starting from anywhere, find the value of the tick node which have a type attribute valued origin in config node</para>
        /// </summary>
        /// <param name="xPath"></param>
        /// <returns></returns>
        public string this[string xPath]
        {
            get
            {
                return config[xPath];
            }
            set
            {
                config[xPath] = value;
            }
        }
        /// <summary>
        /// Get the string value of the path
        /// <para>e.g. GetValue("config","master")</para>
        /// </summary>
        /// <param name="keys">path strings</param>
        /// <returns>string value result</returns>
        public string GetValue(params string[] keys)
        {
            return config.GetValue(keys);
        }
        /// <summary>
        /// Save the config into file
        /// </summary>
        /// <param name="filepath">full file path. The file path where the config is used as default.</param>
        public void Save(string filepath = null)
        {
            config.Save(filepath);
        }
        /// <summary>
        /// Set the value by path
        /// <para>e.g. SetValue("True","config","master")</para>
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="keys">path strings</param>
        /// <returns>result of the operation</returns>
        public bool SetValue(string value, params string[] keys)
        {
            return config.SetValue(value, keys);
        }
        /// <summary>
        /// Get elements of the xPath query 
        /// </summary>
        /// <param name="xPath">xPath</param>
        /// <returns>xmlconfigs which expose the same interface</returns>
        public IEnumerable<XmlConfig> Elements(string xPath)
        {
            return config.Elements(xPath);
        }
    }
}
