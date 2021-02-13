using Discord;
using Discord.WebSocket;
using System.Linq;
using System.Threading.Tasks;
using Victoria;
using Victoria.Entities;
using IvyBot.Configuration;
using System;

namespace IvyBot.Services
{
    public class MusicService
    {
        private readonly LavaRestClient _lavaRestClient;
        private readonly LavaSocketClient _lavaSocketClient;
        private readonly DiscordSocketClient _client;
        private readonly LogService _logService;
        private IConfiguration _config;

        public MusicService(DiscordSocketClient client, LavaSocketClient lavaSocketClient, LogService logService, IConfiguration config)
        {
            _client = client;
            _config = config;

            int _port = Int32.Parse(_config.GetValueFor(Constants.LavaPort));

            _lavaRestClient = new LavaRestClient(new Victoria.Configuration {
                Host = _config.GetValueFor(Constants.LavaHost),
                Port = _port,
                Password = _config.GetValueFor(Constants.LavaPassword)
            });
            
            _lavaSocketClient = lavaSocketClient;
            _logService = logService;
        }

        public Task InitializeAsync()
        {
            _client.Ready += ClientReadyAsync;
            _lavaSocketClient.Log += LogAsync;
            _lavaSocketClient.OnTrackFinished += TrackFinished;
            return Task.CompletedTask;
        }

        public async Task ConnectAsync(SocketVoiceChannel voiceChannel, ITextChannel textChannel)
            => await _lavaSocketClient.ConnectAsync(voiceChannel, textChannel);

        public async Task LeaveAsync(SocketVoiceChannel voiceChannel)
            => await _lavaSocketClient.DisconnectAsync(voiceChannel);

        public async Task<string> PlayAsync(string query, ulong guildId)
        {
            var _player = _lavaSocketClient.GetPlayer(guildId);
            var results = await _lavaRestClient.SearchYouTubeAsync(query);

            if (results.LoadType == LoadType.NoMatches || results.LoadType == LoadType.LoadFailed)
            {
                return "<:xmark:314349398824058880> No matches found";
            }

            var track = results.Tracks.FirstOrDefault();

            if (_player.IsPlaying)
            {
                _player.Queue.Enqueue(track);
                return $"<:youtube:314349922885566475> **{track.Title}** has been added to the queue";
            }
            else
            {
                await _player.PlayAsync(track);
                return $"<:youtube:314349922885566475> Now playing **{track.Title}**";
            }
        }

        public async Task<string> StopAsync(ulong guildId)
        {
            var _player = _lavaSocketClient.GetPlayer(guildId);
            if (_player is null)
                return "<:xmark:314349398824058880> Error with player";
            await _player.StopAsync();
            return "<:muted:585767366722584576> Music playback stopped";
        }

        public async Task<string> SkipAsync(ulong guildId)
        {
            var _player = _lavaSocketClient.GetPlayer(guildId);
            if (_player is null || _player.Queue.Items.Count() is 0)
                return "<:empty:314349398723264512> Nothing in queue";

            var oldTrack = _player.CurrentTrack;
            await _player.SkipAsync();
            return $"<:join_arrow:599612545002635274> Skipped **{oldTrack.Title}**\n<:youtube:314349922885566475> Now playing **{_player.CurrentTrack.Title}**";
        }

        public async Task<string> SetVolumeAsync(int vol, ulong guildId)
        {
            var _player = _lavaSocketClient.GetPlayer(guildId);
            if (_player is null)
                return "<:muted:585767366722584576> Player isn't playing";

            if (vol > 150 || vol <= 2)
            {
                return "<:xmark:314349398824058880> Please use a number between **2 - 150**";
            }

            await _player.SetVolumeAsync(vol);
            return $"<:check:314349398811475968> Volume set to **{vol}**";
        }

        public async Task<string> PauseOrResumeAsync(ulong guildId)
        {
            var _player = _lavaSocketClient.GetPlayer(guildId);
            if (_player is null)
                return "<:muted:585767366722584576> Player isn't playing";

            if (!_player.IsPaused)
            {
                await _player.PauseAsync();
                return "<:slowmode:585790802979061760> Player is paused";
            }
            else
            {
                await _player.ResumeAsync();
                return "<:unmuted:585788304210001920> Playback resumed";
            }
        }

        public async Task<string> ResumeAsync(ulong guildId)
        {
            var _player = _lavaSocketClient.GetPlayer(guildId);
            if (_player is null)
                return "<:muted:585767366722584576> Player isn't playing";

            if (_player.IsPaused)
            {
                await _player.ResumeAsync();
                return "<:unmuted:585788304210001920> Playback resumed";
            }

            return "<:xmark:314349398824058880> Player is not paused";
        }

        public async Task ClientReadyAsync()
        {
            int _port = Int32.Parse(_config.GetValueFor(Constants.LavaPort));

            await _lavaSocketClient.StartAsync(_client, new Victoria.Configuration {
                Host = _config.GetValueFor(Constants.LavaHost),
                Port = _port,
                Password = _config.GetValueFor(Constants.LavaPassword)
            });
        }

        private async Task TrackFinished(LavaPlayer player, LavaTrack track, TrackEndReason reason)
        {
            if (!reason.ShouldPlayNext())
                return;

            if (!player.Queue.TryDequeue(out var item) || !(item is LavaTrack nextTrack))
            {
                await player.TextChannel.SendMessageAsync("<:empty:314349398723264512> There are no more tracks in the queue");
                return;
            }

            await player.PlayAsync(nextTrack);
        }

        private async Task LogAsync(LogMessage logMessage)
        {
            await _logService.LogAsync(logMessage);
        }
    }
}
