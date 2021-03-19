using System.Threading.Tasks;
using Discord.Commands;
using IvyBot.Services;

namespace IvyBot.Modules {
    [Name ("NSFW")]
    public class NSFWModule : ModuleBase<SocketCommandContext> {
        [RequireNsfw]
        [Command ("rule34")]
        [Summary ("Gets images or videos from rule34.xxx specified by the entered text")]
        public async Task Rule34Async ([Remainder] string tag) {
            await ReplyAsync (await NSFWService.GetRule34File (tag));
        }

        [RequireNsfw]
        [Command ("e621")]
        [Summary ("Gets images or videos from e621.net specified by the entered text")]
        public async Task E621Async ([Remainder] string tag) {
            var link = await NSFWService.GetE621File (tag);

            while (link.Contains ("preview") == false) {
                await ReplyAsync (link);
                break;
            }
        }
    }
}