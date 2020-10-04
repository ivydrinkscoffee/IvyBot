# invite
click [here](https://discord.com/api/oauth2/authorize?client_id=719933579865489499&permissions=8&scope=bot) to add the **bot** to a **server** of your choice
# building
can be built on windows, linux, and macos using the [.net core sdk](https://dotnet.microsoft.com/download/dotnet-core) or can be built **directly** through [visual studio](https://visualstudio.microsoft.com) or [visual studio code](https://code.visualstudio.com)
# running and hosting
edit [this line](https://github.com/Ivy-Wusky/ivy-bot/blob/0571bdf61c84fe39e898917ed8f64a2d2b3c7120/IvyBot/IvyBot/IvyBotClient.cs#L45) as follows

```cs
await _client.LoginAsync(TokenType.Bot, "replace with your bot token");
```

and if you would like **music** functionality you will need to edit the configurations [here](https://github.com/Ivy-Wusky/ivy-bot/blob/0571bdf61c84fe39e898917ed8f64a2d2b3c7120/IvyBot/IvyBot/Services/MusicService.cs#L20) and [here](https://github.com/Ivy-Wusky/ivy-bot/blob/0571bdf61c84fe39e898917ed8f64a2d2b3c7120/IvyBot/IvyBot/Services/MusicService.cs#L137) to fit your currently running **lavalink** instance as follows

```cs
_lavaRestClient = new LavaRestClient(new Victoria.Configuration {
    Host = "replace with the set address",
    Port = replace with the set port,
    Password = "replace with the set password"
});
```

```cs
await _lavaSocketClient.StartAsync(_client, new Victoria.Configuration {
Host = "replace with the set address",
Port = replace with the set port,
Password = "replace with the set password"
});
```

make sure to pass the **same** configuration in both and if you are running your **lavalink** instance on [heroku](https://www.heroku.com) the port will always be **80** regardless of the port you set
# future plans
customizable configuration most likely in the **json** or **xml** format which would mean not having to edit source files at all
# credits
[discord.net](https://github.com/discord-net/Discord.Net)

[victoria](https://github.com/Yucked/Victoria)

[newtonsoft.json](https://github.com/JamesNK/Newtonsoft.Json)
# special thanks
[lavalink](https://github.com/Frederikam/Lavalink)

[some random api](https://some-random-api.ml)
# support
contact me on my [discord server](https://discord.gg/svMC3dt) for further help
