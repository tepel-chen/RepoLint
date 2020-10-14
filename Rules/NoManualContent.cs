using AngleSharp.Dom;

namespace RepoLint.Rules
{
	internal class NoManualContent : HTMLRule
	{
		protected override void HTML(IDocument document)
		{
			var manualContentNode = document.QuerySelector("div[id=\"ManualContent\"]");
			if (manualContentNode != null)
				ReportElement("<div id=\"ManualContent\"> wrapper element should be removed.", manualContentNode);
		}
	}
}