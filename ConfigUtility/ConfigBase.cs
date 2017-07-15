using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConfigUtilty
{
    public abstract class ConfigBase
    {
        public ConfigBase(string filePath)
        {
            _sourceFilePath = filePath;
        }
        protected string _sourceFilePath;

        public abstract string GetValue(params string[] keys);

        public abstract bool SetValue(string value, params string[] keys);

        public abstract void Save(string filepath = null);

        public abstract string this[string XPath] { get; set; }

    }
}
