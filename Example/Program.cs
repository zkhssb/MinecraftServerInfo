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