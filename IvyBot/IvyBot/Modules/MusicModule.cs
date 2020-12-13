/*
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using IvyBot.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordBot.Modules
{
    [Name("Music")]
    public class MusicModule : ModuleBase<SocketCommandContext>
    {
        public readonly MusicService _musicService;
        private static readonly IEnumerable<int> Range = Enumerable.Range(1900, 2000);

        public MusicModule(MusicService musicService)
        {
            _musicService = musicService;
        }

        [Command("join")]
        [Summary("Joins the voice channel you are currently in")]
        public async Task Join()
        {
            var user = Context.User as SocketGuildUser;
            if (user.VoiceChannel is null)
            {
                await ReplyAsync("<:voice_locked:585783907488628797> You need to connect to a voice channel");
                return;
            }
            else
            {
                await _musicService.ConnectAsync(user.VoiceChannel, Context.Channel as ITextChannel);
                await ReplyAsync($"<:voice:585783907673440266> Now connected to **{user.VoiceChannel.Name}**");
            }
        }

        [Command("leave")]
        [Summary("Leaves the voice channel which you are currently in")]
        public async Task Leave()
        {
            var user = Context.User as SocketGuildUser;
            if (user.VoiceChannel is null)
            {
                await ReplyAsync("<:voice_locked:585783907488628797> Please join the voice channel I am in to have me leave");
            }
            else
            {
                await _musicService.LeaveAsync(user.VoiceChannel);
                await ReplyAsync($"<:voice:585783907673440266> I have now left **{user.VoiceChannel.Name}**");
            }
        }

        [Command("play")]
        [Summary("Plays a track of your choice")]
        public async Task Play([Remainder] string query)
            => await ReplyAsync(await _musicService.PlayAsync(query, Context.Guild.Id));


        [Command("stop")]
        [Summary("Stops the player")]
        public async Task Stop()
            => await ReplyAsync(await _musicService.StopAsync(Context.Guild.Id));

        [Command("skip")]
        [Summary("Skips the currently playing track")]
        public async Task Skip()
            => await ReplyAsync(await _musicService.SkipAsync(Context.Guild.Id));

        [Command("volume")]
        [Summary("Sets the volume which the track will play at")]
        public async Task Volume(int vol)
            => await ReplyAsync(await _musicService.SetVolumeAsync(vol, Context.Guild.Id));

        [Command("pause")]
        [Summary("Pauses the player")]
        public async Task Pause()
            => await ReplyAsync(await _musicService.PauseOrResumeAsync(Context.Guild.Id));

        [Command("resume")]
        [Summary("Resumes playback")]
        public async Task Resume()
            => await ReplyAsync(await _musicService.ResumeAsync(Context.Guild.Id));
    }
}
*/
