namespace RepoLint.Rules
{
	internal class DOCTYPE : Rule
	{
		public DOCTYPE() : base(".html") {}

		protected override void Lint()
		{
			if (!Content.StartsWith("<!DOCTYPE html>"))
				ReportLine("HTML files must start with <!DOCTYPE html>.", 1);
		}
	}
}