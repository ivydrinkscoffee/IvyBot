using Discord;
using Discord.Commands;
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
        public async Task KickAsync(IGuildUser user)
        {
            var Check = new Emoji("✅");
            await Context.Message.AddReactionAsync(Check);
            await user.KickAsync();
        }

        [RequireUserPermission(GuildPermission.KickMembers)]
        [RequireBotPermission(GuildPermission.KickMembers)]
        [Command("prune", RunMode = RunMode.Async)]
        [Summary("Kicks users who have not been active for the amount of days specified")]
        public Task PruneAsync(int days)
        {
            var Check = new Emoji("✅");
            Context.Message.AddReactionAsync(Check);
            return Context.Guild.PruneUsersAsync(days);
        }

        [RequireUserPermission(GuildPermission.BanMembers)]
        [RequireBotPermission(GuildPermission.BanMembers)]
        [Command("ban", RunMode = RunMode.Async)]
        [Summary("Bans the specified user")]
        public Task BanAsync(IGuildUser user)
        {
            var Check = new Emoji("✅");
            Context.Message.AddReactionAsync(Check);
            return Context.Guild.AddBanAsync(user);
        }

        [RequireUserPermission(GuildPermission.BanMembers)]
        [RequireBotPermission(GuildPermission.BanMembers)]
        [Command("unban", RunMode = RunMode.Async)]
        [Summary("Unbans the specified user")]
        public Task UnbanAsync(ulong userId)
        {
            var Check = new Emoji("✅");
            Context.Message.AddReactionAsync(Check);
            return Context.Guild.RemoveBanAsync(userId);
        }
    }
}