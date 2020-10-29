using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace IvyBot.Modules
{
    [Name("Moderation")]
    public class ModerationModule : ModuleBase<SocketCommandContext>
    {
        [RequireUserPermission(GuildPermission.KickMembers)]
        [RequireBotPermission(GuildPermission.KickMembers)]
        [Command("kick", RunMode = RunMode.Async)]
        [Summary("Kicks the specified user")]
        public async Task KickAsync(IGuildUser user, [Remainder] string reason = null)
        {
            var check = new Emoji("✅");
            await Context.Message.AddReactionAsync(check);
            await user.KickAsync(reason);
        }

        [RequireUserPermission(GuildPermission.KickMembers)]
        [RequireBotPermission(GuildPermission.KickMembers)]
        [Command("prune", RunMode = RunMode.Async)]
        [Summary("Kicks users who have not been active for the amount of days specified")]
        public Task PruneAsync(int days)
        {
            var check = new Emoji("✅");
            Context.Message.AddReactionAsync(check);
            return Context.Guild.PruneUsersAsync(days);
        }

        [RequireUserPermission(GuildPermission.BanMembers)]
        [RequireBotPermission(GuildPermission.BanMembers)]
        [Command("ban", RunMode = RunMode.Async)]
        [Summary("Bans the specified user")]
        public Task BanAsync(IGuildUser user, [Remainder] string reason = null)
        {
            var check = new Emoji("✅");
            Context.Message.AddReactionAsync(check);
            return Context.Guild.AddBanAsync(user, 0, reason);
        }

        [RequireUserPermission(GuildPermission.BanMembers)]
        [RequireBotPermission(GuildPermission.BanMembers)]
        [Command("unban", RunMode = RunMode.Async)]
        [Summary("Unbans the specified user")]
        public Task UnbanAsync(ulong userId)
        {
            var check = new Emoji("✅");
            Context.Message.AddReactionAsync(check);
            return Context.Guild.RemoveBanAsync(userId);
        }

        [Command("bam", RunMode = RunMode.Async)]
        [Summary("B̴̥̆̍̎̄̉͒͂̐͐́͐͘̕͝ầ̸̠̟̞͚̞̥̟͊͒̍̓̒̉̀͘̕n̴̛̪̓̎̐s̷͓͉̫̱̳̹͇̟̜̜̄͛̒̿͌̓̔͑̽́̕̚ ̶̘̥̭̜́̇t̶͚͚̀̋́̚͠h̸͇͚̭͌́̂͑͌͒̅̏͌͐̏͗̊e̸̡͈̲̫̯͑̄͊̾̎͗̀̕̚͝ ̸̜̱́̏s̶̛̛̫̻̣̞͂̂̆̐̎̊̓͛͊̈́͑̐̕̕͠p̸̡̩͇͙̤̺͓̯͇̻͖͓̠͔̾̿̋̄͐̌̓̇̽̿̓͊́̽̊̅e̷̜̹̎̃̊̆̓̈́͂̀̿̈́͋͑̂̒̚ć̵͓̣̃̀͊̾͋͑̀̍̔͘î̶̡̛̼̞̖̀̑̏f̸̛̝͛̆̔͌̂̈́̆͗͂̂͠i̸̢̩͕̬̻̖̦̝̺̒̊̌̿̃̃̾̋́̽̈́̅̄̒̀͠ë̴̬̹͌͗̎̈͠d̶͖͂͋̏̾̀͌̏̆́̔̌́͘ ̶̡̨̠̙̹̙̥͚̱̙̹͇́̈́̔͗̿́͘̕͜u̸̧̼̣͖͔͉̰̰̳͍͈̯̗̮̬̩̥̔̍́͒̌͑͛͋s̶̪̜̥̖͈̜̠̻͓̦͖̤̆ę̴͙̪̝̞̮̉̏̆̇͐͂̆͑̔̓̃͌͘͜ͅr̷̛̞̘̗̲͈̙̾̏͊́̌̕̕")]
        public async Task BamAsync(SocketUser user = null)
        {
            var userInfo = user ?? Context.Client.CurrentUser;
            await ReplyAsync($"**{userInfo.Mention}** i̶̧̫͙̼̤̥͑͜ṥ̸͕̳͉̈́̍̀ ̴̧̧̛̣̥̞͔̥̞̞͕͕̠̝̲̖̟̓͐͂̑̌̎̚͝ṇ̵̝̬̥̠̮̩͚̗͍͇͇͊̏̈́o̸̲̼͕͓̼̜̦͍͔̙͕͈̊̈́̅̋̆͒̅͌̅́̚͝͝͝͝ͅw̷͕͚͍̮̹̪̮͖̞̠̼͓̯̫̓͜ ̶̧̫̣̟̼̻͚̞̹̯̉̃̑͑b̷͎̞̹̘̣̫͖͔̰̘͕̪̲̭͓͕͌͆ͅä̶͉͖͖̥̮̭͔̖̒̊̊͆̋̌̿̃̿͂̍͋̍͆̍n̶̥̓̒̏͑͂̉͋̃͛̐͋͝͠͝n̵̩̺͖̦̥̆͆̆̑̄͆̔̃̀̃̕͝ē̵͙̬͚̝̭͑̃̓̀́̾͠ḋ̵̡͎̖̻͕͖̮̜̖̗̠͙͔̻̰̰̮̓͂̈̇̉̓̚");
        }
    }
}
