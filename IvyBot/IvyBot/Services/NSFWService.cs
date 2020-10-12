using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IvyBot.Services
{
    public class NSFWService
    {
        public static async Task<string> GetRule34File(string tag)
        {
            try
            {
                var rng = new Random();
                var url = $"http://rule34.xxx/index.php?page=dapi&s=post&q=index&limit=100&tags={tag.Replace(" ", "_")}";
                var webpage = await SearchService.GetResponseStringAsync(url).ConfigureAwait(false);
                var matches = Regex.Matches(webpage, "file_url=\"(?<url>.*?)\"");
                
                if (matches.Count == 0)
                return null;
                
                var match = matches[rng.Next(0, matches.Count)];
                return matches[rng.Next(0, matches.Count)].Groups["url"].Value;
            }
            catch (Exception ex)
            {
                // Console.WriteLine(ex.Message);
                return $"Error in rule34 search: {ex.Message}";
            }
        }
    }
}