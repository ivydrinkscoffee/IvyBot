using System.Threading.Tasks;
using Discord;
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

                EmbedBuilder builder = new EmbedBuilder();

                builder.WithImageUrl(link.Result);
                builder.WithFooter("Powered by rule34.xxx");
                builder.WithColor(Color.Blue);
                builder.WithCurrentTimestamp();

                await Context.Channel.SendMessageAsync("", false, builder.Build());
            }
            else
            {
                await ReplyAsync("Please only use NSFW commands in an NSFW channel");
            }
        }
    }
}