using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using System.Xml.XPath;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace UnifiedConfig
{
    internal class JsonConfigManager : XmlConfig
    {

        public JsonConfigManager(string filepath)
            : base(filepath, DecorateJson(File.ReadAllText(filepath)))
        {

        }

        public override void Save(string filepath = null)
        {
            File.WriteAllText(filepath ?? sourceFilePath, ToXmlWithoutRoot());
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
            get => base[this.AddRoot(xPath)];
            set => base[this.AddRoot(xPath)] = value;
        }

        /// <summary>
        /// Single root element (not a array) is not a must for json. Hence a decorator is necessary.
        /// </summary>
        /// <param name="jsonStr"></param>
        /// <returns>Decorated string</returns>
        private static XDocument DecorateJson(string jsonStr)
        {
            return JsonConvert.DeserializeXNode(jsonStr, "G" + Guid.NewGuid().ToString("N"));
        }

        private string ToXmlWithoutRoot()
        {
            JObject result = JObject.FromObject(xDoc.Root);
            var dest = result.Root.Children().First().Children().First();
            return dest.ToString();
        }
    }
}
