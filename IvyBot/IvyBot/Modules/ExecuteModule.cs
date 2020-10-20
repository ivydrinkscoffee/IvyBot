using System;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Discord.Commands;
using System.Threading.Tasks;
using Discord.WebSocket;
using System.Collections.Generic;

namespace IvyBot.Modules
{
    [Name("Code Execution")]
    public class ExecuteModule : ModuleBase<SocketCommandContext>
    {
        public class ScriptGlobals
        {
            public IvyBotClient client { get; internal set; }
        }

        private IvyBotClient _client;

        [Command("eval", RunMode = RunMode.Async)]
        [Summary("Runs a C# sniplet and sends the result")]
        public async Task ExecuteAsync([Remainder] string code)
        {
            var user = Context.User as SocketUser;
            
            IEnumerable<string> metadata = new List<string>() { "System.Collections.Generic", "System.Linq", "System.Net", "System.IO", "Newtonsoft.Json" };
            
            var globals = new ScriptGlobals { client = _client };
            
            var options = ScriptOptions.Default
                .AddReferences(metadata)
                .AddImports("System.Collections.Generic", "System.Linq", "System.Net", "System.IO", "Newtonsoft.Json");
            
            var text = code.Trim('`');

            if (user.Id == 636502606029651998)
            {
                try
                {
                    var script = CSharpScript.Create(text, options, typeof(ScriptGlobals));
                    var scriptState = await script.RunAsync(globals);
                    var returnValue = scriptState.ReturnValue;

                    if (returnValue != null)
                    {
                        await Context.Channel.SendMessageAsync($"```cs\n{returnValue.ToString()}\n```");
                    }
                }
                catch (Exception ex)
                {
                    await Context.Channel.SendMessageAsync($"```cs\n{ex.Message}\n```");
                }
            }
            else
            {
                await ReplyAsync("Only the owner of the bot can use this command");
            }
        }
    }
}