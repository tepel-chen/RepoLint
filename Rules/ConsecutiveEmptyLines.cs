namespace RepoLint.Rules
{
	internal class ConsecutiveEmptyLines : Rule
	{
		public ConsecutiveEmptyLines() : base(".html") {}

		protected override void Lint()
		{
			int emptyLines = 0;
			int lineNumber = 1;
			foreach (var line in Content.Split("\n"))
			{
				if (!string.IsNullOrWhiteSpace(line))
				{
					if (emptyLines >= 2)
						ReportLine($"{emptyLines} consecutive empty lines.", lineNumber + 1 - emptyLines);

					emptyLines = 0;
				}
				else
				{
					emptyLines++;
				}

				lineNumber++;
			}

			if (emptyLines >= 2)
				ReportLine($"{emptyLines} consecutive empty lines.", lineNumber + 1 - emptyLines);
		}
	}
}