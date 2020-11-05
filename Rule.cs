using System.Collections.Generic;
using System.IO;

namespace RepoLint
{
	internal abstract class Rule
	{
		protected Rule(string extension)
		{
			Extensions = extension.Split(" ");
			name = GetType().Name;
		}

		public string[] Extensions;
		public List<string> Problems;
		private readonly List<GroupedProblem> GroupedProblems = new List<GroupedProblem>();

		public static FileInfo File;
		private static string lastPath;

		public static string RootPath;
		public static string Content => contentCache ??= System.IO.File.ReadAllText(File.FullName);
		private static string contentCache;

		private readonly string name;

		public void Reset()
		{
			Problems = null;
			GroupedProblems.Clear();
		}

		public void AddProblems(string path, string rootPath, List<string> allProblems)
		{
			if (lastPath != path)
			{
				File = new FileInfo(path);
				contentCache = null;
				RootPath = rootPath;
			}
			lastPath = path;

			Lint();

			foreach (var groupedReport in GroupedProblems)
				Report(groupedReport.ToString());

			if (Problems != null)
				allProblems.AddRange(Problems);

			Reset();
		}

		protected abstract void Lint();

		protected void Report(string problem) => (Problems ??= new List<string>()).Add($"{problem} ({name})");

		protected void ReportLine(string problem, long lineNumber)
		{
			var previous = GroupedProblems.Find(groupedProblem => lineNumber >= groupedProblem.Start - 1 && lineNumber <= groupedProblem.End + 1 && groupedProblem.Problem == problem);
			if (previous != null)
			{
				previous.End++;
				return;
			}

			GroupedProblems.Add(new GroupedProblem() {
				Problem = problem,
				Start = lineNumber,
				End = lineNumber
			});
		}

		private class GroupedProblem
		{
			public string Problem;
			public long Start;
			public long End;

			public override string ToString()
			{
				if (Start == End)
					return $"Line {Start}: {Problem}";

				return $"Lines {Start}-{End}: {Problem}";
			}
		}
	}
}