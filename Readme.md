# dnc-git 

可以在任意位置将 github repoes 放到合适目录下的 .Net Core Global Tool

## 解决的问题

类似于 go get 命令可以将任意 github repoes 克隆到 /gopath/src/github.com/user_name/repo_name 下，本工具在初次运行时需要输入 -p 指令配置本地的 github.com 目录地址，此后只需要简单 dnc-git -c 'git@github.com/user_name/repo_name.git' 即可将该 repo 克隆到配置的 github.com 目录下并按照 user_name 划分到不同文件夹下

## 使用方法

1. 安装 [.Net Core SDK 2.1 Build300](https://www.microsoft.com/net/learn/get-started/windows) 以上版本
2. 执行命令安装本工具：`dotnet tool install -g dnc-git`
3. 初次运行时请先修改目录到你的 github.com 所在目录，例如： `dnc-git -p 'C:\Users\Username\source\github.com'`
4. clone github repo 时执行 `dnc-git -c 'git@github.com/user_name/repo_name'` 即可