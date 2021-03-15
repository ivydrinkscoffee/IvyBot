using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Discord.Commands;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace IvyBot.Modules {
    [Name ("Code Execution")]
    public class ExecuteModule : ModuleBase<SocketCommandContext> {
        public class ScriptGlobals {
            public IvyBotClient client { get; set; }
        }

        private static readonly IvyBotClient _client;

        [RequireOwner]
        [Command ("eval")]
        [Summary ("Runs a C# snippet and sends the result")]
        public async Task ExecuteAsync ([Remainder] string code) {
            IEnumerable<string> refs = new List<string> () { "System", "System.Diagnostics", "System.Collections.Generic", "System.Linq", "System.Net", "System.Net.Http", "System.IO", "System.Threading.Tasks", "System.Xml", "Newtonsoft.Json" };

            var globals = new ScriptGlobals { client = _client };

            var options = ScriptOptions.Default
                .AddReferences (refs)
                .AddImports (refs);

            var text = code.Trim ('`');

            try {
                var script = CSharpScript.Create (text, options, typeof (ScriptGlobals));
                var scriptState = await script.RunAsync (globals);
                var returnValue = scriptState.ReturnValue;

                if (returnValue != null) {
                    await ReplyAsync ($"```cs\n{returnValue.ToString()}\n```");
                } else {
                    await ReplyAsync ("<:xmark:314349398824058880> An unkown error occured while attempting to run the script");
                }
            } catch (Exception ex) {
                await ReplyAsync ($"```cs\n{ex.Message}\n```");
            }
        }
    }
}