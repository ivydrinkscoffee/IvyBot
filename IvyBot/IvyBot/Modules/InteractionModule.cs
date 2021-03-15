using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Newtonsoft.Json;

namespace IvyBot.Modules {
    [Name ("Interaction")]
    public class InteractionModule : ModuleBase<SocketCommandContext> {
        [Command ("invite")]
        [Summary ("DMs you and invite link so you can invite me to a server of your choice")]
        public async Task InviteAsync () {
            await Context.User.SendMessageAsync ("<:invite:658538493949116428> https://discord.com/api/oauth2/authorize?client_id=719933579865489499&permissions=8&scope=bot");
            await ReplyAsync ("<:channel:585783907841212418> Check DMs");
        }

        [Command ("pat")]
        [Summary ("Pat someone")]
        public async Task Pat (SocketUser user = null) {
            var userInfo = user ?? Context.Client.CurrentUser;
            string json = new WebClient ().DownloadString ("https://some-random-api.ml/animu/pat");

            Dictionary<string, string> items = JsonConvert.DeserializeObject<Dictionary<string, string>> (json);

            await ReplyAsync ("*pats* " + $"*{userInfo.Mention}*");
            await ReplyAsync (items["link"]);
        }

        [Command ("hug")]
        [Summary ("Hug someone")]
        public async Task Hug (SocketUser user = null) {
            var userInfo = user ?? Context.Client.CurrentUser;
            string json = new WebClient ().DownloadString ("https://some-random-api.ml/animu/hug");

            Dictionary<string, string> items = JsonConvert.DeserializeObject<Dictionary<string, string>> (json);

            await ReplyAsync ("*hugs* " + $"*{userInfo.Mention}*");
            await ReplyAsync (items["link"]);
        }
    }
}