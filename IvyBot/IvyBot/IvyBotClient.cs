using Discord.Commands;
using Discord.WebSocket;
using Discord;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.DependencyInjection;
using Victoria;
using IvyBot.Services;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using IvyBot.Configuration;

namespace IvyBot
{
    public class IvyBotClient : ModuleBase<SocketCommandContext>
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _cmdService;
        private IServiceProvider _services;
        private readonly LogService _logService;
        private readonly IConfiguration _config;
        private Timer timer;
        private List<string> statusList = new List<string>() { "music update | .play", "people inviting me | .invite" };
        private int statusIndex = 0;

        public IvyBotClient()
        {
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                AlwaysDownloadUsers = true,
                MessageCacheSize = 100,
                LogLevel = LogSeverity.Verbose
            });

            _cmdService = new CommandService(new CommandServiceConfig
            {
                LogLevel = LogSeverity.Verbose,
                CaseSensitiveCommands = false
            });

            _logService = new LogService();

            _config = new ConfigManager();
        }

        public async Task InitializeAsync()
        {
            await _client.LoginAsync(TokenType.Bot, _config.GetValueFor(Constants.BotToken));

            timer = new Timer(async _ =>
            {
                await _client.SetGameAsync(statusList.ElementAtOrDefault(statusIndex), type: ActivityType.Listening);
                statusIndex = statusIndex + 1 == statusList.Count ? 0 : statusIndex + 1;
            },
            null,
            TimeSpan.FromSeconds(1),
            TimeSpan.FromSeconds(30));
            
            await _client.StartAsync();
            _client.Log += LogAsync;
            _services = SetupServices();

            var cmdHandler = new CommandHandler(_client, _cmdService, _services);
            await cmdHandler.InitializeAsync();

            await _services.GetRequiredService<MusicService>().InitializeAsync();

            //_client.MessageUpdated += MessageUpdated;
            
            await Task.Delay(-1);
        }

        private async Task LogAsync(LogMessage logMessage)
        {
            await _logService.LogAsync(logMessage);
        }

        private IServiceProvider SetupServices()
            => new ServiceCollection()
            .AddSingleton(_client)
            .AddSingleton(_cmdService)
            .AddSingleton(_logService)
            .AddSingleton(_config)
            .AddSingleton<LavaRestClient>()
            .AddSingleton<LavaSocketClient>()
            .AddSingleton<MusicService>()
            .BuildServiceProvider();
        
        /*internal static async Task MessageUpdated(Cacheable<IMessage, ulong> before, SocketMessage after, ISocketMessageChannel channel)
        {
            var message = await before.GetOrDownloadAsync();

            EmbedBuilder builder = new EmbedBuilder();

            builder.WithTitle("Message Edited");
            builder.AddField("Before", $"{message}", true);
            builder.AddField("After", $"{after}", true);
            builder.WithCurrentTimestamp();
            builder.WithFooter("Coded and maintained by Ivy#9804 in Discord.NET");
            builder.WithColor(Color.Blue);

            await channel.SendMessageAsync("", false, builder.Build());
        }
        */
    }
}
