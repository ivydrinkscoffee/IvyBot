using System.Threading.Tasks;

namespace IvyBot {
    class Program {
        static async Task Main (string[] args) => await new IvyBotClient ().InitializeAsync ();
    }
}