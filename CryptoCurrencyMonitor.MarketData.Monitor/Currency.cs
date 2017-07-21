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

namespace CryptoCurrencyMonitor.MarketData.Monitor {
	internal class Currency : IEquatable<Currency> {
		public Currency(int id) {
			Id = id;
		}

		public int Id { get; }

		public String Name { get; set; }

		public String NameAndSymbol => ToString();

		public String Symbol { get; set; }

		#region IEquatable Implementation
		public bool Equals(Currency other) {
			if (ReferenceEquals(null, other)) {
				return false;
			}
			if (ReferenceEquals(this, other)) {
				return true;
			}

			return Id == other.Id;
		}
		#endregion

		#region Equality Checking
		public override bool Equals(object obj) {
			if (ReferenceEquals(null, obj)) {
				return false;
			}
			if (ReferenceEquals(this, obj)) {
				return true;
			}
			if (obj.GetType() != GetType()) {
				return false;
			}

			return Equals((Currency)obj);
		}

		public override int GetHashCode() {
			return Id;
		}

		public static bool operator ==(Currency left, Currency right) {
			return Equals(left, right);
		}

		public static bool operator !=(Currency left, Currency right) {
			return !Equals(left, right);
		}
		#endregion

		public override string ToString() {
			return $"{Symbol} ({Name})";
		}
	}
}
