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
        [Command("base64decode", RunMode = RunMode.Sync)]
        [Summary("Decode Base64")]
        public async Task DecodeBase64(string base64)
        {
            string json = new WebClient().DownloadString("https://some-random-api.ml/base64?decode=" + base64);

            Dictionary<string, string> items = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            await Context.Channel.SendMessageAsync(items["text"]);
        }

        [Command("base64encode", RunMode = RunMode.Sync)]
        [Summary("Encode Base64")]
        public async Task EncodeBase64(string text)
        {
            string json = new WebClient().DownloadString("https://some-random-api.ml/base64?encode=" + text.Replace(" ", "%20"));

            Dictionary<string, string> items = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            await Context.Channel.SendMessageAsync(items["base64"]);
        }

        [Command("binarydecode", RunMode = RunMode.Sync)]
        [Summary("Decode Binary")]
        public async Task DecodeBinary(string binary)
        {
            string json = new WebClient().DownloadString("https://some-random-api.ml/binary?decode=" + binary);

            Dictionary<string, string> items = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            await Context.Channel.SendMessageAsync(items["text"]);
        }

        [Command("binaryencode", RunMode = RunMode.Sync)]
        [Summary("Encode Binary")]
        public async Task EncodeBinary(string text)
        {
            string json = new WebClient().DownloadString("https://some-random-api.ml/binary?text=" + text.Replace(" ", "%20"));
            
            Dictionary<string, string> items = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            await Context.Channel.SendMessageAsync(items["binary"]);
        }
    }
}