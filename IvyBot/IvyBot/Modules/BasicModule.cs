using Discord.Commands;
using System.Threading.Tasks;

namespace IvyBot.Modules
{
    [Name("Basic")]
    public class BasicModule : ModuleBase<SocketCommandContext>
    {

        [Command("stonks", RunMode = RunMode.Async)]
        [Summary("Get stonks")]
        public async Task Stonks()
        {
            await ReplyAsync("https://tenor.com/view/stonks-up-stongs-meme-stocks-gif-15715298");
        }

        [Command("notstonks", RunMode = RunMode.Async)]
        [Summary("Lose stonks")]
        public async Task NotStonks()
        {
            await ReplyAsync("https://tenor.com/view/not-stonks-profit-down-sad-frown-arms-crossed-gif-15684535");
        }

        [Command("obama", RunMode = RunMode.Async)]
        [Summary("Embrace the Obama")]
        public async Task Obama()
        {
            await ReplyAsync("https://tenor.com/view/obama-prism-politics-triangle-illuminati-gif-16608552");
        }

        [Command("covid", RunMode = RunMode.Async)]
        [Summary("Chile...")]
        public async Task CovidAsync()
        {
            await ReplyAsync("https://cdn.discordapp.com/attachments/597722263550296071/743097970718933054/EcLmbnFXgAAn_pX.png");
        }

        [Command("emergency", RunMode = RunMode.Async)]
        [Summary("Kinda sus")]
        public async Task EmergencyAsync()
        {
            await ReplyAsync("https://cdn.discordapp.com/attachments/742845108424278027/761034336660095026/image0.png");
        }

        [Command("hueh", RunMode = RunMode.Async)]
        [Summary("Yes")]
        public async Task HuehAsync()
        {
            await ReplyAsync("https://cdn.discordapp.com/emojis/535595627124359178.png?v=1");
        }

        [Command("pog", RunMode = RunMode.Async)]
        [Summary("Poggers")]
        public async Task PogAsync()
        {
            await ReplyAsync("⠄⠄⠄⠄⠄⠄⣀⣀⣀⣤⣶⣿⣿⣶⣶⣶⣤⣄⣠⣴⣶⣿⣿⣿⣿⣶⣦⣄⠄⠄\n⠄⠄⣠⣴⣾⣿⠿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣦\n⢠⠾⣋⣭⣄⡀⠄⠄⠈⠙⠻⣿⣿⡿⠛⠋⠉⠉⠉⠙⠛⠿⣿⣿⣿⣿⣿⣿⣿⣿\n⡎⣾⡟⢻⣿⣷⠄⠄⠄⠄⠄⡼⣡⣾⣿⣿⣦⠄⠄⠄⠄⠄⠈⠛⢿⣿⣿⣿⣿⣿\n⡇⢿⣷⣾⣿⠟⠄⠄⠄⠄⢰⠁⣿⣇⣸⣿⣿⠄⠄⠄⠄⠄⠄⠄⣠⣼⣿⣿⣿⣿\n⢸⣦⣭⣭⣄⣤⣤⣤⣴⣶⣿⣧⡘⠻⠛⠛⠁⠄⠄⠄⠄⣀⣴⣿⣿⣿⣿⣿⣿⣿\n⠄⢉⣹⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣶⣦⣶⣶⣶⣶⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿\n⢰⡿⠛⠛⠛⠛⠻⠿⠿⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿\n⠸⡇⠄⠄⢀⣀⣀⠄⠄⠄⠄⠄⠉⠉⠛⠛⠻⠿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿\n⠄⠈⣆⠄⠄⢿⣿⣿⣿⣷⣶⣶⣤⣤⣀⣀⡀⠄⠄⠉⢻⣿⣿⣿⣿⣿⣿⣿⣿⣿\n⠄⠄⣿⡀⠄⠸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠂⠄⢠⣿⣿⣿⣿⣿⣿⣿⣿⣿\n⠄⠄⣿⡇⠄⠄⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠃⠄⢀⣼⣿⣿⣿⣿⣿⣿⣿⣿⣿\n⠄⠄⣿⡇⠄⠠⣿⣿⣿⣿⣿⣿⣿⣿⡿⠋⠄⠄⣠⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿\n⠄⠄⣿⠁⠄⠐⠛⠛⠛⠛⠉⠉⠉⠉⠄⠄⣠⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿\n⠄⠄⠻⣦⣀⣀⣀⣀⣀⣀⣤⣤⣤⣤⣶⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠋⠄");
        }
        
        [Command("chadthink", RunMode = RunMode.Async)]
        [Summary("Very chad indeed")]
        public async Task ChadThinkAsync()
        {
            await ReplyAsync("https://cdn.discordapp.com/emojis/366999782348292108.png?v=1");
        }
    }
}