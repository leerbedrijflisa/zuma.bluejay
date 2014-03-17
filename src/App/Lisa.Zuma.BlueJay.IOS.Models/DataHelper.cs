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

		public void SetNewNote (string text, Action Ready)
		{
			var Note = new NoteModel{Text = text, Media = this.GetAllDataElements()};

			database.InsertNote (Note, () => {});

			Ready ();
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
			database.InsertNewTemporaryMediaItem (new TemporaryItemMedia{Type = type, fileName = System.IO.Path.GetFileName(path), Path = path});
		}

		public List<Dosier> GetDosiers()
		{
			return database.GetAllDosiers ();
		}
	
		public List<ProfileItems> GetProfileItems()
		{
			return database.GetProfileItemsByProfileID (1);
		}

		public void DeleteAllDataElements()
		{
			database.DeleteAllTemporaryMediaItems ();
		}

		private List<NoteMediaModel> GetAllDataElements()
		{
			List<NoteMediaModel> MediaModel = new List<NoteMediaModel> ();

			foreach(var x in database.ReturnAllTemporaryMediaItems()){
				MediaModel.Add (new NoteMediaModel { Name = x.fileName });
			}

			return MediaModel;
		}

	}
}

