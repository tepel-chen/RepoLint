using System;
using AngleSharp.Dom;
using AngleSharp.Css.Dom;
using System.Linq;

namespace RepoLint.Rules
{
	internal class FontFamily : ComplexHTMLRule
	{
		private static readonly string[] genericFonts = { "serif", "sans-serif", "monospace", "cursive", "fantasy", "system-ui", "inherit" };

		protected override void HTML(IDocument document)
		{
			var fonts = document.StyleSheets.GetRules<ICssFontFaceRule>().Select(rule => rule.Family.Trim('"')).Distinct().Concat(genericFonts);

			foreach (var rule in document.StyleSheets.GetRules<ICssStyleRule>())
			{
				var fontFamily = rule.Style.GetPropertyValue("font-family");
				if (string.IsNullOrEmpty(fontFamily))
					continue;

				if (!fontFamily.Split(',').Select(font => font.Trim().Trim('"')).Any(fonts.Contains))
					ReportElement($"Font family \"{fontFamily}\" doesn't match any included fonts.", rule.Owner.OwnerNode);
			}
		}
	}
}