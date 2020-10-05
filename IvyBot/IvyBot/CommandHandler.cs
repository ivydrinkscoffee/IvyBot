﻿using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using IvyBot.Configuration;

namespace IvyBot
{
    public class CommandHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _cmdService;
        private readonly IServiceProvider _services;
        private readonly IConfiguration _config;

        public CommandHandler(DiscordSocketClient client, CommandService cmdService, IServiceProvider services, IConfiguration config)
        {
            _client = client;
            _cmdService = cmdService;
            _services = services;
            _config = config;
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
            if (socketMessage.Author.IsBot) return;

            var userMessage = socketMessage as SocketUserMessage;
            if (userMessage is null)
                return;

            if (!(userMessage.HasMentionPrefix(_client.CurrentUser, ref argPos) || userMessage.HasStringPrefix(_config.GetValueFor(Constants.BotPrefix), ref argPos) || userMessage.HasStringPrefix("osu!", ref argPos)))
                return;

            var context = new SocketCommandContext(_client, userMessage);
            var result = await _cmdService.ExecuteAsync(context, argPos, _services);
        }

        private Task LogAsync(LogMessage logMessage)
        {
            Console.WriteLine(logMessage.Message);
            return Task.CompletedTask;
        }
    }
}