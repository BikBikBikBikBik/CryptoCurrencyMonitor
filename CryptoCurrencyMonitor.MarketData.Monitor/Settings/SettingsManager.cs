using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace CryptoCurrencyMonitor.MarketData.Monitor.Settings {
	internal class SettingsManager {
		public SettingsManager(String settingsFileName) {
			_settingsFileName = settingsFileName;
		}

		public CompleteSettings LoadCompleteSettings() {
			if (!File.Exists(_settingsFileName)) {
				return null;
			}

			var settingsFileContents = File.ReadAllText(_settingsFileName, SETTINGS_FILE_ENCODING);
			if (String.IsNullOrWhiteSpace(settingsFileContents)) {
				return null;
			}

			var completeSettings = JsonConvert.DeserializeObject<CompleteSettings>(settingsFileContents);

			return completeSettings;
		}

		public void SaveCompleteSettings(CompleteSettings settings) {
			var completeSettings = JsonConvert.SerializeObject(settings, Formatting.Indented, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver(), Converters = new List<JsonConverter> { new StringEnumConverter() } });

			File.WriteAllText(_settingsFileName, completeSettings, SETTINGS_FILE_ENCODING);
		}

		private static readonly Encoding SETTINGS_FILE_ENCODING = Encoding.ASCII;
		private readonly String _settingsFileName;
	}
}
