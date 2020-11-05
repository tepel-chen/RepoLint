using System.Collections.Generic;
using System.Text.Json;
using System.Text.RegularExpressions;

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
			catch (JsonException exception)
			{
				var message = Regex.Replace(exception.Message, @" \| Line.+", "");
				if (exception.LineNumber.HasValue)
					ReportLine(message, exception.LineNumber.Value + 1);
				else
					Report(message);
			}
		}
	}
}