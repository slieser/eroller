using System;

namespace eroller.logic.provider
{
	public static class RandomStringId
	{
		private const int LengthOfId = 6;

		public const string LegalCharacters =
			"0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
		private static readonly Random Random = new Random();

		public static string New() {
			var result = new char[LengthOfId];

			for (var i = 0; i < LengthOfId; ++i) {
				var charIndex = Random.Next(LegalCharacters.Length);
				result[i] = LegalCharacters[charIndex];
			}

			return new string(result);
		}
	}
}
