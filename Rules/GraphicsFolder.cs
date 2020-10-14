using System;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;

namespace RepoLint.Rules
{
	internal class GraphicsFolder : HTMLRule
	{
		protected override void HTML(IDocument document)
		{
			foreach (var image in document.QuerySelectorAll<IHtmlImageElement>("img"))
			{
				if (image.Source == null)
					continue;

				if (!Uri.TryCreate(image.Source, UriKind.Absolute, out Uri result))
					return;

				var segments = result.Segments;
				if (segments.Length == 3 && segments[1] == "img/")
					ReportElement("Graphics must be in the appropriate folder named for the module.", image);
			}
		}
	}
}