using System.Linq;

namespace RepoLint.Rules
{
	internal class ParentFolder : Rule
	{
		public ParentFolder() : base(".html .json .css .svg") { }

		readonly string[] repoFiles = new[] { "Bomb.svg", "BombSide.svg", "repo-logo-unminified.svg", "repo-logo.svg", "twitch.svg" };

		protected override void Lint()
		{
			if (repoFiles.Contains(File.Name))
				return;

			var parentFolder = File.Directory.Name;

			switch (File.Extension)
			{
				case ".html" when parentFolder != "HTML":
					Report(".html files should be in the \"HTML\" folder.");
					break;
				case ".json" when parentFolder != "JSON":
					Report(".json files should be in the \"JSON\" folder.");
					break;
				case ".svg":
					var parent = File.Directory.Parent;
					while (parent != null && parent.Name != "img")
						parent = parent.Parent;

					if (parent != null)
						break;

					Report(".svg files should be in a folder that's inside the \"img\" folder.");
					break;
			}
		}
	}
}