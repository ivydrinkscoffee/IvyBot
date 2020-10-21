using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using IvyBot.Services;

namespace IvyBot.Modules
{
    [Name("NSFW")]
    public class NSFWModule : ModuleBase<SocketCommandContext>
    {
        [Command("rule34", RunMode = RunMode.Async)]
        [Summary("Gets porn from rule34.xxx specified by the entered text")]
        public async Task Rule34([Remainder] string tag)
        {
            var channel = Context.Channel as SocketTextChannel;

            if (channel.IsNsfw == true)
            {
                var link = NSFWService.GetRule34File(tag);

                await ReplyAsync(link.Result);
            }
            else
            {
                await ReplyAsync("Please only use NSFW commands in an NSFW channel");
            }
        }

        [Command("e621", RunMode = RunMode.Async)]
        [Summary("Gets porn from e621.net specified by the entered text")]
        public async Task E621([Remainder] string tag)
        {
            var channel = Context.Channel as SocketTextChannel;

            if (channel.IsNsfw == true)
            {
                var link = NSFWService.GetE621File(tag);

                await ReplyAsync(link.Result);
            }
            else
            {
                await ReplyAsync("Please only use NSFW commands in an NSFW channel");
            }
        }
    }
}