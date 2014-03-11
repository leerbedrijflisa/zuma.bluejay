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

		public void SetNewNote (string text)
		{
			var Note = new Notes{DosierID = 1, Text = text, Media = new List<Media>()};

			foreach (var x in this.GetAllDataElements()) {
				Note.Media.Add (new Media{Location = x.Path});
			}

			database.InsertNote (Note);
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

		public void InsertNewDataElement(int type, string path)
		{
			database.InsertNewTemporaryMediaItem (new TemporaryItemMedia{Type = type, Path = path});
		}

		public void DeleteAllDataElements()
		{
			database.DeleteAllTemporaryMediaItems ();
		}

		private List<TemporaryItemMedia> GetAllDataElements()
		{
			return database.ReturnAllTemporaryMediaItems (); 
		}

	}
}

