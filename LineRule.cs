namespace RepoLint
{
	internal abstract class LineRule : Rule
	{
		protected LineRule(string extension) : base(extension) {}

		private int lineNumber;

		protected override void Lint()
		{
			string[] lines = Content.Split("\n");
			for (int i = 0; i < lines.Length; i++)
			{
				lineNumber = i + 1;
				Line(lines[i]);
			}
		}

		protected abstract void Line(string line);

		protected void ReportLine(string problem)
		{
			ReportLine(problem, lineNumber);
		}
	}
}