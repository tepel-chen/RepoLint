using System;
using System.Collections.Generic;
using System.Text.Json;

namespace RepoLint.Rules
{
	internal class InvalidDate : JSONRule
	{
		protected override void JSON(Dictionary<string, JsonElement> json)
		{
			if (json.TryGetValue("Published", out JsonElement element) && element.ValueKind == JsonValueKind.String && !DateTime.TryParse(element.GetString(), out DateTime _))
			{
				Report("Published needs to be a valid date.");
			}
		}
	}
}