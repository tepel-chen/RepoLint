namespace RepoLint.Rules
{
	internal class TemplateManual : Rule
	{
		public TemplateManual() : base(".html .png .svg") {}

		protected override void Lint()
		{
			switch (File.Name)
			{
				case "Example Image.png":
				case "Module Name.svg":
					Report("Template Manual files should not be included.");
					break;
				case "Template Manual.html":
					Report("Manual should be renamed to the name of the module and the Template Manual should be removed.");
					break;
			}
		}
	}
}