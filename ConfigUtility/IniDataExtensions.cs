 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ConfigUtilty
{
    internal static class ConfigDataExtensions
    {
        public static string ToIni(this XDocument model)
        {
            return "";
        }

        public static XDocument ToXml(this string IniStr)
        {
            return new XDocument("Nodes");
        }

    }

}
