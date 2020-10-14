using System.IO;
using System.Text.RegularExpressions;

namespace RepoLint
{
	public static class GeneralExtensions
	{
		/// <summary>
		/// Compares the string against a given pattern.
		/// </summary>
		/// <param name="str">The string.</param>
		/// <param name="pattern">The pattern to match, where "*" means any sequence of characters, and "?" means any single character.</param>
		/// <returns><c>true</c> if the string matches the given pattern; otherwise <c>false</c>.</returns>
		public static bool Like(this string str, string pattern)
		{
			return new Regex(
				"^" + Regex.Escape(pattern).Replace(@"\*", ".*").Replace(@"\?", ".") + "$",
				RegexOptions.IgnoreCase | RegexOptions.Singleline
			).IsMatch(str);
		}

		public static string GetNameWithoutExtension(this FileInfo file) => Path.GetFileNameWithoutExtension(file.Name);
	}
}