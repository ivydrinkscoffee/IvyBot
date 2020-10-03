using Discord;
using Discord.Commands;
using System.Linq;
using System.Threading.Tasks;

namespace IvyBot.Modules
{
    [Name("Help")]
    public class HelpModule : ModuleBase<SocketCommandContext>
    {
        private readonly CommandService _service;

        public HelpModule(CommandService service)
        {
            _service = service;
        }

        [Command("help")]
        [Summary("Sends a message with all the available commands that you can use")]
        public async Task HelpAsync()
        {
            string prefix = ".";
            var builder = new EmbedBuilder()
            {
                Color = Color.Blue
            };
            
            foreach (var module in _service.Modules)
            {
                string description = null;
                foreach (var cmd in module.Commands)
                {
                    var result = await cmd.CheckPreconditionsAsync(Context);
                    if (result.IsSuccess)
                        description += $"{prefix}{cmd.Aliases.First()}\n";
                }
                
                if (!string.IsNullOrWhiteSpace(description))
                {
                    builder.AddField(x =>
                    {
                        x.Name = module.Name;
                        x.Value = description;
                        x.IsInline = false;
                    });

                    builder.WithFooter("Coded and maintained by Ivy#9804 in Discord.NET");
                }
            }

            await ReplyAsync("", false, builder.Build());
        }

        [Command("help")]
        [Summary("Search for help on a specific command")]
        public async Task HelpAsync(string command)
        {
            var result = _service.Search(Context, command);

            if (!result.IsSuccess)
            {
                await ReplyAsync($"A command like **{command}** was not found");
                return;
            }

            var builder = new EmbedBuilder()
            {
                Color = Color.Blue
            };

            foreach (var match in result.Commands)
            {
                var cmd = match.Command;

                builder.AddField(x =>
                {
                    x.Name = string.Join(", ", cmd.Aliases);
                    x.Value = $"{cmd.Summary}";
                    x.IsInline = false;
                });

                builder.WithFooter("Coded and maintained by Ivy#9804 in Discord.NET");
            }

            await ReplyAsync("", false, builder.Build());
        }
    }
}