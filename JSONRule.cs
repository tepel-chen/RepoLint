using System.Collections.Generic;
using System.Text.Json;

namespace RepoLint
{
	internal abstract class JSONRule : Rule
	{
		public JSONRule() : base(".json") {}

		protected override void Lint()
		{
			try
			{
				JSON(JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(Content));
			}
			catch (JsonException)
			{
			}
		}

		protected abstract void JSON(Dictionary<string, JsonElement> json);
	}
}