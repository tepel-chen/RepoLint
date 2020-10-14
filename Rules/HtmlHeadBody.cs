using System.Collections.Generic;
using AngleSharp.Dom;

namespace RepoLint.Rules
{
	internal class HtmlHeadBody : HTMLRule
	{
		protected override void HTML(IDocument document)
		{
			var elementLines = new Dictionary<int, string>();

			void checkLine(int line, string tag)
			{
				if (elementLines.TryGetValue(line, out string conflict))
				{
					ReportLine($"{tag} shouldn't be on the same line as {conflict}.", line);
				}
				else
				{
					elementLines.Add(line, tag);
				}
			}

			foreach (var tag in new[] { "html", "head", "body" })
			{
				var element = document.QuerySelector(tag);
				if (element == null)
				{
					Report($"Missing <{tag}>.");
					break;
				}

				if (element.SourceReference.Position.Column != 1)
				{
					//ReportElement($"<{tag}> should not be indented.", element);
				}

				checkLine(element.SourceReference.Position.Line, $"<{tag}>");
				checkLine(element.SourceReference.Position.After(element.TextContent).Line, $"</{tag}>");
			}
		}
	}
}