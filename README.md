# UnifiedConfig
A common C# class to manipulate xml, ini and json config files with unified interfaces.

[![Version](https://img.shields.io/nuget/v/UnifiedConfig.svg)](https://www.nuget.org/packages/UnifiedConfig)
[![Downloads](https://img.shields.io/nuget/dt/UnifiedConfig.svg)](https://www.nuget.org/packages/UnifiedConfig)

## Features
* Wrapped the difference among xml, ini and json, you can access the config file without the consideration of format.
* Provide XPath as default locating method.
* Saving file will not change the original format.

## Usage
Recommend [nuget package](https://www.nuget.org/packages/UnifiedConfig)
```powershell
Install-Package UnifiedConfig
```
*OR*
1. compile the class library and add reference of the dlls including the "UnifiedConfig.dll" to your project.
2. add *using* statement to your namespaces.
3. you can call the functions by using the following code or just read the test class.

```C#
ConfigManager config = new ConfigManager("test.ini");
Assert.Equal("5", config[@"//Default/Interval"]);
```
> alternatively, you can directly start a project from the solution. By adding a new project into the solution, you can easily make use of the class. A unittest project is provided.

## Roadmap

Issues and PRs are welcomed.

- [X] Support for json
- [X] Nuget package
- [X] Auto-inference the type of config file without extension detection
- [X] Support returning more types other than string (via ToObject<T> method)
- [X] Support of IEnumerable for sophisticated query
- [ ] ~~Support for int indexer~~ is removed since the xpath supported already
- [ ] *Ready to go* template project
- [X] Case-sensitive swtich for path matching

## Update Notes
UPDATE 2017/07/29: Add Case-sensitive swtich for path matching

UPDATE 2017/07/25: Add Name property and fix the IEnumerable bug.

UPDATE 2017/07/24: Add IEnumerable support for sophisticated query.

UPDATE 2017/07/22: Add Auto-inference and type conversion.

UPDATE 2017/07/20: Add json support.

UPDATE 2017/07/18: Nuget package supported.