using AngleSharp.Dom;

namespace RepoLint.Rules
{
	internal class HeadTag : HTMLRule
	{
		private readonly ElementHTML[] elements =
		{
			new ElementHTML("<meta charset=\"utf-8\">"),
			new ElementHTML("<meta name=\"viewport\" content=\"initial-scale=1\">"),
			new ElementHTML("<meta *>", pattern: true, optional: true),
			new ElementHTML("<title>*—*</title>", pattern: true),
			new ElementHTML("<link rel=\"stylesheet\" type=\"text/css\" href=\"css/font.css\">"),
			new ElementHTML("<link rel=\"stylesheet\" type=\"text/css\" href=\"css/font-*.css\">", pattern: true, optional: true),
			new ElementHTML("<link rel=\"stylesheet\" type=\"text/css\" href=\"font/*/font.css\">", pattern: true, optional: true),
			new ElementHTML("<link rel=\"stylesheet\" type=\"text/css\" href=\"css/normalize.css\">"),
			new ElementHTML("<link rel=\"stylesheet\" type=\"text/css\" href=\"css/main.css\">"),
			new ElementHTML("<link rel=\"stylesheet\" type=\"text/css\" href=\"css/*/*.css\">", pattern: true, optional: true),
			new ElementHTML("<script src=\"js/ktane-utils.js\"></script>"),
		};

		protected override void HTML(IDocument document)
		{
			var children = document.Head.Children;
			var childNumber = 0;
			var childOffset = 0;

			if (children.Length == 0)
				return;

			for (int i = 0; i < elements.Length; i++)
			{
				var element = elements[i];
				var child = childNumber >= children.Length ? null : children[childNumber];
				var matches = element.Matches(child);
				if (!matches && !element.Optional)
				{
					var childOrFallback = children[child == null ? children.Length - 1 : childNumber];
					ReportElement($"Expected “{element.Text}” as the {Nth(childNumber + childOffset + 1)} child element in the <head>.", childOrFallback);
					childOffset++;
				}

				if (matches)
					childNumber++;

				if (matches && element.Optional)
					childOffset--;
			}
		}

		private string Nth(int number)
		{
			var nthNumber = number > 20 ? number % 10 : number;

			var suffix = nthNumber switch
			{
				1 => "st",
				2 => "nd",
				3 => "rd",
				_ => "th"
			};

			return number + suffix;
		}

		private class ElementHTML
		{
			public string Text;
			public bool Pattern;
			public bool Optional;

			public ElementHTML(string text, bool pattern = false, bool optional = false)
			{
				Text = text;
				Pattern = pattern;
				Optional = optional;
			}

			public bool Matches(IElement element)
			{
				if (element == null)
					return false;

				var outer = element.OuterHtml;
				return Pattern ? outer.Like(Text) : outer == Text;
			}
		}
	}
}