using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Discord.Commands;
using IvyBot.Services;

namespace IvyBot.Modules {
    [Name ("Special")]
    public class SpecialModule : ModuleBase<SocketCommandContext> {
        [Command ("longelmo")]
        [Summary ("This elmo is long")]
        public async Task LongElmoAsync () {
            var filestream = WebRequest.Create ("https://cdn.discordapp.com/attachments/762004355888185365/767022764916736020/video0.mp4");
            Stream stream = filestream.GetResponse ().GetResponseStream ();
            await Context.Channel.SendFileAsync (stream, "superstrong.mp4", "https://twitter.com/intent/tweet?hashtags=LongElmo2020%2CMakeAmericaLongAgain");
        }

        [Command ("asm")]
        [Summary ("Converts ARM64 assembly code to hex code")]
        public async Task AssembleAsync ([Remainder] string assembly) {
            try {
                var client = new WebClient ();

                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                string json = @"{""asm"":""" + $"{assembly}" + @"""," + @"""offset"":"""",""arch"":""arm64""}";

                string result = client.UploadString ("https://armconverter.com/api/convert", json);

                JsonService.Assembly.Json obj = JsonService.Assembly.Json.FromJson (result);
                string hex = obj.Hex.Arm64[0].String.Replace ("### ", " ");

                await ReplyAsync (hex);
            } catch (Exception) {
                await ReplyAsync ("Invalid assembly code");
            }
        }

        [Command ("disasm")]
        [Summary ("Converts ARM64 hex code to assembly code")]
        public async Task DisassembleAsync ([Remainder] string hex) {
            try {
                var client = new WebClient ();

                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                string json = @"{""hex"":""" + $"{hex}" + @"""," + @"""offset"":"""",""arch"":""arm64""}";

                string result = client.UploadString ("https://armconverter.com/api/convert", json);

                JsonService.Hex.Json obj = JsonService.Hex.Json.FromJson (result);
                string asm = obj.Asm.Arm64[0].String.Replace ("### ", " ");

                await ReplyAsync (asm);
            } catch (Exception) {
                await ReplyAsync ("Invalid hex code");
            }
        }

        [Command ("pchtxt")]
        [Summary ("Sends the specified pchtxt for Splatoon 2")]
        public async Task SendPatchesAsync ([Remainder] string version) {
            IEnumerable<string> versionList = new List<string> () { "5.0.0", "5.0.1", "5.1.0", "5.2.0", "5.2.1", "5.2.2", "5.3.0", "5.3.1" };

            if (StringService.EqualsAny (version, versionList) == false) {
                await ReplyAsync ("<:xmark:314349398824058880> Game version not supported");
            } else {
                var filestream = WebRequest.Create ($"https://splatoon-hackers.github.io/assets/pchtxt/{version}public.pchtxt");
                Stream stream = filestream.GetResponse ().GetResponseStream ();
                await Context.Channel.SendFileAsync (stream, $"{version}public.pchtxt");
            }
        }

        [Command ("starlion")]
        [Summary ("Sends the latest public Starlion for Splatoon 2")]
        public async Task SendStarlionAsync () {
            var filestream = WebRequest.Create ("https://splatoon-hackers.github.io/assets/misc/starlion-injector-gui.rar");
            Stream stream = filestream.GetResponse ().GetResponseStream ();
            await Context.Channel.SendFileAsync (stream, "starlion-injector-gui.rar");
        }
    }
}