using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using IvyBot.Configuration;
using Victoria;
using Victoria.Enums;
using Victoria.EventArgs;

namespace IvyBot.Services {
    public class MusicService {
        private readonly DiscordSocketClient _client;
        private readonly LavaNode _lavaNode;
        private readonly LogService _logService;
        private readonly ConfigurationManager _configurationManager;
        private static readonly IEnumerable<int> Range = Enumerable.Range (1900, 2000);

        public MusicService (DiscordSocketClient client, LavaNode lavaNode, LogService logService, ConfigurationManager configurationManager) {
            _client = client;
            _configurationManager = configurationManager;

            ushort _port = ushort.Parse (_configurationManager.GetValueFor (Constants.LavaPort));

            _lavaNode = new LavaNode (_client, new LavaConfig {
                Hostname = _configurationManager.GetValueFor (Constants.LavaHost),
                    Port = _port,
                    Authorization = _configurationManager.GetValueFor (Constants.LavaPassword),
                    LogSeverity = LogSeverity.Warning
            });

            _logService = logService;
        }

        public Task InitializeAsync () {
            _client.Ready += ClientReadyAsync;
            _lavaNode.OnLog += LogAsync;
            _lavaNode.OnTrackEnded += TrackEnded;
            return Task.CompletedTask;
        }

        public async Task ConnectAsync (SocketVoiceChannel voiceChannel, ITextChannel textChannel) => await _lavaNode.JoinAsync (voiceChannel, textChannel);

        public async Task LeaveAsync (SocketVoiceChannel voiceChannel) => await _lavaNode.LeaveAsync (voiceChannel);

        public async Task<string> PlayAsync (string query, ulong guildId) {
            var _player = _lavaNode.GetPlayer (_client.GetGuild (guildId));
            var results = await _lavaNode.SearchYouTubeAsync (query);

            if (results.LoadStatus == LoadStatus.NoMatches || results.LoadStatus == LoadStatus.LoadFailed) {
                return $"No matches found for '{query}'";
            }

            var track = results.Tracks.FirstOrDefault ();

            if (_player.PlayerState == PlayerState.Playing) {
                _player.Queue.Enqueue (track);
                return $"**{track.Title}** has been added to the queue";
            } else {
                await _player.PlayAsync (track);
                return $"Now playing **{track.Title}**";
            }
        }

        public async Task<string> StopAsync (ulong guildId) {
            var _player = _lavaNode.GetPlayer (_client.GetGuild (guildId));
            if (_player is null)
                return "Error with player";
            await _player.StopAsync ();
            return "Music playback stopped";
        }

        public async Task<string> SkipAsync (ulong guildId) {
            var _player = _lavaNode.GetPlayer (_client.GetGuild (guildId));
            if (_player is null || _player.Queue.Count () is 0)
                return "Nothing in queue";

            var oldTrack = _player.Track;
            await _player.SkipAsync ();
            return $"Skipped **{oldTrack.Title}**\nNow playing **{_player.Track.Title}**";
        }

        public async Task<string> SetVolumeAsync (ushort vol, ulong guildId) {
            var _player = _lavaNode.GetPlayer (_client.GetGuild (guildId));
            if (_player is null)
                return "Player isn't playing";

            if (vol > 150 || vol <= 2) {
                return "Please use a number between **2** and **150**";
            }

            await _player.UpdateVolumeAsync (vol);
            return $"Volume set to **{vol}**";
        }

        public async Task<string> PauseOrResumeAsync (ulong guildId) {
            var _player = _lavaNode.GetPlayer (_client.GetGuild (guildId));
            if (_player is null)
                return "Player isn't playing";

            if (!(_player.PlayerState == PlayerState.Paused)) {
                await _player.PauseAsync ();
                return "Player is paused";
            } else {
                await _player.ResumeAsync ();
                return "Playback resumed";
            }
        }

        public async Task<string> ResumeAsync (ulong guildId) {
            var _player = _lavaNode.GetPlayer (_client.GetGuild (guildId));
            if (_player is null)
                return "Player isn't playing";

            if (_player.PlayerState == PlayerState.Paused) {
                await _player.ResumeAsync ();
                return "Playback resumed";
            }

            return "Player is not paused";
        }

        public async Task<string> GetCurrentLyricsAsync (ulong guildId) {
            if (!_lavaNode.TryGetPlayer (_client.GetGuild (guildId), out var player)) {
                return "I am not connected to a voice channel";
            }

            if (player.PlayerState != PlayerState.Playing) {
                return "Player isn't playing";
            }

            var lyrics = await player.Track.FetchLyricsFromGeniusAsync ();
            if (string.IsNullOrWhiteSpace (lyrics)) {
                return $"No lyrics found for **{player.Track.Title}**";
            }

            var splitLyrics = lyrics.Split ('\n');
            var stringBuilder = new StringBuilder ();
            foreach (var line in splitLyrics) {
                if (Range.Contains (stringBuilder.Length)) {
                    return $"```{stringBuilder}```";
                } else {
                    stringBuilder.AppendLine (line);
                }
            }

            return $"```{stringBuilder}```";
        }

        public async Task ClientReadyAsync () {
            await _lavaNode.ConnectAsync ();
        }

        private async Task TrackEnded (TrackEndedEventArgs trackEndedEventArgs) {
            if (!trackEndedEventArgs.Reason.ShouldPlayNext ())
                return;

            if (!trackEndedEventArgs.Player.Queue.TryDequeue (out var item) || !(item is LavaTrack nextTrack)) {
                await trackEndedEventArgs.Player.TextChannel.SendMessageAsync ("There are no more tracks in the queue");
                return;
            }

            await trackEndedEventArgs.Player.PlayAsync (nextTrack);
        }

        private async Task LogAsync (LogMessage logMessage) {
            await _logService.LogAsync (logMessage);
        }
    }
}