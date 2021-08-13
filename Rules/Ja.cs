using AngleSharp.Dom;
using System.Text.RegularExpressions;

namespace RepoLint.Rules
{
	internal class Ja : HTMLRule
	{
		protected override void HTML(IDocument document)
		{
      if(!Regex.IsMatch(File.Name, @"\(日本語 — .+\)")) {
        return;
      }
      if(document.QuerySelector("html").GetAttribute("lang") != "ja") {
        Report("<html>にlang=\"ja\"が必要です");
      }
      if(document.QuerySelector("link[href=\"css/font-japanese.css\"]") == null) {
        Report("<link rel=\"stylesheet\" type=\"text/css\" href=\"css/font-*.css\">がありません");
      }
		}
	}
}