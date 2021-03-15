using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using IvyBot.Configuration;
using IvyBot.Services;
using Microsoft.Extensions.DependencyInjection;
using Victoria;

namespace IvyBot {
    public class IvyBotClient {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _cmdService;
        private IServiceProvider _services;
        private readonly LogService _logService;
        private readonly IConfiguration _config;
        private Timer _timer;
        private readonly List<string> _statusList = new List<string> () { "people inviting me | .invite", "people needing help | .help" };
        private int _statusIndex = 0;

        public IvyBotClient () {
            _client = new DiscordSocketClient (new DiscordSocketConfig {
                AlwaysDownloadUsers = true,
                    MessageCacheSize = 100,
                    LogLevel = LogSeverity.Verbose
            });

            _cmdService = new CommandService (new CommandServiceConfig {
                LogLevel = LogSeverity.Verbose,
                    CaseSensitiveCommands = false,
                    DefaultRunMode = RunMode.Async
            });

            _logService = new LogService ();

            _config = new ConfigManager ();
        }

        public async Task InitializeAsync () {
            await _client.LoginAsync (TokenType.Bot, _config.GetValueFor (Constants.BotToken));

            await _client.SetStatusAsync (UserStatus.Idle);

            _timer = new Timer (async _ => {
                    await _client.SetGameAsync (_statusList.ElementAtOrDefault (_statusIndex), type : ActivityType.Listening);
                    _statusIndex = _statusIndex + 1 == _statusList.Count ? 0 : _statusIndex + 1;
                },
                null,
                TimeSpan.FromSeconds (1),
                TimeSpan.FromSeconds (30));

            await _client.StartAsync ();
            _client.Log += LogAsync;
            _services = SetupServices ();

            var cmdHandler = new CommandHandler (_client, _cmdService, _services, _config, _logService);
            await cmdHandler.InitializeAsync ();

            await _services.GetRequiredService<MusicService> ().InitializeAsync ();

            await Task.Delay (-1);
        }

        private async Task LogAsync (LogMessage logMessage) {
            await _logService.LogAsync (logMessage);
        }

        private IServiceProvider SetupServices () => new ServiceCollection ()
            .AddSingleton (_client)
            .AddSingleton (_cmdService)
            .AddSingleton (_logService)
            .AddSingleton (_config)
            .AddSingleton<LavaRestClient> ()
            .AddSingleton<LavaSocketClient> ()
            .AddSingleton<MusicService> ()
            .BuildServiceProvider ();
    }
}