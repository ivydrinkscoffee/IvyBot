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
                public Arm64[] Arm64 { get; set; }
            }

            public partial struct Arm64 {
                public bool? Bool;
                public string String;

                public static implicit operator Arm64 (bool Bool) => new Arm64 { Bool = Bool };
                public static implicit operator Arm64 (string String) => new Arm64 { String = String };
            }

            public partial class Json {
                public static Json FromJson (string json) => JsonConvert.DeserializeObject<Json> (json, Converter.Settings);
            }

            internal static class Converter {
                public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings {
                    MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                    DateParseHandling = DateParseHandling.None,
                    Converters = {
                    Arm64Converter.Singleton,
                    new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
                    },
                };
            }

            internal class Arm64Converter : JsonConverter {
                public override bool CanConvert (Type t) => t == typeof (Arm64) || t == typeof (Arm64?);

                public override object ReadJson (JsonReader reader, Type t, object existingValue, JsonSerializer serializer) {
                    switch (reader.TokenType) {
                        case JsonToken.Boolean:
                            var boolValue = serializer.Deserialize<bool> (reader);
                            return new Arm64 { Bool = boolValue };
                        case JsonToken.String:
                        case JsonToken.Date:
                            var stringValue = serializer.Deserialize<string> (reader);
                            return new Arm64 { String = stringValue };
                    }
                    throw new Exception ("Cannot unmarshal type Arm64");
                }

                public override void WriteJson (JsonWriter writer, object untypedValue, JsonSerializer serializer) {
                    var value = (Arm64) untypedValue;
                    if (value.Bool != null) {
                        serializer.Serialize (writer, value.Bool.Value);
                        return;
                    }
                    if (value.String != null) {
                        serializer.Serialize (writer, value.String);
                        return;
                    }
                    throw new Exception ("Cannot marshal type Arm64");
                }

                public static readonly Arm64Converter Singleton = new Arm64Converter ();
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
                public Arm64[] Arm64 { get; set; }
            }

            public partial struct Arm64 {
                public bool? Bool;
                public string String;

                public static implicit operator Arm64 (bool Bool) => new Arm64 { Bool = Bool };
                public static implicit operator Arm64 (string String) => new Arm64 { String = String };
            }

            public partial class Json {
                public static Json FromJson (string json) => JsonConvert.DeserializeObject<Json> (json, Converter.Settings);
            }

            internal static class Converter {
                public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings {
                    MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                    DateParseHandling = DateParseHandling.None,
                    Converters = {
                    Arm64Converter.Singleton,
                    new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
                    },
                };
            }

            internal class Arm64Converter : JsonConverter {
                public override bool CanConvert (Type t) => t == typeof (Arm64) || t == typeof (Arm64?);

                public override object ReadJson (JsonReader reader, Type t, object existingValue, JsonSerializer serializer) {
                    switch (reader.TokenType) {
                        case JsonToken.Boolean:
                            var boolValue = serializer.Deserialize<bool> (reader);
                            return new Arm64 { Bool = boolValue };
                        case JsonToken.String:
                        case JsonToken.Date:
                            var stringValue = serializer.Deserialize<string> (reader);
                            return new Arm64 { String = stringValue };
                    }
                    throw new Exception ("Cannot unmarshal type Arm64");
                }

                public override void WriteJson (JsonWriter writer, object untypedValue, JsonSerializer serializer) {
                    var value = (Arm64) untypedValue;
                    if (value.Bool != null) {
                        serializer.Serialize (writer, value.Bool.Value);
                        return;
                    }
                    if (value.String != null) {
                        serializer.Serialize (writer, value.String);
                        return;
                    }
                    throw new Exception ("Cannot marshal type Arm64");
                }

                public static readonly Arm64Converter Singleton = new Arm64Converter ();
            }
        }
    }
}