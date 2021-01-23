using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Linq;
using System.Threading.Tasks;

namespace IvyBot.Modules
{
    [Name("Moderation")]
    public class ModerationModule : ModuleBase<SocketCommandContext>
    {
        [RequireUserPermission(GuildPermission.KickMembers)]
        [RequireBotPermission(GuildPermission.KickMembers)]
        [Command("kick")]
        [Summary("Kicks the specified user")]
        public async Task KickAsync(IGuildUser user, [Remainder] string reason = null)
        {
            await user.KickAsync(reason);

            var check = new Emoji("✅");
            await Context.Message.AddReactionAsync(check);
        }

        [RequireUserPermission(GuildPermission.KickMembers)]
        [RequireBotPermission(GuildPermission.KickMembers)]
        [Command("prune")]
        [Summary("Kicks users who have not been active for the amount of days specified")]
        public async Task PruneAsync(int days)
        {
            await Context.Guild.PruneUsersAsync(days);

            var check = new Emoji("✅");
            await Context.Message.AddReactionAsync(check);
        }

        [RequireUserPermission(GuildPermission.BanMembers)]
        [RequireBotPermission(GuildPermission.BanMembers)]
        [Command("ban")]
        [Summary("Bans the specified user")]
        public async Task BanAsync(IGuildUser user, [Remainder] string reason = null)
        {
            await Context.Guild.AddBanAsync(user, 0, reason);

            var check = new Emoji("✅");
            await Context.Message.AddReactionAsync(check);
        }

        [RequireUserPermission(GuildPermission.BanMembers)]
        [RequireBotPermission(GuildPermission.BanMembers)]
        [Command("unban")]
        [Summary("Unbans the specified user from their user ID")]
        public async Task UnbanAsync(ulong userId)
        {
            await Context.Guild.RemoveBanAsync(userId);

            var check = new Emoji("✅");
            await Context.Message.AddReactionAsync(check);
        }

        [Command("bam")]
        [Summary("B̴̥̆̍̎̄̉͒͂̐͐́͐͘̕͝ầ̸̠̟̞͚̞̥̟͊͒̍̓̒̉̀͘̕n̴̛̪̓̎̐s̷͓͉̫̱̳̹͇̟̜̜̄͛̒̿͌̓̔͑̽́̕̚ ̶̘̥̭̜́̇t̶͚͚̀̋́̚͠h̸͇͚̭͌́̂͑͌͒̅̏͌͐̏͗̊e̸̡͈̲̫̯͑̄͊̾̎͗̀̕̚͝ ̸̜̱́̏s̶̛̛̫̻̣̞͂̂̆̐̎̊̓͛͊̈́͑̐̕̕͠p̸̡̩͇͙̤̺͓̯͇̻͖͓̠͔̾̿̋̄͐̌̓̇̽̿̓͊́̽̊̅e̷̜̹̎̃̊̆̓̈́͂̀̿̈́͋͑̂̒̚ć̵͓̣̃̀͊̾͋͑̀̍̔͘î̶̡̛̼̞̖̀̑̏f̸̛̝͛̆̔͌̂̈́̆͗͂̂͠i̸̢̩͕̬̻̖̦̝̺̒̊̌̿̃̃̾̋́̽̈́̅̄̒̀͠ë̴̬̹͌͗̎̈͠d̶͖͂͋̏̾̀͌̏̆́̔̌́͘ ̶̡̨̠̙̹̙̥͚̱̙̹͇́̈́̔͗̿́͘̕͜u̸̧̼̣͖͔͉̰̰̳͍͈̯̗̮̬̩̥̔̍́͒̌͑͛͋s̶̪̜̥̖͈̜̠̻͓̦͖̤̆ę̴͙̪̝̞̮̉̏̆̇͐͂̆͑̔̓̃͌͘͜ͅr̷̛̞̘̗̲͈̙̾̏͊́̌̕̕")]
        public async Task BamAsync(SocketUser user = null)
        {
            var userInfo = user ?? Context.User;
            await ReplyAsync($"<:authorized:585790083161128980> **{userInfo.Mention}** i̶̧̫͙̼̤̥͑͜ṥ̸͕̳͉̈́̍̀ ̴̧̧̛̣̥̞͔̥̞̞͕͕̠̝̲̖̟̓͐͂̑̌̎̚͝ṇ̵̝̬̥̠̮̩͚̗͍͇͇͊̏̈́o̸̲̼͕͓̼̜̦͍͔̙͕͈̊̈́̅̋̆͒̅͌̅́̚͝͝͝͝ͅw̷͕͚͍̮̹̪̮͖̞̠̼͓̯̫̓͜ ̶̧̫̣̟̼̻͚̞̹̯̉̃̑͑b̷͎̞̹̘̣̫͖͔̰̘͕̪̲̭͓͕͌͆ͅä̶͉͖͖̥̮̭͔̖̒̊̊͆̋̌̿̃̿͂̍͋̍͆̍n̶̥̓̒̏͑͂̉͋̃͛̐͋͝͠͝n̵̩̺͖̦̥̆͆̆̑̄͆̔̃̀̃̕͝ē̵͙̬͚̝̭͑̃̓̀́̾͠ḋ̵̡͎̖̻͕͖̮̜̖̗̠͙͔̻̰̰̮̓͂̈̇̉̓̚");
        }

        [RequireUserPermission(GuildPermission.ManageRoles)]
        [RequireBotPermission(GuildPermission.ManageRoles)]
        [Command("mute")]
        [Summary("Mutes the specified user, disallowing them from sending messages anywhere in the server")]
        public async Task MuteAsync(SocketGuildUser user)
        {
            GuildPermissions mutedPerms = new GuildPermissions(sendMessages: false);
            
            IRole mutedRole = user.Guild.Roles.Where(r => r.Name == "Muted").FirstOrDefault() ?? await user.Guild.CreateRoleAsync("Muted", mutedPerms, null, false, null) as IRole;
            await user.AddRoleAsync(mutedRole);

            var check = new Emoji("✅");
            await Context.Message.AddReactionAsync(check);
        }

        [RequireUserPermission(GuildPermission.ManageRoles)]
        [RequireBotPermission(GuildPermission.ManageRoles)]
        [Command("unmute")]
        [Summary("Undoes the 'mute' command, allowing the specified user to send messages again")]
        public async Task UnmuteAsync(SocketGuildUser user)
        {
            var mutedRole = user.Roles.Where(r => r.Name.Equals("Muted"));
            await user.RemoveRolesAsync(mutedRole);

            var check = new Emoji("✅");
            await Context.Message.AddReactionAsync(check);
        }
    }
}
