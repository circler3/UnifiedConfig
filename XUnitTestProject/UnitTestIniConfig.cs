using System;
using Xunit;
using UnifiedConfig;
using Newtonsoft.Json;
namespace XUnitTestProject
{
    public class UnitTestIniConfig
    {
        [Fact(DisplayName = "ini test")]
        public void Test1()
        {
            string str = @";距离单位为m，时间单位为ms
[Dynamic]
Interval = 5
Delay = 4000

[Default]
Interval = 5
";
            System.IO.File.WriteAllText("test.ini", str);
            ConfigManager config = new ConfigManager("test.ini");
            Assert.Equal("5", config[@"/Default/Interval"]);
            config[@"//Default/Interval"] = "6";
            Assert.Equal("6", config[@"//Default/Interval"]);
            config.Save();
            config = new ConfigManager("test.ini");
            Assert.Equal("6", config[@"//Default/Interval"]);
            Assert.Equal("6", config.GetValue("Default", "Interval"));
        }

        [Fact(DisplayName ="xml test")]
        public void Test2()
        {
            ConfigManager config = new ConfigManager("yard.xml");
            Assert.Equal("200", config[@"Yard/Section[@ID='1']/Block/MaxHeight"]);
            config[@"//Section[@ID='1']/Block/MaxHeight"] = "6";
            Assert.Equal("6", config[@"Yard/Section[@ID='1']/Block/MaxHeight"]);
            config.Save();
            config = new ConfigManager("yard.xml");
            Assert.Equal("6", config[@"Yard/Section[@ID='1']/Block/MaxHeight"]);
            Assert.Equal("6", config.GetValue("Yard", "Section[@ID = '1']", "Block", "MaxHeight"));

        }
        [Fact(DisplayName = "json test")]
        public void Test3()
        {
            string json = @"{
 'person': [
     {
       '@id': '1',
       'name': 'Alan',
       'url': 'http://www.google.com'
     },
     {
       '@id': '2',
       'name': 'Louis',
       'url': 'http://www.yahoo.com'
     }
   ]
 }
";
            System.IO.File.WriteAllText("test.json", json);
            ConfigManager config = new ConfigManager("test.json");
            Assert.Equal("Alan", config[@"//person[@id='1']/name"]);
            config[@"//person[@id='1']/name"] = "Lucas";
            Assert.Equal("Lucas", config[@"//person[@id='1']/name"]);
            config.Save();
            config = new ConfigManager("test.json");
            Assert.Equal("Lucas", config[@"/person[@id='1']/name"]);
            Assert.Equal("Lucas", config.GetValue("person[@id='1']", "name"));
        }
    }
}
