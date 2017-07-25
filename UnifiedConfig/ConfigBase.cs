using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace UnifiedConfig
{
    /// <summary>
    /// Abstract base of config class
    /// </summary>
    public abstract class ConfigBase : IConfig
    {
        /// <summary>
        /// Constructor which set the orginal config filename.
        /// </summary>
        /// <param name="filePath"></param>
        public ConfigBase(string filePath)
        {
            sourceFilePath = filePath;
        }
        /// <summary>
        /// Original config source file
        /// </summary>
        protected string sourceFilePath;

        /// <summary>
        /// Get or set the value by findingPath.
        /// </summary>
        /// <param name="findingPath"></param>
        /// <returns></returns>
        public abstract string this[string findingPath] { get; set; }
        /// <summary>
        /// Get the string value of the path
        /// <para>e.g. GetValue("config","master")</para>
        /// </summary>
        /// <param name="keys">path strings</param>
        /// <returns>string value result</returns>
        public abstract string GetValue(params string[] keys);
        /// <summary>
        /// Set the value by path
        /// <para>e.g. SetValue("True","config","master")</para>
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="keys">path strings</param>
        /// <returns>result of the operation</returns>
        public abstract bool SetValue(string value, params string[] keys);
        /// <summary>
        /// Save the config into file
        /// </summary>
        /// <param name="filepath">full file path. The file path where the config is used as default.</param>
        public abstract void Save(string filepath = null);
    }
}
