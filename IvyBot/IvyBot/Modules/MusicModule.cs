using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using IvyBot.Services;

namespace IvyBot.Modules {
    [Name ("Music")]
    public class MusicModule : ModuleBase<SocketCommandContext> {
        public readonly MusicService _musicService;
        private static readonly IEnumerable<int> Range = Enumerable.Range (1900, 2000);

        public MusicModule (MusicService musicService) {
            _musicService = musicService;
        }

        [Command ("join")]
        [Summary ("Joins the voice channel you are currently in")]
        public async Task JoinAsync () {
            var user = Context.User as SocketGuildUser;
            if (user.VoiceChannel is null) {
                await ReplyAsync ("You need to connect to a voice channel");
                return;
            } else {
                await _musicService.ConnectAsync (user.VoiceChannel, Context.Channel as ITextChannel);
                await ReplyAsync ($"Now connected to **{user.VoiceChannel.Name}**");
            }
        }

        [Command ("leave")]
        [Summary ("Leaves the voice channel which you are currently in")]
        public async Task LeaveAsync () {
            var user = Context.User as SocketGuildUser;
            if (user.VoiceChannel is null) {
                await ReplyAsync ("Please join the voice channel I am in to have me leave");
            } else {
                await _musicService.LeaveAsync (user.VoiceChannel);
                await ReplyAsync ($"I have now left **{user.VoiceChannel.Name}**");
            }
        }

        [Command ("play")]
        [Summary ("Plays a track of your choice")]
        public async Task PlayAsync ([Remainder] string query) => await ReplyAsync (await _musicService.PlayAsync (query, Context.Guild.Id));

        [Command ("stop")]
        [Summary ("Stops the player")]
        public async Task StopAsync () => await ReplyAsync (await _musicService.StopAsync (Context.Guild.Id));

        [Command ("skip")]
        [Summary ("Skips the currently playing track")]
        public async Task SkipAsync () => await ReplyAsync (await _musicService.SkipAsync (Context.Guild.Id));

        [Command ("volume")]
        [Summary ("Sets the volume which the track will play at")]
        public async Task VolumeAsync (ushort volume) => await ReplyAsync (await _musicService.SetVolumeAsync (volume, Context.Guild.Id));

        [Command ("pause")]
        [Summary ("Pauses the player")]
        public async Task PauseAsync () => await ReplyAsync (await _musicService.PauseOrResumeAsync (Context.Guild.Id));

        [Command ("resume")]
        [Summary ("Resumes playback")]
        public async Task ResumeAsync () => await ReplyAsync (await _musicService.ResumeAsync (Context.Guild.Id));

        [Command ("lyrics")]
        [Summary ("Gets the lyrics for the currently playing song")]
        public async Task GetCurrentLyricsAsync () => await ReplyAsync (await _musicService.GetCurrentLyricsAsync (Context.Guild.Id));

        [Command ("lyrics")]
        [Summary ("Gets the lyrics for the specified song")]
        public async Task GetLyricsAsync ([Remainder] string title) {
            string json = new WebClient ().DownloadString ("https://some-random-api.ml/lyrics?title=" + title);
            JsonService.Lyrics.Json obj = JsonService.Lyrics.Json.FromJson (json);

            /*
            EmbedBuilder builder = new EmbedBuilder ();

            builder.WithTitle (obj.Title);
            builder.WithUrl (obj.Links.Genius.ToString ());
            builder.AddField ("Artist", obj.Author);
            builder.WithImageUrl (obj.Thumbnail.Genius.ToString ());
            builder.WithCurrentTimestamp ();
            builder.WithFooter ("Coded and maintained by Ivy#9804 in Discord.Net");
            builder.WithColor (Color.Blue);
            */

            var splitLyrics = obj.Lyrics.Split ('\n');
            var stringBuilder = new StringBuilder ();

            foreach (var line in splitLyrics) {
                if (Range.Contains (stringBuilder.Length)) {
                    await ReplyAsync ($"```{stringBuilder}```");
                    stringBuilder.Clear ();
                } else {
                    stringBuilder.AppendLine (line);
                }
            }

            // await Context.User.SendMessageAsync (null, false, builder.Build ());
            await ReplyAsync ($"```{stringBuilder}```", true);
        }
    }
}