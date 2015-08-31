using System;
using System.Linq;

namespace CCWebUIAuto.Helpers
{
	// since we will have a need to randomly generate data, I created this to hold
	// methods for that purpose.  This will get expanded to generate numbers, paragraphs,
	// and other formats for data
	public class DataGen
	{
		private const string UpperCaseCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		private const string LowerCaseCharacters = "abcdefghijklmnopqrstuvwxyz";
		private const string Digits = "0123456789";
		private const string UpperAndLowerCaseCharacters = UpperCaseCharacters + LowerCaseCharacters;
		private static readonly Random Random = new Random();

		public static string String(int count)
		{
			return RandomStringFrom(UpperAndLowerCaseCharacters, count);
		}

		public static string AllUpperCaseString(int count)
		{
			return RandomStringFrom(UpperCaseCharacters, count);
		}

		public static string AllLowerCaseString(int count)
		{
			return RandomStringFrom(LowerCaseCharacters, count);
		}

		/// <summary>
		/// Entire string is lower case, except for first letter
		/// </summary>
		public static string CapitalizedString(int count)
		{
			return AllUpperCaseString(1) + AllLowerCaseString(count - 1);
		}

		public static string NumberString(int count)
		{
			return RandomStringFrom(Digits, count);
		}

		private static string RandomStringFrom(string source, int count)
		{
			return new string(
				Enumerable.Repeat(source, count)
						  .Select(s => s[Random.Next(s.Length)])
						  .ToArray());
		}
	}
}
