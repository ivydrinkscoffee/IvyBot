using System.Configuration;

namespace IvyBot.Configuration {
    public class ConfigManager : IConfiguration {
        public string GetValueFor (string key) {
            return ConfigurationManager.AppSettings[key];
        }

        public void SetValueFor (string key, string value) {
            StoreSetting (new KeyValuePair (key, value));
        }

        private static void StoreSetting (KeyValuePair setting) {
            var editor = new ConfigurationFileEditor ();
            editor.WriteSetting (setting);
            editor.Save ();
        }
    }
}