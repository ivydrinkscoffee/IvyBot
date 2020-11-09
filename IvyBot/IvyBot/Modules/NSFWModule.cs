using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using IvyBot.Services;

namespace IvyBot.Modules
{
    [Name("NSFW")]
    public class NSFWModule : ModuleBase<SocketCommandContext>
    {
        [Command("rule34")]
        [Summary("Gets images or videos from rule34.xxx specified by the entered text")]
        public async Task Rule34([Remainder] string tag)
        {
            var channel = Context.Channel as SocketTextChannel;

            if (channel.IsNsfw == true)
            {
                var link = await NSFWService.GetRule34File(tag);

                await ReplyAsync(link);
            }
            else
            {
                await ReplyAsync("Please only use NSFW commands in an NSFW channel");
            }
        }

        [Command("e621")]
        [Summary("Gets images or videos from e621.net specified by the entered text")]
        public async Task E621([Remainder] string tag)
        {
            var channel = Context.Channel as SocketTextChannel;

            if (channel.IsNsfw == true)
            {
                var link = await NSFWService.GetE621File(tag);
                
                if (link.Contains("sample"))
                {
                    var removeSampleFromLink = link.Replace("sample", "");
                    await ReplyAsync(removeSampleFromLink);
                }
                else if (link.Contains("preview"))
                {
                    var removePreviewFromLink = link.Replace("preview", "");
                    await ReplyAsync(removePreviewFromLink);
                }
                else
                {
                    await ReplyAsync(link);
                }
            }
            else
            {
                await ReplyAsync("Please only use NSFW commands in an NSFW channel");
            }
        }
    }
}
