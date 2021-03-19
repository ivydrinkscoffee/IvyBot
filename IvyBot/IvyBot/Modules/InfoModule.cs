using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace IvyBot.Modules {
    [Name ("Information")]
    public class InfoModule : ModuleBase<SocketCommandContext> {
        [Command ("whois")]
        [Summary ("Returns the information of the specified user")]
        public async Task UserInfoAsync (SocketUser user = null) {
            var userInfo = user ?? Context.User;
            var avatar = userInfo.GetAvatarUrl (ImageFormat.Auto) ?? userInfo.GetDefaultAvatarUrl ();

            EmbedBuilder builder = new EmbedBuilder ();

            builder.AddField ("Username", userInfo.ToString (), true);
            builder.AddField ("Mention", userInfo.Mention, true);
            builder.AddField ("Bot", userInfo.IsBot, true);
            builder.AddField ("Webhook", userInfo.IsWebhook, true);
            builder.AddField ("Account Creation Date", userInfo.CreatedAt, true);
            builder.WithThumbnailUrl (avatar);
            builder.WithCurrentTimestamp ();
            builder.WithFooter ("Coded and maintained by Ivy#9804 in Discord.Net");
            builder.WithColor (Color.Blue);

            await ReplyAsync (null, false, builder.Build ());
        }

        [Command ("avatar")]
        [Summary ("Returns the avatar of the specified user")]
        public async Task AvatarAsync (SocketUser user = null) {
            var userInfo = user ?? Context.User;
            var avatar = userInfo.GetAvatarUrl (ImageFormat.Auto) ?? userInfo.GetDefaultAvatarUrl ();

            await ReplyAsync (avatar);
        }

        [Command ("membercount")]
        [Summary ("Returns the amount of users in the server")]
        public async Task GetUserCountAsync () {
            await ReplyAsync ($"**{Context.Guild.MemberCount}** members");
        }

        [Command ("serverinfo")]
        [Summary ("Returns the information of the server")]
        public async Task ServerInfoAsync () {
            EmbedBuilder builder = new EmbedBuilder ();

            builder.WithTitle (Context.Guild.Name);
            builder.AddField ("Member Count", Context.Guild.MemberCount, true);
            builder.AddField ("Owner", Context.Guild.Owner, true);
            builder.AddField ("Server Creation Date", Context.Guild.CreatedAt, true);
            builder.AddField ("Region", Context.Guild.VoiceRegionId, true);
            builder.AddField ("Verification Level", Context.Guild.VerificationLevel, true);
            builder.AddField ("Content Filter", Context.Guild.ExplicitContentFilter, true);
            builder.AddField ("2FA", Context.Guild.MfaLevel, true);
            builder.WithThumbnailUrl (Context.Guild.IconUrl);
            builder.WithCurrentTimestamp ();
            builder.WithFooter ("Coded and maintained by Ivy#9804 in Discord.Net");
            builder.WithColor (Color.Blue);

            await ReplyAsync (null, false, builder.Build ());
        }
    }
}