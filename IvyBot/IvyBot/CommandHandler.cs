using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using IvyBot.Configuration;
using IvyBot.Services;

namespace IvyBot
{
    public class CommandHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _cmdService;
        private readonly IServiceProvider _services;
        private readonly IConfiguration _config;
        private readonly LogService _logService;

        public CommandHandler(DiscordSocketClient client, CommandService cmdService, IServiceProvider services, IConfiguration config, LogService logService)
        {
            _client = client;
            _cmdService = cmdService;
            _services = services;
            _config = config;
            _logService = logService;
        }

        public async Task InitializeAsync()
        {
            await _cmdService.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
            _cmdService.Log += LogAsync;
            _client.MessageReceived += HandleMessageAsync;
        }

        private async Task HandleMessageAsync(SocketMessage socketMessage)
        {
            var argPos = 0;
            
            if (socketMessage.Author.IsBot) 
                return;

            var userMessage = socketMessage as SocketUserMessage;
            if (userMessage is null)
                return;

            if (!(userMessage.HasMentionPrefix(_client.CurrentUser, ref argPos) || userMessage.HasStringPrefix(_config.GetValueFor(Constants.BotPrefix), ref argPos)))
                return;

            var context = new SocketCommandContext(_client, userMessage);
            var result = await _cmdService.ExecuteAsync(context, argPos, _services);
        }

        private async Task LogAsync(LogMessage logMessage)
        {
            await _logService.LogAsync(logMessage);
            return await Task.CompletedTask;
        }
    }
}
