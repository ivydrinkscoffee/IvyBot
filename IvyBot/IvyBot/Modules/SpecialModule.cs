using Discord.Commands;
using System.IO;
using System.Threading.Tasks;
using System.Net;

namespace IvyBot.Modules
{
    [Name("Special")]
    public class SpecialModule : ModuleBase<SocketCommandContext>
    {
        [Command("530public", RunMode = RunMode.Async)]
        [Summary("Sends the latest pchtxt for Splatoon 2")]
        public async Task SendPatchesAsync()
        {
            var filestream = WebRequest.Create("https://raw.githubusercontent.com/CrustySean/CrustyMods/master/5.3.0public.pchtxt");
            Stream stream = filestream.GetResponse().GetResponseStream();
            await Context.Channel.SendFileAsync(stream, "5.3.0public.pchtxt");
        }

        [Command("color", RunMode = RunMode.Async)]
        [Summary("Sends an image of the color you have requested in hex")]
        public async Task ColorViewer(string hex)
        {
            await Context.Channel.SendMessageAsync("https://some-random-api.ml/canvas/colorviewer?hex=" + hex);
        }
    }
}
