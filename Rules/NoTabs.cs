namespace RepoLint.Rules
{
	internal class NoTabs : LineRule
	{
		public NoTabs() : base(".html .json") {}

		protected override void Line(string line)
		{
			foreach (var character in line)
			{
				if (!char.IsWhiteSpace(character))
				{
					break;
				}
				else if (character == '\t')
				{
					ReportLine("Use spaces instead of tabs.");
					break;
				}
			}
		}
	}
}