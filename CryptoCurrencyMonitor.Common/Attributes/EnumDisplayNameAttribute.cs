using System;
using System.ComponentModel;

namespace CryptoCurrencyMonitor.Common.Attributes {
	[AttributeUsage(AttributeTargets.Field)]
	public class EnumDisplayNameAttribute : DisplayNameAttribute {
		public EnumDisplayNameAttribute(String displayName) : base(displayName) {
		}
	}
}