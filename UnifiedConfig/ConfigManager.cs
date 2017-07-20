using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnifiedConfig
{
    /// <summary>
    /// Initialize the config which represents a single file.
    /// </summary>
    public class ConfigManager
    {
        private ConfigBase config;

        /// <summary>
        /// Loads main configuration file. The filename extension must have be supported.
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
                throw new InvalidOperationException("Unexpected file type!");
            }
        }

        /// <summary>
        /// <para>Get or set node value by XPath</para>   
        /// </summary>
        /// <example>
        /// <para>e.g. "/config/general/interval" : Starting from root, find the node value of interval node inside general node in config node</para>
        /// <para>    "//config/tick[@type='origin']" : Starting from anywhere, find the value of the tick node which have a type attribute valued origin in config node</para>
        /// </example>
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
    }
}
