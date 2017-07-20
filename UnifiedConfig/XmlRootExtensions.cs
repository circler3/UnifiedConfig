using System;
using System.Collections.Generic;
using System.Text;

namespace UnifiedConfig
{
    internal static class XmlRootExtensions
    {
        internal static string AddRoot(this XmlConfig config, string xPath)
        {
            if (xPath.StartsWith("/")) xPath = "/" + config._xDoc.Root.Name.LocalName + xPath;
            return xPath;
        }

        internal static string[] AddRoot(this XmlConfig config, string[] keys)
        {
            string[] str = new string[keys.Length + 1];
            str[0] = config._xDoc.Root.Name.LocalName;
            Array.Copy(keys, 0, str, 1, keys.Length);
            return str;
        }
    }
}
