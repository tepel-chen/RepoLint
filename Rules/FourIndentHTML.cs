namespace RepoLint.Rules
{
	internal class FourIndentHTML : LineRule
	{
		public FourIndentHTML() : base(".html") {}

		protected override void Line(string line)
		{
			for (int i = 0; i < line.Length; i++)
			{
				if (line[i] != ' ')
				{
					if (i % 4 != 0)
						ReportLine($"{i} spaces, expected a multiple of 4.");

					break;
				}
			}
		}
	}
}