using AngleSharp.Dom;
using AngleSharp.Html.Dom;

namespace RepoLint.Rules
{
	internal class ConsecutiveBRs : HTMLRule
	{
		protected override void HTML(IDocument document)
		{
			foreach (var element in document.QuerySelectorAll<IHtmlBreakRowElement>("br"))
			{
				var prevSibling = element.PreviousSibling;
				while (prevSibling != null && prevSibling.NodeType == NodeType.Text && string.IsNullOrWhiteSpace(prevSibling.TextContent))
					prevSibling = prevSibling.PreviousSibling;

				if (prevSibling?.NodeName == "BR")
					ReportElement("Avoid using consecutive <br> tags.", element);
			}
		}
	}
}