using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace IvyBot.Services
{
    public enum RequestHttpMethod
    {
        GET,
        POST
    }
    
    public class SearchService
    {
        private static readonly HttpClient http = new HttpClient();
        
        public static async Task<Stream> GetResponseStreamAsync(string url, IEnumerable<KeyValuePair<string, string>> headers = null, RequestHttpMethod method = RequestHttpMethod.GET)
        {
            if (string.IsNullOrWhiteSpace(url))
                
                throw new ArgumentNullException(nameof(url));
            
            var something = headers == null || method == RequestHttpMethod.POST ? http : new HttpClient();
            
            something.DefaultRequestHeaders.Clear();
            
            switch (method)
            {
                case RequestHttpMethod.GET:
                    
                    if (headers != null)
                    {
                        foreach (var header in headers)
                        {
                            something.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
                        }
                    }
                    
                    return await something.GetStreamAsync(url).ConfigureAwait(false);
                
                case RequestHttpMethod.POST:
                    
                    FormUrlEncodedContent formdatcontent = null;
                    
                    if (headers != null)
                    {
                        formdatcontent = new FormUrlEncodedContent(headers);
                    }
                    
                    var message = await something.PostAsync(url, formdatcontent).ConfigureAwait(false);
                    return await message.Content.ReadAsStreamAsync().ConfigureAwait(false);
                
                default:
                    
                    throw new NotImplementedException("Request type unsupported.");
            }
        }
        
        public static async Task<string> GetResponseStringAsync(string url, IEnumerable<KeyValuePair<string, string>> headers = null, RequestHttpMethod method = RequestHttpMethod.GET)
        {
            using (var streampoop = new StreamReader(await GetResponseStreamAsync(url, headers, method).ConfigureAwait(false)))
            {
                return await streampoop.ReadToEndAsync().ConfigureAwait(false);
            }
        }
    }
}