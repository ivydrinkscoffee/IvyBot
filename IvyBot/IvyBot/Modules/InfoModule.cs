using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace IvyBot.Modules
{
    [Name("Information")]
    public class InfoModule : ModuleBase<SocketCommandContext>
    {
        [Command("whois")]
        [Summary("Returns the information of the specified user")]
        public async Task UserInfoAsync(SocketUser user = null)
        {
            var userInfo = user ?? Context.Client.CurrentUser;
            var avatar = userInfo.GetAvatarUrl(ImageFormat.Auto);
            var defaultAvatar = userInfo.GetDefaultAvatarUrl();

            EmbedBuilder builder = new EmbedBuilder();
            
            builder.AddField("Username", $"{userInfo.Username}#{userInfo.Discriminator}", true);
            builder.AddField("Mention", $"{userInfo.Mention}", true);
            builder.AddField("Bot", $"{userInfo.IsBot}", true);
            builder.AddField("Webhook", $"{userInfo.IsWebhook}", true);
            builder.AddField("Account Creation Date", $"{userInfo.CreatedAt}", true);
            
            if (avatar == null)
            {
                builder.WithThumbnailUrl(defaultAvatar);
            }
            else
            {
                builder.WithThumbnailUrl(avatar);
            }
            
            builder.WithCurrentTimestamp();
            builder.WithFooter("Coded and maintained by Ivy#9804 in Discord.NET");
            builder.WithColor(Color.Blue);

            await Context.Channel.SendMessageAsync("", false, builder.Build());
        }

        [Command("avatar")]
        [Summary("Returns the avatar of the specified user")]
        public async Task AvatarAsync(SocketUser user = null)
        {
            var userInfo = user ?? Context.Client.CurrentUser;
            var avatar = userInfo.GetAvatarUrl(ImageFormat.Auto, size: 1024);
            var defaultAvatar = userInfo.GetDefaultAvatarUrl();

            if (avatar == null)
            {
                await Context.Channel.SendMessageAsync(defaultAvatar);
            }
            else
            {
                await Context.Channel.SendMessageAsync(avatar);
            }
        }

        [Command("membercount")]
        [Summary("Returns the amount of users in the server")]
        public async Task GetUserCount()
        {
            await Context.Channel.SendMessageAsync($"**{Context.Guild.MemberCount}** members");
        }

        [Command("serverinfo")]
        [Summary("Returns the information of the server")]
        public async Task ServerInfoAsync()
        {
            EmbedBuilder builder = new EmbedBuilder();

            builder.WithTitle($"{Context.Guild.Name}");
            builder.AddField("Member Count", $"{Context.Guild.MemberCount}", true);
            builder.AddField("Owner", $"{Context.Guild.Owner}", true);
            builder.AddField("Server Creation Date", $"{Context.Guild.CreatedAt}", true);
            builder.AddField("Region", $"{Context.Guild.VoiceRegionId}", true);
            builder.AddField("Verification Level", $"{Context.Guild.VerificationLevel}", true);
            builder.AddField("Content Filter", $"{Context.Guild.ExplicitContentFilter}", true);
            builder.AddField("2FA", $"{Context.Guild.MfaLevel}", true);
            builder.WithThumbnailUrl($"{Context.Guild.IconUrl}");
            builder.WithCurrentTimestamp();
            builder.WithFooter("Coded and maintained by Ivy#9804 in Discord.NET");
            builder.WithColor(Color.Blue);

            await Context.Channel.SendMessageAsync("", false, builder.Build());
        }
    }
}
