using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using IvyBot.Configuration;

namespace IvyBot.Modules {
    [Name ("Help")]
    public class HelpModule : ModuleBase<SocketCommandContext> {
        private readonly CommandService _service;
        private readonly IConfiguration _config;

        public HelpModule (CommandService service, IConfiguration config) {
            _service = service;
            _config = config;
        }

        [Command ("help")]
        [Summary ("Sends a message with all the available commands that you can use")]
        public async Task HelpAsync () {
            string prefix = _config.GetValueFor (Constants.BotPrefix);
            var builder = new EmbedBuilder () {
                Color = Color.Blue,
                Title = "Commands"
            };

            foreach (var module in _service.Modules) {
                string description = null;
                foreach (var cmd in module.Commands) {
                    var result = await cmd.CheckPreconditionsAsync (Context);
                    if (result.IsSuccess)
                        description += $"`{prefix}{cmd.Aliases.First()}`\n";
                }

                if (!string.IsNullOrWhiteSpace (description)) {
                    builder.AddField (_field => {
                        _field.Name = module.Name;
                        _field.Value = description;
                        _field.IsInline = false;
                    });

                    builder.WithFooter ("Coded and maintained by Ivy#9804 in Discord.Net");
                    builder.WithCurrentTimestamp ();
                }
            }

            await ReplyAsync ("", false, builder.Build ());
        }

        [Command ("command")]
        [Summary ("Search for help on a specific command")]
        public async Task SearchCommandAsync ([Remainder] string command) {
            var result = _service.Search (Context, command);

            if (!result.IsSuccess) {
                await ReplyAsync ($"A command like **{command}** was not found");
                return;
            }

            var builder = new EmbedBuilder () {
                Color = Color.Blue
            };

            foreach (var match in result.Commands) {
                var cmd = match.Command;

                builder.AddField (_field => {
                    _field.Name = $"Command(s): {string.Join(", ", cmd.Aliases)}";
                    _field.Value = $"Description: {cmd.Summary}\nParameters: {string.Join(", ", cmd.Parameters.Select(p => p.Name))}";
                    _field.IsInline = false;
                });

                builder.WithCurrentTimestamp ();
            }

            await ReplyAsync ("", false, builder.Build ());
        }
    }
}