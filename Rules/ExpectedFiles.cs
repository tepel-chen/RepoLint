using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace RepoLint.Rules
{
	internal class ExpectedFiles : Rule
	{
		public ExpectedFiles() : base(".html") {}

		protected override void Lint()
		{
			var moduleName = Regex.Match(File.GetNameWithoutExtension(), @"^([A-Z\d❖][!-~]* ?)+").Value.Trim();
			var files = new List<string>();

			foreach (var file in Directory.EnumerateFiles(RootPath, moduleName + "*.*", SearchOption.AllDirectories))
			{
				if (file == File.FullName)
					continue;

				files.Add(file);

				if (files.Count == 2)
					break;
			}

			if (files.Count < 2)
				Report($"Expected 2 other files (SVG and JSON) with the same name \"{moduleName}\", found {files.Count}: " + string.Join(", ", files.Select(file => Path.GetRelativePath(RootPath, file))));
		}
	}
}