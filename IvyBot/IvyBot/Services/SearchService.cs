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
            
            var cl = headers == null || method == RequestHttpMethod.POST ? http : new HttpClient();
            
            cl.DefaultRequestHeaders.Clear();
            
            switch (method)
            {
                case RequestHttpMethod.GET:
                    
                    if (headers != null)
                    {
                        foreach (var header in headers)
                        {
                            cl.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
                        }
                    }
                    
                    return await cl.GetStreamAsync(url).ConfigureAwait(false);
                
                case RequestHttpMethod.POST:
                    
                    FormUrlEncodedContent formContent = null;
                    
                    if (headers != null)
                    {
                        formContent = new FormUrlEncodedContent(headers);
                    }
                    
                    var message = await cl.PostAsync(url, formContent).ConfigureAwait(false);
                    return await message.Content.ReadAsStreamAsync().ConfigureAwait(false);
                
                default:
                    
                    throw new NotImplementedException("That type of request is unsupported.");
            }
        }
        
        public static async Task<string> GetResponseStringAsync(string url, IEnumerable<KeyValuePair<string, string>> headers = null, RequestHttpMethod method = RequestHttpMethod.GET)
        {
            using (var streamReader = new StreamReader(await GetResponseStreamAsync(url, headers, method).ConfigureAwait(false)))
            {
                return await streamReader.ReadToEndAsync().ConfigureAwait(false);
            }
        }
    }
}