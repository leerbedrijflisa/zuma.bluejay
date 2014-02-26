using System;
using Lisa.Zuma.BlueJay.IOS.Data;

namespace Lisa.Zuma.BlueJay.IOS.Models
{
	public class TemplateParser
	{
		private Database db;
		private string ParentHTML;

		public TemplateParser ()
		{
			db = new Database ();
		}

		public string ParseTimelineFromDosier(int id)
		{
			ParentHTML = System.IO.File.ReadAllText("HTML/timeline/index.html");
			Console.WriteLine (ParentHTML);

			return ParentHTML;
		}

		private string ParseTimelineItems()
		{
			return "";
		}

		private string ParseItemMedia()
		{
			return "";
		}

	}
}

