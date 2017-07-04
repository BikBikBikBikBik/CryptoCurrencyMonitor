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
