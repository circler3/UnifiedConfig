using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;

namespace ConfigUtilty
{
    internal class XmlConfig : ConfigBase
    {
        protected XDocument _root;

        public XmlConfig(string filepath, XDocument content)
            :base(filepath)
        {
            _root = content;
        }

        public override void Save(string filePath = null)
        {
            FileStream fs = new FileStream(filePath ?? _sourceFilePath, FileMode.Create);
            _root.Save(fs, SaveOptions.None);
        }

        public override string GetValue(params string[] keys)
        {
            var element = LocateElement(keys);
            if (element == null) return "";
            return element.Value;
        }

        public override bool SetValue(string value, params string[] keys)
        {
            var element = LocateElement(keys);
            if (element == null) return false;
            element.Value = value;
            return true;
        }

        public override string this[string xPath]
        {
            get
            {
                return LocateXPath(xPath).Value;
            }
            set
            {
                LocateXPath(xPath).Value = value;
            }
        }

        /// <summary>
        /// 按照XPath检索Xlement，只能返回单个Xelement
        /// e.g. "/config/general/interval" 从根节点检索config节点下general节点下的interval节点
        ///     "/config/tick[@type='origin']" 从根节点检索config节点下，属性值type='origin'的tick节点
        /// </summary>
        /// <param name="xPath">XPath语句</param>
        /// <returns>选中的单一element</returns>
        public XElement LocateXPath(string xPath)
        {
            return _root.XPathSelectElement(xPath);
        }

        private XElement LocateElement(params string[] keys)
        {
            if (_root == null)
            {
                return null;
            }
            XElement element = _root.Root;
            for (int i = 0; i < keys.Length; i++)
            {
                element = element.Element(keys[i]);
            }
            return element;
        }
    }
}
