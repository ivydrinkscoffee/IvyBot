using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IvyBot.Modules
{
    [Name("Randoms")]
    public class RandomModule : ModuleBase<SocketCommandContext>
    {
        [Command("smug", RunMode = RunMode.Async)]
        [Summary("Share your smugness")]
        public async Task SmugAsync()
        {
            string[] smug = { "https://media.discordapp.net/attachments/733629569667563553/743850246072959036/SmugTekaaluk60.gif", "https://media.discordapp.net/attachments/733629569667563553/744288849127276544/icecattac60.gif", "https://media.discordapp.net/attachments/733629569667563553/744616741820301392/SmugTanuki60.gif", "https://media.discordapp.net/attachments/733629569667563553/743817046281683074/SmugCitrus60.gif" };
            Random random = new Random();
            int index = random.Next(smug.Length);
            
            await ReplyAsync($"{smug[index]}");
        }

        [Command("duck", RunMode = RunMode.Sync)]
        [Summary("Sends a random duck")]
        public async Task GetDuck()
        {
            string json = new WebClient().DownloadString("https://random-d.uk/api/random");

            Dictionary<string, string> items = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                
            EmbedBuilder builder = new EmbedBuilder();

            builder.WithImageUrl(items["url"]);
            builder.WithFooter(items["message"]);
            builder.WithCurrentTimestamp();
            builder.WithColor(Color.Blue);

            await Context.Channel.SendMessageAsync("", false, builder.Build());
        }

        [Command("dog", RunMode = RunMode.Sync)]
        [Summary("Sends a random dog")]
        public async Task GetDog()
        {
            string json = new WebClient().DownloadString("https://random.dog/woof.json");

            Dictionary<string, string> items = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            EmbedBuilder builder = new EmbedBuilder();

            builder.WithImageUrl(items["url"]);
            builder.WithFooter("Powered by random.dog");
            builder.WithCurrentTimestamp();
            builder.WithColor(Color.Blue);

            await Context.Channel.SendMessageAsync("", false, builder.Build());
        }

        [Command("cat", RunMode = RunMode.Sync)]
        [Summary("Sends a random cat")]
        public async Task GetCat()
        {
            string json = new WebClient().DownloadString("http://aws.random.cat//meow");

            Dictionary<string, string> items = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            EmbedBuilder builder = new EmbedBuilder();

            builder.WithImageUrl(items["file"]);
            builder.WithFooter("Powered by random.cat");
            builder.WithCurrentTimestamp();
            builder.WithColor(Color.Blue);

            await Context.Channel.SendMessageAsync("", false, builder.Build());
        }

        [Command("meme", RunMode = RunMode.Sync)]
        [Summary("Sends a random meme")]
        public async Task GetMeme()
        {
            string json = new WebClient().DownloadString("https://some-random-api.ml/meme");

            Dictionary<string, string> items = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            EmbedBuilder builder = new EmbedBuilder();

            builder.WithTitle(items["caption"]);
            builder.WithImageUrl(items["image"]);
            builder.WithFooter(items["category"]);
            builder.WithCurrentTimestamp();
            builder.WithColor(Color.Blue);

            await Context.Channel.SendMessageAsync("", false, builder.Build());
        }

        [Command("coffee", RunMode = RunMode.Sync)]
        [Summary("You need coffee")]
        public async Task GetCoffee()
        {
            string json = new WebClient().DownloadString("https://coffee.alexflipnote.dev/random.json");

            Dictionary<string, string> items = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            EmbedBuilder builder = new EmbedBuilder();

            builder.WithImageUrl(items["file"]);
            builder.WithFooter("Powered by coffee.alexflipnote.dev");
            builder.WithCurrentTimestamp();
            builder.WithColor(Color.Blue);

            await Context.Channel.SendMessageAsync("", false, builder.Build());
        }
    }
}