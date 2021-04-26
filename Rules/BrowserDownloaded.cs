using AngleSharp.Dom;

namespace RepoLint.Rules
{
	internal class BrowserDownloaded : HTMLRule
	{
		protected override void HTML(IDocument document)
		{
			if (document.QuerySelector("input#highlighter-enabled[type=\"checkbox\"]") != null)
			{
				Report("Don't save pages from the browser using the \"complete\" option. Use the \"HTML only\" option or get it from the GitHub repository.");
			}
		}
	}
}