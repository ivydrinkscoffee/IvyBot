using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace IvyBot.Services
{
    public enum HttpRequestMethod
    {
        GET,
        POST
    }
    
    public class SearchService
    {
        private static readonly HttpClient http = new HttpClient();
        
        public static async Task<Stream> GetResponseStreamAsync(string url, IEnumerable<KeyValuePair<string, string>> headers = null, HttpRequestMethod method = HttpRequestMethod.GET)
        {
            if (string.IsNullOrWhiteSpace(url))
                
                throw new ArgumentNullException(nameof(url));
            
            var something = headers == null || method == HttpRequestMethod.POST ? http : new HttpClient();
            
            something.DefaultRequestHeaders.Clear();
            
            switch (method)
            {
                case HttpRequestMethod.GET:
                    
                    if (headers != null)
                    {
                        foreach (var header in headers)
                        {
                            something.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
                        }
                    }
                    
                    return await something.GetStreamAsync(url);
                
                case HttpRequestMethod.POST:
                    
                    FormUrlEncodedContent formdatcontent = null;
                    
                    if (headers != null)
                    {
                        formdatcontent = new FormUrlEncodedContent(headers);
                    }
                    
                    var message = await something.PostAsync(url, formdatcontent);
                    return await message.Content.ReadAsStreamAsync();
                
                default:
                    
                    throw new NotImplementedException("Request type unsupported");
            }
        }
        
        public static async Task<string> GetResponseStringAsync(string url, IEnumerable<KeyValuePair<string, string>> headers = null, HttpRequestMethod method = HttpRequestMethod.GET)
        {
            using (var streampoop = new StreamReader(await GetResponseStreamAsync(url, headers, method)))
            {
                return await streampoop.ReadToEndAsync();
            }
        }
    }
}
