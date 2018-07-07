可以成功运行的执行脚本： 

1. dotnet build
2. dotnet .\bin\Debug\netcoreapp2.0\Shuxiao.Wang.Cit.dll -c 'git@github.com:keaising/PythonScripts.git'

失败的脚本：

1. dotnet publish -r win-x64 -c release
2. 去对应的release\native文件夹运行 Shuxiao.Wang.Cit.exe文件: .\Shuxiao.Wang.Cit.exe -c 'git@github.com:keaising/PythonScripts.git'

报错信息：
Method 'CommandLine.OptionAttribute..ctor' not found.