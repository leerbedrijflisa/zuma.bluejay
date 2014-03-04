using System;
using Lisa.Zuma.BlueJay.IOS.Data;
using System.Collections.Generic;

namespace Lisa.Zuma.BlueJay.IOS.Models
{
	public class DataHelper
	{
		private Database database;

		public DataHelper ()
		{
			database = new Database ();
		}

		public void SignIn(int id)
		{
			database.DummyLoggedIn (id);
		}

		public void SetNewNote (string text, string media)
		{
			database.InsertNote (new Notes{DosierID = 1, Text = text, Media = "https://zumabluejay.blob.core.windows.net/publicfiles/e7fb2c96-e605-4fbb-bd3e-ba334e87cb4f.mp4"});
		}

		public IList<string> picker()
		{
			return database.colors;
		}
	}
}

