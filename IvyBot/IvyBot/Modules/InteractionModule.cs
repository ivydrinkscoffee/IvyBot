using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System;
using Discord.Rest;

namespace IvyBot.Modules
{
    [Name("Interaction")]
    public class InteractionModule : ModuleBase<SocketCommandContext>
    {
        [Command("invite")]
        [Summary("DMs you and invite link so you can invite me to a server of your choice")]
        public async Task InviteAsync()
        {
            await Context.User.SendMessageAsync("https://discord.com/api/oauth2/authorize?client_id=719933579865489499&permissions=8&scope=bot");
            await ReplyAsync("Check DMs");
        }

        private readonly DiscordSocketClient _client;

        public InteractionModule(DiscordSocketClient client)
        {
            _client = client;
        }
        
        [Command("getinvites")]
        [Summary("Returns all the server invites from the specified guild through its ID")]
        public async Task GetInviteAsync(ulong guildId)
        {
            try
            {
                var guild = _client.GetGuild(guildId);
                var inviteList = await guild.GetInvitesAsync() as IEnumerable<RestInvite>;

                foreach (var invite in inviteList)
                {
                    await ReplyAsync(invite.ToString());
                }
            }
            catch (Exception ex)
            {
                await ReplyAsync(ex.Message);
            }
        }
        
        [Command("pat")]
        [Summary("Pat someone")]
        public async Task Pat(SocketUser user = null)
        {
            var userInfo = user ?? Context.Client.CurrentUser;
            string json = new WebClient().DownloadString("https://some-random-api.ml/animu/pat");

            Dictionary<string, string> items = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            await Context.Channel.SendMessageAsync("*pats* " + $"*{userInfo.Mention}*");
            await Context.Channel.SendMessageAsync(items["link"]); 
        }

        [Command("hug")]
        [Summary("Hug someone")]
        public async Task Hug(SocketUser user = null)
        {
            var userInfo = user ?? Context.Client.CurrentUser;
            string json = new WebClient().DownloadString("https://some-random-api.ml/animu/hug");

            Dictionary<string, string> items = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            await Context.Channel.SendMessageAsync("*hugs* " + $"*{userInfo.Mention}*");
            await Context.Channel.SendMessageAsync(items["link"]); 
        }
    }
}
