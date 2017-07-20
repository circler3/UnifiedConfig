using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using System.Xml.XPath;

namespace UnifiedConfig
{
    internal class IniConfigManager : XmlConfig
    {

        public IniConfigManager(string filepath)
            : base(filepath, File.ReadAllLines(filepath).ToXml())
        {

        }

        public override void Save(string filepath = null)
        {
            File.WriteAllText(filepath ?? _sourceFilePath, _xDoc.ToIni());
        }

        public override string GetValue(params string[] keys)
        {
            return base.GetValue(this.AddRoot(keys));
        }

        public override bool SetValue(string value, params string[] keys)
        {
            return base.SetValue(value, this.AddRoot(keys));
        }

        public override string this[string xPath]
        {
            get => base[Decorate(xPath)];
            set => base[Decorate(xPath)] = value;
        }
        /// <summary>
        /// Ini file does not contains an root element. Hence a decorator is necessary.
        /// </summary>
        /// <param name="xPath"></param>
        /// <returns></returns>
        private string Decorate(string xPath)
        {
            if (xPath.StartsWith("/")) xPath = "/" + _xDoc.Root.Name.LocalName + xPath;
            return xPath;
        }
    }
}
