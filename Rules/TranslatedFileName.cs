﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace RepoLint.Rules
{
	internal class TranslatedFileName : Rule
	{
		public TranslatedFileName() : base(".html") { }

		static TranslatedFileName()
		{
			try
			{
				var httpClient = new HttpClient();
				var response = httpClient.GetAsync("https://ktane.timwi.de/json/raw").Result;
				if (!response.IsSuccessStatusCode)
					return;

				var jsonString = response.Content.ReadAsStringAsync().Result;
				var modules = JsonSerializer.Deserialize<WebsiteJSON>(jsonString).KtaneModules;
				moduleNames = modules.ConvertAll(module => module.FileName != null ? module.FileName : module.Name);
			}
			catch(Exception e)
			{
        Console.Error.WriteLine(e);
				Console.Error.WriteLine("Failed to load the repository.");
			}
		}

		static readonly List<string> moduleNames;

		class WebsiteJSON
		{
			public List<KtaneModule> KtaneModules { get; set; }

			public class KtaneModule
			{
				public string FileName { get; set; }
				public string Name { get; set; }
			}
		}

		protected override void Lint()
		{
      var filename = File.GetNameWithoutExtension();

      if(!Regex.IsMatch(filename, @"([A-Z\d❖][!-~]*) translated ") && !Regex.IsMatch(filename, @"\(.+ — .+\)") ) return;

			var moduleName = Regex.Match(filename, @"^(.+) translated").Groups[1].ToString();

			if (moduleNames != null && !moduleNames.Contains(moduleName)) {
        Report($"No original module named \"{moduleName}\" found");
      }

      if (!Regex.IsMatch(filename, @"^" + moduleName + @" translated \(.+ — .+\)( [^()]+)?( \([^()]+\))?$")) {
         Report($"Invalid file name: expected \"(.+) translated \\(.+ — .+\\)( [^()]+)?( \\([^()]+\\))?.html\"");
      }
		}
	}
}