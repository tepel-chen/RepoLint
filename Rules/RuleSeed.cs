using System.Linq;
using AngleSharp.Dom;

namespace RepoLint.Rules
{
	internal class RuleSeed : HTMLRule
	{
		protected override void HTML(IDocument document)
		{
			bool ruleseedScript = document.QuerySelector("script[src=\"js/ruleseed.js\"]") != null;
			bool functions = document.QuerySelectorAll("script").Any(script => script.TextContent.Contains("setRules") && script.TextContent.Contains("setDefaultRules"));
			if (ruleseedScript && !functions)
				Report("ruleseed.js was included but setRules and setDefaultRules functions were not.");
			else if (!ruleseedScript && functions)
				Report("setRules and setDefaultRules functions were included but ruleseed.js was not.");
		}
	}
}