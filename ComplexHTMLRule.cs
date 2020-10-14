using System;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using AngleSharp.Io;

namespace RepoLint
{
	/// <summary>Similar to HTMLRule but loads file resources. Because of that, it's also slower.</summary>
	internal abstract class ComplexHTMLRule : Rule
	{
		protected ComplexHTMLRule() : base(".html") {}

		protected override void Lint()
		{
			var config = Configuration.Default
				.WithCss()
				.With(new FileRequester())
				.WithDefaultLoader(new LoaderOptions {
					IsResourceLoadingEnabled = true,
					Filter = (req) => {
						var path = req.Address.Path;
						return path.EndsWith(".html") || path.EndsWith(".css");
					}
				})
				.With(new HtmlParser(new HtmlParserOptions { IsKeepingSourceReferences = true }));
			var context = BrowsingContext.New(config);

			var document = context
				.OpenAsync(resp => resp.Address(new Url("file:///" + File.FullName)).Content(Content))
				.Result;

			HTML(document);
		}

		protected abstract void HTML(IDocument document);

		protected void ReportElement(string problem, IElement element)
		{
			ReportLine(problem, element.SourceReference.Position.Line);
		}
	}
}