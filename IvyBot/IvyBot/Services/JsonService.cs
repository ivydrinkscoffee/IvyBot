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

        public class Hex {
            public partial class Json {
                [JsonProperty ("asm")]
                public Asm Asm { get; set; }

                [JsonProperty ("counter")]
                public long Counter { get; set; }
            }

            public partial class Asm {
                [JsonProperty ("arm64")]
                public object[] Arm64 { get; set; }
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

        public class Assembly {
            public partial class Json {
                [JsonProperty ("hex")]
                public Hex Hex { get; set; }

                [JsonProperty ("counter")]
                public long Counter { get; set; }
            }

            public partial class Hex {
                [JsonProperty ("arm64")]
                public object[] Arm64 { get; set; }
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