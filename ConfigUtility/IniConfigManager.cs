using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using System.Xml.XPath;

namespace ConfigUtilty
{
    internal class IniConfigManager : XmlConfig
    {

        public IniConfigManager(string filepath)
            :base(filepath, File.ReadAllLines(filepath).ToXml())
        {

        }

        public override void Save(string filepath = null)
        {
            File.WriteAllText(filepath ?? _sourceFilePath, _root.ToIni());
        }
    }
}
