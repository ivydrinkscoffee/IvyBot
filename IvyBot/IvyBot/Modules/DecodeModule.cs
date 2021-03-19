using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Discord.Commands;
using Newtonsoft.Json;

namespace IvyBot.Modules {
    [Name ("Decode/Encode")]
    public class DecodeModule : ModuleBase<SocketCommandContext> {
        [Command ("base64decode")]
        [Summary ("Decode the entered Base64")]
        public async Task DecodeBase64Async ([Remainder] string base64) {
            string json = new WebClient ().DownloadString ("https://some-random-api.ml/base64?decode=" + base64);

            Dictionary<string, string> items = JsonConvert.DeserializeObject<Dictionary<string, string>> (json);

            await ReplyAsync (items["text"]);
        }

        [Command ("base64encode")]
        [Summary ("Encode the entered text into Base64")]
        public async Task EncodeBase64Async ([Remainder] string text) {
            string json = new WebClient ().DownloadString ("https://some-random-api.ml/base64?encode=" + text.Replace (" ", "%20"));

            Dictionary<string, string> items = JsonConvert.DeserializeObject<Dictionary<string, string>> (json);

            await ReplyAsync (items["base64"]);
        }

        [Command ("binarydecode")]
        [Summary ("Decode the entered Binary")]
        public async Task DecodeBinaryAsync ([Remainder] string binary) {
            string json = new WebClient ().DownloadString ("https://some-random-api.ml/binary?decode=" + binary);

            Dictionary<string, string> items = JsonConvert.DeserializeObject<Dictionary<string, string>> (json);

            await ReplyAsync (items["text"]);
        }

        [Command ("binaryencode")]
        [Summary ("Encode the entered text into Binary")]
        public async Task EncodeBinaryAsync ([Remainder] string text) {
            string json = new WebClient ().DownloadString ("https://some-random-api.ml/binary?text=" + text.Replace (" ", "%20"));

            Dictionary<string, string> items = JsonConvert.DeserializeObject<Dictionary<string, string>> (json);

            await ReplyAsync (items["binary"]);
        }
    }
}