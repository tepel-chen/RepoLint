using System.Collections.Generic;
using System.Text.Json;

namespace RepoLint.Rules
{
	internal class InvalidJSON : Rule
	{
		public InvalidJSON() : base(".json") {}

		protected override void Lint()
		{
			try
			{
				JsonSerializer.Deserialize<Dictionary<string, object>>(Content);
			}
			catch (JsonException)
			{
				Report("Invalid JSON.");
			}
		}
	}
}