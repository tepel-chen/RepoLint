namespace RepoLint.Rules
{
	internal class NoTextSVG : Rule
	{
		public NoTextSVG() : base(".svg") {}

		protected override void Lint()
		{
			if (File.Directory.Name == "Component" && Content.Contains("<text "))
				Report("<text> elements should be converted to <path> elements.");
		}
	}
}