using System;
using Lisa.Zuma.BlueJay.IOS.Data;
using System.Collections.Generic;

namespace Lisa.Zuma.BlueJay.IOS.Models
{
	public class DataHelper
	{
		private Database database;
		private HTMLTemplates templateParser;

		public DataHelper ()
		{
			database = new Database ();
			templateParser = new HTMLTemplates ();
		}

		public void SignIn(int id)
		{
			database.DummyLoggedIn (id);
		}

		public void SetNewNote (string text, string media)
		{
			database.InsertNote (new Notes{DosierID = 1, Text = text});
		}

		public IList<string> picker()
		{
			return database.colors;
		}

		public void SyncNotesByID(int id, Action DoSomething)
		{
			database.SyncAllNotesFromDosier(id, ()=>{

				DoSomething();

			});
		}
	}
}

