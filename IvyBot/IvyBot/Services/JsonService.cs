using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IvyBot.Services {
    public class JsonService {
        public class Lyrics {
            public partial class Json {
                [JsonProperty ("title")]
                public string Title { get; set; }

                [JsonProperty ("author")]
                public string Author { get; set; }

                [JsonProperty ("lyrics")]
                public string Lyrics { get; set; }

                [JsonProperty ("thumbnail")]
                public Links Thumbnail { get; set; }

                [JsonProperty ("links")]
                public Links Links { get; set; }
            }

            public partial class Links {
                [JsonProperty ("genius")]
                public Uri Genius { get; set; }
            }

            public partial class Json {
                public static Json FromJson (string json) => JsonConvert.DeserializeObject<Json> (json, Converter.Settings);
            }

            internal static class Converter {
                public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings {
                    MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                    DateParseHandling = DateParseHandling.None,
                    Converters = {
                    new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
                    },
                };
            }
        }
    }
}