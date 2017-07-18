using System;
using Xunit;
using ConfigUtilty;

namespace XUnitTestProject
{
    public class UnitTestIniConfig
    {
        [Fact]
        public void Test1()
        {
            string str = @";���뵥λΪm��ʱ�䵥λΪms
[Dynamic]
Interval = 5
Delay = 4000

[Default]
Interval = 5
";
            System.IO.File.WriteAllText("test.ini", str);
            ConfigManager config = new ConfigManager("test.ini");
            Assert.Equal("5", config[@"//Default/Interval"]);
            config[@"//Default/Interval"] = "6";
            Assert.Equal("6", config[@"//Default/Interval"]);
            config.Save();
            config = new ConfigManager("test.ini");
            Assert.Equal("6", config[@"//Default/Interval"]);
        }

        [Fact]
        public void Test2()
        {
            ConfigManager config = new ConfigManager("yard.xml");
            Assert.Equal("200", config[@"Yard/Section[@ID='1']/Block/MaxHeight"]);
            config[@"//Section[@ID='1']/Block/MaxHeight"] = "6";
            Assert.Equal("6", config[@"Yard/Section[@ID='1']/Block/MaxHeight"]);
            config.Save();
            config = new ConfigManager("yard.xml");
            Assert.Equal("6", config[@"Yard/Section[@ID='1']/Block/MaxHeight"]);

        }
    }
}