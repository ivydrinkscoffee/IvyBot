using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IvyBot.Modules
{
    [Name("Interaction")]
    public class InteractionModule : ModuleBase<SocketCommandContext>
    {
        [Command("invite", RunMode = RunMode.Async)]
        [Summary("DMs you and invite link so you can invite me to a server of your choice")]
        public async Task InviteAsync()
        {
            await Context.User.SendMessageAsync("https://discord.com/api/oauth2/authorize?client_id=719933579865489499&permissions=8&scope=bot");
            await ReplyAsync("Check DMs");
        }

        [Command("pat", RunMode = RunMode.Sync)]
        [Summary("Pat someone")]
        public async Task Pat(SocketUser user = null)
        {
            var userInfo = user ?? Context.Client.CurrentUser;
            string json = new WebClient().DownloadString("https://some-random-api.ml/animu/pat");

            Dictionary<string, string> items = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            await Context.Channel.SendMessageAsync("*pats* " + $"*{userInfo.Mention}*");
            await Context.Channel.SendMessageAsync(items["link"]); 
        }

        [Command("hug", RunMode = RunMode.Sync)]
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
