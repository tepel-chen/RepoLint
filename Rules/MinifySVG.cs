namespace RepoLint.Rules
{
	internal class MinifySVG : Rule
	{
		public MinifySVG() : base(".svg") {}

		protected override void Lint()
		{
			if ((File.Directory.Name == "Component" || SingleFile) && Content.Contains('\n'))
				Report("Component SVGs should be minified with SVGOMG.");
		}
	}
}