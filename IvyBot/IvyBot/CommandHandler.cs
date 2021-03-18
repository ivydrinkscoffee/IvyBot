using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using IvyBot.Configuration;
using IvyBot.Services;

namespace IvyBot {
    public class CommandHandler {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _cmdService;
        private readonly IServiceProvider _services;
        private readonly IConfiguration _config;
        private readonly LogService _logService;

        public CommandHandler (DiscordSocketClient client, CommandService cmdService, IServiceProvider services, IConfiguration config, LogService logService) {
            _client = client;
            _cmdService = cmdService;
            _services = services;
            _config = config;
            _logService = logService;
        }

        public async Task InitializeAsync () {
            await _cmdService.AddModulesAsync (Assembly.GetEntryAssembly (), _services);

            _cmdService.Log += LogAsync;
            _client.MessageReceived += HandleMessageAsync;

            // _client.MessageUpdated += HandleUpdatedMessageAsync;
            // _client.MessageDeleted += HandleDeletedMessageAsync;
        }

        private async Task HandleMessageAsync (SocketMessage socketMessage) {
            var argPos = 0;

            if (socketMessage.Author.IsBot)
                return;

            var userMessage = socketMessage as SocketUserMessage;

            if (userMessage is null)
                return;

            if (!(userMessage.HasMentionPrefix (_client.CurrentUser, ref argPos) || userMessage.HasStringPrefix (_config.GetValueFor (Constants.BotPrefix), ref argPos)))
                return;

            var context = new SocketCommandContext (_client, userMessage);
            var result = await _cmdService.ExecuteAsync (context, argPos, _services);

            if (result.Error != null) {
                switch (result.Error) {
                    case CommandError.UnknownCommand:
                        return;
                    default:
                        await context.Channel.SendMessageAsync ($"<:xmark:314349398824058880> {result.ErrorReason}");
                        break;
                }
            }
        }

        /*
        private async Task HandleUpdatedMessageAsync(Cacheable<IMessage, ulong> before, SocketMessage after, ISocketMessageChannel channel)
        {
            ulong logChannelId = 771314761807691808;
            var logChannel = _client.GetChannel(logChannelId) as ISocketMessageChannel;
            
            try
            {
                var message = await before.GetOrDownloadAsync();
                
                if (after.Author.IsBot == true || after is null)
                {
                    return;
                }
                else
                {
                    string channelName = channel.Name;

                    var author = after.Author as SocketUser;
                    var avatar = author.GetAvatarUrl(ImageFormat.Auto);
                    var defaultAvatar = author.GetDefaultAvatarUrl();

                    EmbedAuthorBuilder embedAuthorBuilder = new EmbedAuthorBuilder();

                    embedAuthorBuilder.WithName($"Message edited by {author.Username}#{author.Discriminator} in #{channelName}");
            
                    if (avatar == null)
                    {
                        embedAuthorBuilder.WithIconUrl(defaultAvatar);
                    }
                    else
                    {
                        embedAuthorBuilder.WithIconUrl(avatar);
                    }

                    EmbedBuilder builder = new EmbedBuilder();

                    builder.AddField("Before", $"```diff\n- {message}\n```", true);
                    builder.AddField("After", $"```diff\n+ {after}\n```", true);
                    builder.WithAuthor(embedAuthorBuilder);
                    builder.WithCurrentTimestamp();
                    builder.WithColor(Color.Blue);

                    await logChannel.SendMessageAsync("", false, builder.Build());
                }
            }
            catch (Exception ex)
            {
                await logChannel.SendMessageAsync(ex.Message);
            }
        }

        private async Task HandleDeletedMessageAsync(Cacheable<IMessage, ulong> message, ISocketMessageChannel channel)
        {
            ulong logChannelId = 771314761807691808;
            var logChannel = _client.GetChannel(logChannelId) as ISocketMessageChannel;
            
            try
            {
                var originalMessage = await message.GetOrDownloadAsync();
                
                if (originalMessage.Author.IsBot == true || originalMessage is null)
                {
                    return;
                }
                else
                {
                    string channelName = channel.Name;
                    
                    var author = originalMessage.Author as SocketUser;
                    var avatar = author.GetAvatarUrl(ImageFormat.Auto);
                    var defaultAvatar = author.GetDefaultAvatarUrl();

                    EmbedAuthorBuilder embedAuthorBuilder = new EmbedAuthorBuilder();

                    embedAuthorBuilder.WithName($"Message deleted by {author.Username}#{author.Discriminator} in #{channelName}");
            
                    if (avatar == null)
                    {
                        embedAuthorBuilder.WithIconUrl(defaultAvatar);
                    }
                    else
                    {
                        embedAuthorBuilder.WithIconUrl(avatar);
                    }

                    EmbedBuilder builder = new EmbedBuilder();

                    builder.AddField("Message", $"```diff\n- {originalMessage}\n```", true);
                    builder.WithAuthor(embedAuthorBuilder);
                    builder.WithCurrentTimestamp();
                    builder.WithColor(Color.Blue);

                    await logChannel.SendMessageAsync("", false, builder.Build());
                }
            }
            catch (Exception ex)
            {
                await logChannel.SendMessageAsync(ex.Message);
            }
        }
        */

        private async Task LogAsync (LogMessage logMessage) {
            await _logService.LogAsync (logMessage);
            await Task.CompletedTask;
        }
    }
}