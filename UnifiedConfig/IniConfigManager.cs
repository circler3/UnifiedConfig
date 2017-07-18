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

        public override string this[string xPath]
        {
            get => base[Decorate(xPath)];
            set => base[Decorate(xPath)] = value;
        }

        private string Decorate(string xPath)
        {
            if (xPath.StartsWith("/")) xPath = "/" + base._xDoc.Root.Name.LocalName + xPath;
            return xPath;
        }
    }
}
