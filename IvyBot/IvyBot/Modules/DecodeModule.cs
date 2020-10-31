using Discord.Commands;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IvyBot.Modules
{
    [Name("Decode/Encode")]
    public class DecodeModule : ModuleBase<SocketCommandContext>
    {
        [Command("base64decode")]
        [Summary("Decode the entered Base64")]
        public async Task DecodeBase64([Remainder] string base64)
        {
            string json = new WebClient().DownloadString("https://some-random-api.ml/base64?decode=" + base64);

            Dictionary<string, string> items = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            await Context.Channel.SendMessageAsync(items["text"]);
        }

        [Command("base64encode")]
        [Summary("Encode the entered text into Base64")]
        public async Task EncodeBase64([Remainder] string text)
        {
            string json = new WebClient().DownloadString("https://some-random-api.ml/base64?encode=" + text.Replace(" ", "%20"));

            Dictionary<string, string> items = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            await Context.Channel.SendMessageAsync(items["base64"]);
        }

        [Command("binarydecode")]
        [Summary("Decode the entered Binary")]
        public async Task DecodeBinary([Remainder] string binary)
        {
            string json = new WebClient().DownloadString("https://some-random-api.ml/binary?decode=" + binary);

            Dictionary<string, string> items = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            await Context.Channel.SendMessageAsync(items["text"]);
        }

        [Command("binaryencode")]
        [Summary("Encode the entered text into Binary")]
        public async Task EncodeBinary([Remainder] string text)
        {
            string json = new WebClient().DownloadString("https://some-random-api.ml/binary?text=" + text.Replace(" ", "%20"));
            
            Dictionary<string, string> items = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            await Context.Channel.SendMessageAsync(items["binary"]);
        }
    }
}