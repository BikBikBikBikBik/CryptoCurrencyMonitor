/*
Copyright (C) 2017 BikBikBikBikBik

This file is part of CryptoCurrencyMonitor.

CryptoCurrencyMonitor is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

CryptoCurrencyMonitor is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with CryptoCurrencyMonitor.  If not, see <http://www.gnu.org/licenses/>.
*/
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
				return new CompleteSettings();
			}

			var settingsFileContents = File.ReadAllText(_settingsFileName, SETTINGS_FILE_ENCODING);
			if (String.IsNullOrWhiteSpace(settingsFileContents)) {
				return new CompleteSettings();
			}

			return JsonConvert.DeserializeObject<CompleteSettings>(settingsFileContents);
		}

		public void SaveCompleteSettings(CompleteSettings settings) {
			var completeSettings = JsonConvert.SerializeObject(settings, Formatting.Indented, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver(), Converters = new List<JsonConverter> { new StringEnumConverter() } });

			File.WriteAllText(_settingsFileName, completeSettings, SETTINGS_FILE_ENCODING);
		}

		private static readonly Encoding SETTINGS_FILE_ENCODING = Encoding.ASCII;
		private readonly String _settingsFileName;
	}
}
