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
        [Command("smug")]
        [Summary("Share your smugness")]
        public async Task SmugAsync()
        {
            string[] smug = { "https://media.discordapp.net/attachments/733629569667563553/743850246072959036/SmugTekaaluk60.gif", "https://media.discordapp.net/attachments/733629569667563553/744288849127276544/icecattac60.gif", "https://media.discordapp.net/attachments/733629569667563553/744616741820301392/SmugTanuki60.gif", "https://media.discordapp.net/attachments/733629569667563553/743817046281683074/SmugCitrus60.gif" };
            Random random = new Random();
            int index = random.Next(smug.Length);
            
            await ReplyAsync($"{smug[index]}");
        }

        [Command("ratedick")]
        [Summary("The gay council will decide your dick's fate")]
        public async Task RateAsync()
        {
            string[] length = { "==========", "=========", "========", "=======", "======", "=====", "====", "===", "==", "=", "()" };
            string[] rating = { "10", "9", "8", "7", "6", "5", "4", "3", "2", "1", "0" };
            
            Random random = new Random();
            
            int lengthIndex = random.Next(length.Length);
            int ratingIndex = random.Next(rating.Length);

            EmbedBuilder builder = new EmbedBuilder();

            builder.WithTitle("Dick");

            if (length[lengthIndex].ToString() == "()")
            {
                builder.AddField("Length", "()", true);
                builder.AddField("Rating", "0/10", true);
            }
            else
            {
                builder.AddField("Length", $"8{length[lengthIndex]}D", true);
                builder.AddField("Rating", $"{rating[ratingIndex]}/10", true);
            }

            builder.WithCurrentTimestamp();
            builder.WithFooter("Coded and maintained by Ivy#9804 in Discord.Net");
            builder.WithColor(Color.Blue);

            await ReplyAsync(null, false, builder.Build());
        }

        [Command("duck")]
        [Summary("Sends a random duck")]
        public async Task GetDuckAsync()
        {
            string json = new WebClient().DownloadString("https://random-d.uk/api/random");

            Dictionary<string, string> items = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                
            EmbedBuilder builder = new EmbedBuilder();

            builder.WithImageUrl(items["url"]);
            builder.WithFooter(items["message"]);
            builder.WithCurrentTimestamp();
            builder.WithColor(Color.Blue);

            await ReplyAsync(null, false, builder.Build());
        }

        [Command("dog")]
        [Summary("Sends a random dog")]
        public async Task GetDogAsync()
        {
            string json = new WebClient().DownloadString("https://random.dog/woof.json");

            Dictionary<string, string> items = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            EmbedBuilder builder = new EmbedBuilder();

            builder.WithImageUrl(items["url"]);
            builder.WithFooter("Powered by random.dog");
            builder.WithCurrentTimestamp();
            builder.WithColor(Color.Blue);

            await ReplyAsync(null, false, builder.Build());
        }

        [Command("cat")]
        [Summary("Sends a random cat")]
        public async Task GetCatAsync()
        {
            string json = new WebClient().DownloadString("http://aws.random.cat//meow");

            Dictionary<string, string> items = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            EmbedBuilder builder = new EmbedBuilder();

            builder.WithImageUrl(items["file"]);
            builder.WithFooter("Powered by random.cat");
            builder.WithCurrentTimestamp();
            builder.WithColor(Color.Blue);

            await ReplyAsync(null, false, builder.Build());
        }

        [Command("meme")]
        [Summary("Sends a random meme")]
        public async Task GetMemeAsync()
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

        [Command("coffee")]
        [Summary("You need coffee")]
        public async Task GetCoffeeAsync()
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
