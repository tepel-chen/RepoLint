namespace RepoLint.Rules
{
	internal class TwoIndentJSON : LineRule
	{
		public TwoIndentJSON() : base(".json") {}

		protected override void Line(string line)
		{
			for (int i = 0; i < line.Length; i++)
			{
				if (line[i] != ' ')
				{
					if (i % 2 != 0)
						ReportLine($"{i} spaces, expected a multiple of 2.");

					break;
				}
			}
		}
	}
}