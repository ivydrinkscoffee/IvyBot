// using System.IO;
using System.Threading.Tasks;

namespace IvyBot {
    class Program {
        static async Task Main (string[] args) {
            /*
            if (File.Exists("ivy-bot.log")) {
                File.Delete("ivy-bot.log");
            }
            */

            await new IvyBotClient ().InitializeAsync ();
        }
    }
}