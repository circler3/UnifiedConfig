using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConfigUtilty
{
    /// <summary>
    /// Initialize the config which represents a single file.
    /// </summary>
    public class ConfigManager : ConfigBase
    {
        private ConfigBase config;

        /// <summary>
        /// Loads main configuration file.
        /// </summary>
        /// <param name="filePath"></param>
        public ConfigManager(string filePath)
            : base(filePath)
        {
            if (filePath.ToUpper().EndsWith("XML"))
            {
                config = new XmlConigManager(filePath);
            }
            else if (filePath.ToUpper().EndsWith("INI"))
            {
                config = new IniConfigManager(filePath);
            }
            else
            {
                throw new Exception("Unexpected file type!");
            }
        }

        /// <summary>
        /// <para>Get or set node value by XPath</para>   
        /// <para>e.g. "/config/general/interval" : Starting from root, find the node value of interval node inside general node in config node</para>
        /// <para>    "/config/tick[@type='origin']" : Starting from root, find the value of the tick node which have a type attribute valued origin in config node</para>
        /// </summary>
        /// <param name="xPath"></param>
        /// <returns></returns>
        public override string this[string xPath]
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
        public override string GetValue(params string[] keys)
        {
            return config.GetValue(keys);
        }
        /// <summary>
        /// Save the config into file
        /// </summary>
        /// <param name="filepath">full file path. The file path where the config is used as default.</param>
        public override void Save(string filepath = null)
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
        public override bool SetValue(string value, params string[] keys)
        {
            return config.SetValue(value, keys);
        }
    }
}
