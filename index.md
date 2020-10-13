# invite
click [here](https://discord.com/api/oauth2/authorize?client_id=719933579865489499&permissions=8&scope=bot) to add the **bot** to a **server** of your choice
# building
can be built on windows, linux, and macos using the [.net core sdk](https://dotnet.microsoft.com/download/dotnet-core) or can be built **directly** through [visual studio](https://visualstudio.microsoft.com) or [visual studio code](https://code.visualstudio.com)
# running and hosting
clone the repository with `git clone https://github.com/Ivy-Wusky/ivy-bot.git` then rename **App.config.example** to **App.config** and edit it accordingly to your **discord** and **lavalink** environment

```xml
<?xml version="1.0" encoding="utf-8"?>  
<configuration>  
    <startup>   
        <supportedRuntime version="v4.0" sku="netcoreapp2.1"/>  
    </startup>  
  <appSettings>  
    <add key="Token" value="replace with your bot token"/>
    <add key="Prefix" value="replace with a prefix of your choice"/>
    <add key="Host" value="replace with the set lavalink address"/>
    <add key="Port" value="replace with the set lavalink port"/>
    <add key="Password" value="replace with the set lavalink password"/>
  </appSettings>  
</configuration>  
```

remember that if you are running your **lavalink** instance on [heroku](https://www.heroku.com) the port will always be **80** regardless of the port you set

# future plans
additional commands and [migrating to latest .net core and dependencies](https://github.com/Ivy-Wusky/ivy-bot/projects/1)
# credits
[discord.net](https://github.com/discord-net/Discord.Net)

[victoria](https://github.com/Yucked/Victoria)

[newtonsoft.json](https://github.com/JamesNK/Newtonsoft.Json)
# special thanks
[lavalink](https://github.com/Frederikam/Lavalink)

[some random api](https://some-random-api.ml)
# support
**contact** me on my [discord server](https://discord.gg/svMC3dt) for further help
