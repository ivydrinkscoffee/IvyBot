using Discord.Commands;
using System.IO;
using System.Threading.Tasks;
using System.Net;
using System;

namespace IvyBot.Modules
{
    [Name("Special")]
    public class SpecialModule : ModuleBase<SocketCommandContext>
    {
        [Command("longelmo")]
        [Summary("This elmo is long")]
        public async Task LongElmoAsync()
        {
            var filestream = WebRequest.Create("https://cdn.discordapp.com/attachments/762004355888185365/767022764916736020/video0.mp4");
            Stream stream = filestream.GetResponse().GetResponseStream();
            await Context.Channel.SendFileAsync(stream, "superstrong.mp4", "https://twitter.com/intent/tweet?hashtags=LongElmo2020%2CMakeAmericaLongAgain");
        }
        
        [Command("asm")]
        [Summary("Converts ARM64 assembly code to hex code")]
        public Task AssembleAsync([Remainder] string assembly)
        {  
            try
            {
                var client = new WebClient();
                
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                string json = @"{""asm"":""" + $"{assembly}" + @"""," + @"""offset"":""0x100"",""arch"":""arm64""}";
                
                string result = client.UploadString("https://armconverter.com/api/convert", "POST", json);
                
                return ReplyAsync(result);
            }
            catch (Exception ex)
            {
                return ReplyAsync(ex.Message);
            }
        }

        [Command("disasm")]
        [Summary("Converts ARM64 hex code to assembly code")]
        public Task DisassembleAsync([Remainder] string hex)
        {
            try
            {
                var client = new WebClient();
                
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                string json = @"{""hex"":""" + $"{hex}" + @"""," + @"""offset"":""0x100"",""arch"":""arm64""}";
                
                string result = client.UploadString("https://armconverter.com/api/convert", "POST", json);
                
                return ReplyAsync(result);
            }
            catch (Exception ex)
            {
                return ReplyAsync(ex.Message);
            }
        }
        
        [Command("531public")]
        [Summary("Sends the latest pchtxt for Splatoon 2")]
        public async Task SendPatchesAsync()
        {
            var filestream = WebRequest.Create("https://splatoon-hackers.github.io/5.3.1public.pchtxt");
            Stream stream = filestream.GetResponse().GetResponseStream();
            await Context.Channel.SendFileAsync(stream, "5.3.1public.pchtxt");
        }

        [Command("310starlion")]
        [Summary("Sends the latest public Starlion for Splatoon 2")]
        public async Task SendStarlionAsync()
        {
            var filestream = WebRequest.Create("https://splatoon-hackers.github.io/starlion_public.rar");
            Stream stream = filestream.GetResponse().GetResponseStream();
            await Context.Channel.SendFileAsync(stream, "starlion_public.rar");
        }
    }
}
