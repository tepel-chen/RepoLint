namespace RepoLint.Rules
{
	internal class ParentFolder : Rule
	{
		public ParentFolder() : base(".html .json .css") {}

		protected override void Lint()
		{
			var parentFolder = File.Directory.Name;

			switch (File.Extension)
			{
				case ".html" when parentFolder != "HTML":
					Report(".html files should be in the \"HTML\" folder.");
					break;
				case ".json" when parentFolder != "JSON":
					Report(".json files should be in the \"JSON\" folder.");
					break;
			}
		}
	}
}