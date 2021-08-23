using System;
using AngleSharp.Dom;
using System.Linq;
using AngleSharp.Html.Dom;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RepoLint.Rules
{
	internal class PageNum : HTMLRule
	{
    static Dictionary<string, Func<string, string, string>> translation = new Dictionary<string, Func<string, string, string>>() {
      {"日本語", (string v, string h) => $"ページ {v}/{h}"},
      {"default", (string v, string h) => $"Page {v} of {h}"}
    };
		protected override void HTML(IDocument document)
		{
      var filename = File.GetNameWithoutExtension();
      var elements = document.QuerySelectorAll(".page");
      var langMatch = Regex.Match(filename, @"\((.+) — .+\)");
      var converter = langMatch.Length < 1 ? 
        translation["default"] : 
        translation.ContainsKey(langMatch.Groups[1].ToString()) ? translation[langMatch.Groups[1].ToString()] : null;

      for (var i = 0; i < elements.Length; i++) {
        var element = elements[i];
        var pageNumEl = element.QuerySelector(".page-footer");
        if(pageNumEl == null) {
          Report($"Page {i+1} is missing page number");
          continue;
        }
        if(converter != null && pageNumEl.TextContent != converter((i+1).ToString(), elements.Length.ToString())) {
          Report($"Page {i+1} has invalid page number; Lang {langMatch.Groups[1].ToString()}, has \"{pageNumEl.TextContent}\", expected \"{converter((i+1).ToString(), elements.Length.ToString())}\"");
        }
        
      }

		}
	}
}