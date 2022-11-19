using MinecraftServerInfo;
using MinecraftServerInfo.Model;

MinecraftProtocolVersion minecraftProtocolVersion = MinecraftProtocolVersion.FromGameVersion("1.8.9");
MinecraftServer minecraftServer = new MinecraftServer("mc.hypixel.net", 25565, minecraftProtocolVersion);
CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(3000);
ServerInfo serverInfo = await minecraftServer.PingAsync(cancellationTokenSource.Token);

Console.WriteLine(serverInfo.PlayerInfo.ToString());