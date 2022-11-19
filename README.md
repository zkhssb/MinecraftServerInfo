# MinecraftServerInfo

>  快速获取Minecraft服务器信息
>
> 理论上支持所有版本服务器 (已对Description字段做兼容)

例子

```c#
using MinecraftServerInfo;
using MinecraftServerInfo.Model;

// 可指定一个协议版本 (可以但是没必要 (划掉)
// MinecraftProtocolVersion minecraftProtocolVersion = MinecraftProtocolVersion.FromGameVersion("1.8.9");
MinecraftServer minecraftServer = new MinecraftServer("mc.hypixel.net", 25565);
CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(3000);
ServerInfo serverInfo = await minecraftServer.PingAsync(cancellationTokenSource.Token);

Console.WriteLine("==========================================================");
Console.WriteLine(serverInfo.Description.GetText());
Console.WriteLine(serverInfo.PlayerInfo.ToString());    // 此处可自己获取Players数组
Console.WriteLine($"{serverInfo.Version.VersionName} ProtocolId:{serverInfo.Version.ProtocolId}");
Console.WriteLine("==========================================================");
```

## 服务器信息

> ServerInfo类封装了我的世界服务器返回的所有信息

- 玩家信息

   - 最大玩家数 - MaxPlayer

   - 在线玩家数 - Online

   - 玩家列表 - Players
     - Name
     - UUID

- 服务器描述

   - 纯文本

   - Json文本(高版本)

- 服务器版本
  - 协议版本
  - 版本名称
- 服务器图标 (base64)
- 高版本信息
  - 是否强制执行安全聊天
  - 是否启用启用聊天预览功能

## 截图

![debug](https://github.com/zkhssb/MinecraftServerInfo/blob/master/Images/debug.png)

![hypixel](https://github.com/zkhssb/MinecraftServerInfo/blob/master/Images/hypixel.png)

![myserver](https://github.com/zkhssb/MinecraftServerInfo/blob/master/Images/myserver.png)