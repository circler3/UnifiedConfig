using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace UnifiedConfig
{
    internal static class ConfigDataExtensions
    {

        public static string ToIni(this XDocument model)
        {
            StringBuilder sb = new StringBuilder();
            WriteNode(model.Root, sb);
            return sb.ToString();
        }

        private static void WriteNode(XElement node, StringBuilder sb)
        {
            foreach (var ele in node.DescendantNodes())
            {
                if (ele is XComment)
                {
                    sb.Append(";");
                    sb.Append(((XComment)ele).Value);
                    sb.Append(Environment.NewLine);
                }
                else if (ele is XElement)
                {
                    if ((ele as XElement).HasElements)
                    {
                        sb.Append("[");
                        sb.Append((ele as XElement).Name.LocalName);
                        sb.Append("]");
                        sb.Append(Environment.NewLine);
                    }
                    else
                    {
                        sb.Append((ele as XElement).Name.LocalName);
                        sb.Append("=");
                        sb.Append((ele as XElement).Value);
                        sb.Append(Environment.NewLine);
                    }
                }
            }
        }

        public static XDocument ToXml(this string[] iniStr)
        {
            XDocument xdoc = new XDocument(new XElement("G" + Guid.NewGuid().ToString("N")));
            XElement node = xdoc.Root;
            foreach (var line in iniStr)
            {
                var cline = line.Trim();
                if (string.IsNullOrWhiteSpace(cline)) continue;
                switch (line[0])
                {
                    case ';':
                        node.Add(new XComment(cline.Substring(1)));
                        break;
                    case '[':
                        node = new XElement(cline.Substring(1, line.Length - 2));
                        xdoc.Root.Add(node);
                        break;
                    case '\r':
                        break;
                    default:
                        int index = cline.IndexOf('=');
                        if (index < 1)
                        {
                            throw new Exception("Property does not contains '=' operator");
                        }
                        node.Add(new XElement(cline.Substring(0, index).Trim(), cline.Substring(index + 1)));
                        break;
                }

            }
            return xdoc;
        }

    }

}
