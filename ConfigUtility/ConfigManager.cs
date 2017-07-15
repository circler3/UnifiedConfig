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
            :base(filePath)
        {
            if (filePath.ToUpper().EndsWith("XML"))
            {
                config = new XmlConigManager(filePath);
            }
            else if(filePath.ToUpper().EndsWith("INI"))
            {
                config = new IniConfigManager(filePath);
            }
            else
            {
                throw new Exception("Unexpected file type!");
            }
        }

        public override string this[string XPath] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override string GetValue(params string[] keys)
        {
            return config.GetValue(keys);
        }

        public override void Save(string filepath = null)
        {
            config.Save(filepath);
        }

        public override bool SetValue(string value, params string[] keys)
        {
            return config.SetValue(value, keys);
        }
    }
}
