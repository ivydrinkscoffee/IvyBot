# IvyBot
Multi-purpose [Discord](http://discordapp.com/) bot written in **C#**
# Invite
Click [here](https://discord.com/api/oauth2/authorize?client_id=719933579865489499&permissions=8&scope=bot) to add the **bot** to a **server** of your choice
# Building
Can be built on Windows, Linux, and macOS using the [.NET 5 SDK](https://dotnet.microsoft.com/download/dotnet/5.0) or can be built **directly** through [Visual Studio](https://visualstudio.microsoft.com) or [Visual Studio Code](https://code.visualstudio.com)
# Running and hosting
Clone the repository with `git clone https://github.com/ivydrinkscoffee/IvyBot.git` then rename **App.config.example** to **App.config** and edit it accordingly to your **Discord** and **Lavalink** environment
```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
    <startup>
        <supportedRuntime version="v4.0" sku="net5.0" />
    </startup>
  <appSettings>
    <add key="Token" value="replace with your bot token" />
    <add key="Prefix" value="replace with a prefix of your choice" />
    <add key="Host" value="replace with the set lavalink address" />
    <add key="Port" value="replace with the set lavalink port" />
    <add key="Password" value="replace with the set lavalink password" />
  </appSettings>
</configuration>
```
Remember that if you are running your **Lavalink** instance on [Heroku](https://www.heroku.com) the port will always be **80** regardless of the port you set
# Future plans
Cleaning up the rest of `MusicService` but nothing big at the moment, feel free to suggest any ideas!
# Credits
[Discord.Net](https://github.com/discord-net/Discord.Net)

[Victoria](https://github.com/Yucked/Victoria)

[Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json)
# Special thanks
[Lavalink](https://github.com/Frederikam/Lavalink)

[some random api](https://some-random-api.ml)
# Support
**Contact** me on my [Discord server](https://discord.gg/svMC3dt) for further help
