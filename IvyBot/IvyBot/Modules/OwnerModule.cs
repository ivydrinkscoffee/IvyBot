using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using IvyBot.Configuration;

namespace IvyBot.Modules {
    [Name ("Owner")]
    public class OwnerModule : ModuleBase<SocketCommandContext> {
        [RequireOwner]
        [Command ("changeprefix")]
        [Summary ("Modifies the prefix of the bot in all servers")]
        public async Task UpdatePrefixAsync ([Remainder] string prefix) {
            ConfigManager configManager = new ConfigManager ();
            configManager.SetValueFor (configManager.GetValueFor (Constants.BotPrefix), prefix);

            var check = new Emoji ("✅");
            await Context.Message.AddReactionAsync (check);
        }
    }
}