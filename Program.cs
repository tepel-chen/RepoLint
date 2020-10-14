using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using AngleSharp.Dom;

namespace RepoLint
{
	internal static class Program
    {
		private static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

			if (args.Length == 1)
			{
				var path = args[0];
				if (Directory.Exists(path))
				{
					ScanRepo(path);
					return;
				}
				else if (File.Exists(path) && Path.GetExtension(path) == ".zip")
				{
					ScanZip(path);
					return;
				}
			}

			Console.Error.WriteLine("Pass either the repo or zip file to scan.");
			Environment.ExitCode = 1;
        }

		private static void ScanZip(string zipPath)
		{
			try
			{
				ZipFile.ExtractToDirectory(zipPath, "tempZip");
				LintDirectory("tempZip", GetRules(new[] { "FourIndentHTML" }));
			}
			catch(Exception exception)
			{
				Console.Error.WriteLine("Failed to scan zip:");
				Console.Error.WriteLine(exception);
				Environment.ExitCode = 1;
			}
			finally
			{
				Directory.Delete("tempZip", true);
			}
		}

		private static readonly string[] Folders = new[] { "HTML", "JSON", "Icons" };
		private static void ScanRepo(string repoDir)
		{
            foreach (var folder in Folders)
            {
                LintDirectory(Path.Combine(repoDir, folder), GetRules(new[] { "FourIndentHTML", "W3CValidator", "FontFamily" }), rootPath: repoDir);
            }
		}

        private static void LintDirectory(string path, Rule[] rules, string rootPath = null)
        {
			if (rootPath == null)
				rootPath = path;

            foreach (string file in Directory.GetFiles(path, "*.*", SearchOption.AllDirectories))
            {
                string extension = Path.GetExtension(file);
                var applicableRules = rules.Where(rule => rule.Extensions.Contains(extension)).ToArray();
                if (applicableRules.Length == 0)
                    continue;

                List<string> problems = new List<string>();
                foreach (Rule rule in applicableRules)
                {
                    rule.AddProblems(file, rootPath, problems);

                    // Stop after 15 problems, since we only display 15.
                    if (problems.Count > 15)
                        break;
                }

                if (problems.Count == 0)
                    continue;

                Console.WriteLine($"{Path.GetRelativePath(path, file)}: ({problems.Count} problem{(problems.Count != 1 ? "s" : "")})");
                for (int i = 0; i < Math.Min(15, problems.Count); i++)
                    Console.WriteLine("    " + problems[i]);

                if (problems.Count > 15)
                    Console.WriteLine("    ... 15 limit ...");
            }
        }

		private static Rule[] GetRules(string[] ignoredRules) => GetRules(name => !ignoredRules.Contains(name));
		private static Rule[] GetRules(string onlyRule) => GetRules(name => name == onlyRule);

		private static Rule[] GetRules(Func<string, bool> ruleFilter) => Assembly
				.GetExecutingAssembly()
				.GetTypes()
				.Where(type => typeof(Rule).IsAssignableFrom(type) && !type.IsAbstract && ruleFilter(type.Name))
				.Select(type => (Rule) Activator.CreateInstance(type))
				.ToArray();
    }
}
