/*
Copyright (C) 2018 BikBikBikBikBik

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
using System.Drawing;

namespace CryptoCurrencyMonitor.MarketData.Monitor.Extensions {
	internal static class ColorExtensionMethods {
		public static Color Darken(this Color sourceColor, double scaleFactor) {
			return Color.FromArgb((int)Math.Floor(sourceColor.R * scaleFactor), (int)Math.Floor(sourceColor.G * scaleFactor), (int)Math.Floor(sourceColor.B * scaleFactor));
		}
	}
}
