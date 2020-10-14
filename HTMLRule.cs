using AngleSharp.Dom;
using AngleSharp.Html.Parser;

namespace RepoLint
{
	internal abstract class HTMLRule : Rule
	{
		protected HTMLRule() : base(".html") {}

		private static string previousContent;
		private static IDocument previousDocument;

		private static readonly HtmlParser parser = new HtmlParser(new HtmlParserOptions
		{
			IsKeepingSourceReferences = true,
		});

		protected override void Lint()
		{
			if (previousContent == Content)
			{
				HTML(previousDocument);
				return;
			}

			var document = parser.ParseDocument(Content);
			previousDocument = document;
			previousContent = Content;

			HTML(document);
		}

		protected abstract void HTML(IDocument document);

		protected void ReportElement(string problem, IElement element)
		{
			ReportLine(problem, element.SourceReference.Position.Line);
		}
	}
}