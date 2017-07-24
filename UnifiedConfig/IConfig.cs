using System;
using System.Collections.Generic;
using System.Text;

namespace UnifiedConfig
{
    interface IConfig
    {

        string GetValue(params string[] keys);

        bool SetValue(string value, params string[] keys);

        void Save(string filepath = null);

        string this[string findingPath] { get; set; }
    }
}
