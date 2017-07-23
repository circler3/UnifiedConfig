using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;

namespace UnifiedConfig
{
    public class XmlConfig : ConfigBase
    {
        internal XDocument xDoc;

        internal XmlConfig(string filepath, XDocument content)
            :base(filepath)
        {
            xDoc = content;
        }

        public override void Save(string filePath = null)
        {
            if (sourceFilePath == null) return;
            FileStream fs = new FileStream(filePath ?? sourceFilePath, FileMode.Create);
            xDoc.Save(fs, SaveOptions.None);
        }

        public override string GetValue(params string[] keys)
        {
            var element = LocateElementXPath(keys);
            if (element == null) return "";
            return element.Value;
        }

        public override bool SetValue(string value, params string[] keys)
        {
            var element = LocateElementXPath(keys);
            if (element == null) return false;
            element.Value = value;
            return true;
        }

        public override string this[string xPath]
        {
            get
            {
                var result = (xDoc.XPathEvaluate(xPath) as IEnumerable).Cast<XObject>().FirstOrDefault();
                switch( result.NodeType)
                {
                    case System.Xml.XmlNodeType.Attribute:
                        return ((XAttribute)result).Value.Trim();
                    case System.Xml.XmlNodeType.Element:
                        return ((XElement)result).Value.Trim();
                }
                return null;
            }
            set
            {
                var result = (xDoc.XPathEvaluate(xPath) as IEnumerable).Cast<XObject>().FirstOrDefault();
                switch (result.NodeType)
                {
                    case System.Xml.XmlNodeType.Attribute:
                        ((XAttribute)result).Value = value;
                        break;
                    case System.Xml.XmlNodeType.Element:
                        ((XElement)result).Value = value;
                        break;
                }
            }
        }

        public IEnumerable<XmlConfig> Elements(string xPath)
        {
            foreach(var n in xDoc.XPathSelectElements(xPath))
            {
                yield return new XmlConfig(null, new XDocument(n));
            }
        }

        /// <summary>
        /// 按照XPath检索Xlement，只能返回单个Xelement
        /// e.g. "/config/general/interval" 从根节点检索config节点下general节点下的interval节点
        ///     "/config/tick[@type='origin']" 从根节点检索config节点下，属性值type='origin'的tick节点
        /// </summary>
        /// <param name="xPath">XPath语句</param>
        /// <returns>选中的单一element</returns>
        private XElement LocateXPath(string xPath)
        {
            return xDoc.XPathSelectElement(xPath);
        }

        private XElement LocateElementXPath(params string[] keys)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var n in keys)
            {
                sb.Append("/");
                sb.Append(n);
            }
            return LocateXPath(sb.ToString());
        }
    }
}
