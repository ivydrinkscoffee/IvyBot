using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using ArmConverter;
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
            string hex = Assembler.Assemble (assembly);

            await ReplyAsync (hex);
        }

        [Command ("disasm")]
        [Summary ("Converts ARM64 hex code to assembly code")]
        public async Task DisassembleAsync ([Remainder] string hex) {
            string asm = Disassembler.Disassemble (hex);

            await ReplyAsync ($"```armasm\n{asm}\n```");
        }

        [Command ("pchtxt")]
        [Summary ("Sends the specified pchtxt for Splatoon 2")]
        public async Task SendPatchesAsync ([Remainder] string version) {
            IEnumerable<string> versionList = new List<string> () { "5.0.0", "5.0.1", "5.1.0", "5.2.0", "5.2.1", "5.2.2", "5.3.0", "5.3.1", "5.4.0" };

            if (version.EqualsAny (versionList) == false) {
                await ReplyAsync ("Game version not supported");
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