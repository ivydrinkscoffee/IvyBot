using System;
using System.Collections.Generic;
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
                var random = new Random();
                var url = $"http://rule34.xxx/index.php?page=dapi&s=post&q=index&limit=100&tags={tag.Replace(" ", "_")}";
                var webpage = await SearchService.GetResponseStringAsync(url);
                var matches = Regex.Matches(webpage, "file_url=\"(?<url>.*?)\"");
                
                if (matches.Count == 0)
                return null;
                
                var match = matches[random.Next(0, matches.Count)];
                return matches[random.Next(0, matches.Count)].Groups["url"].Value;
            }
            catch (Exception ex)
            {
                // Console.WriteLine(ex.Message);
                return $"Error in rule34 search: {ex.Message}";
            }
        }

        public static async Task<string> GetE621File(string tag)
        {
            try
            {
                var headers = new Dictionary<string,string> { {"User-Agent", "ivy-bot/3.1 (https://github.com/Ivy-Wusky/ivy-bot)"} };
                
                var random = new Random();
                string poop = @"""url""";
                var url = $"http://e621.net/posts.json?limit=100&tags={tag.Replace(" ", "%20")}";
                var webpage = await SearchService.GetResponseStringAsync(url, headers);
                var matches = Regex.Matches(webpage, $"{poop}:\"(?<url>.*?)\"");
                
                if (matches.Count == 0)
                return null;
                
                var match = matches[random.Next(0, matches.Count)];
                return matches[random.Next(0, matches.Count)].Groups["url"].Value;
            }
            catch (Exception ex)
            {
                // Console.WriteLine(ex.Message);
                return $"Error in e621 search: {ex.Message}";
            }
        }
    }
}