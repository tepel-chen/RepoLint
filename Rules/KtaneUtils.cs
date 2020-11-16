using AngleSharp.Dom;

namespace RepoLint.Rules
{
	internal class KtaneUtils : HTMLRule
	{
		protected override void HTML(IDocument document)
		{
			if (document.QuerySelector("head script[src=\"js/ktane-utils.js\"]") == null)
				Report("<script src=\"ktane-utils.js\"> should be included.");
		}
	}
}