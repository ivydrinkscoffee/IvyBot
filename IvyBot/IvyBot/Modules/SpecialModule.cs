using Discord.Commands;
using System.IO;
using System.Threading.Tasks;
using System.Net;

namespace IvyBot.Modules
{
    [Name("Special")]
    public class SpecialModule : ModuleBase<SocketCommandContext>
    {
        [Command("longelmo", RunMode = RunMode.Async)]
        [Summary("This elmo is long")]
        public async Task LongElmoAsync()
        {
            var filestream = WebRequest.Create("https://cdn.discordapp.com/attachments/762004355888185365/767022764916736020/video0.mp4");
            Stream stream = filestream.GetResponse().GetResponseStream();
            await Context.Channel.SendFileAsync(stream, "superstrong.mp4", "https://twitter.com/intent/tweet?hashtags=LongElmo2020%2CMakeAmericaLongAgain");
        }
        
        [Command("530public", RunMode = RunMode.Async)]
        [Summary("Sends the latest pchtxt for Splatoon 2")]
        public async Task SendPatchesAsync()
        {
            var filestream = WebRequest.Create("https://raw.githubusercontent.com/CrustySean/CrustyMods/master/5.3.0public.pchtxt");
            Stream stream = filestream.GetResponse().GetResponseStream();
            await Context.Channel.SendFileAsync(stream, "5.3.0public.pchtxt");
        }

        [Command("310starlion", RunMode = RunMode.Async)]
        [Summary("Sends the latest public Starlion for Splatoon 2")]
        public async Task SendStarlionAsync()
        {
            var filestream = WebRequest.Create("https://splatoon-hackers.github.io/starlion_public.rar");
            Stream stream = filestream.GetResponse().GetResponseStream();
            await Context.Channel.SendFileAsync(stream, "starlion_public.rar");
        }

        [Command("color", RunMode = RunMode.Async)]
        [Summary("Sends an image of the color you have requested in hex")]
        public async Task ColorViewer([Remainder] string hex)
        {
            await Context.Channel.SendMessageAsync("https://some-random-api.ml/canvas/colorviewer?hex=" + hex);
        }
    }
}
