using System;
using System.IO;
using System.Collections.Generic;

namespace Lisa.Zuma.BlueJay.IOS.Data
{
	public class Database
	{
		private string pathToDatabase;
		private SQLite.SQLiteConnection db;
		private User ReturnUser;
		private Notes ReturnNote;

		public Database ()
		{
			var documents = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			Console.WriteLine (documents);
			pathToDatabase = Path.Combine(documents, "BlueJay_DB.db");

			db = new SQLite.SQLiteConnection (pathToDatabase);

			db.CreateTable<Dosier>();
			db.CreateTable<Notes>();
			db.CreateTable<Profile>();
			db.CreateTable<User>();

		}

		public void DummyLoggedIn(int id)
		{
			db.Query<User>("UPDATE User SET LoggedIn = 0");
			db.Query<User>("UPDATE User SET LoggedIn = 1 WHERE ID = '"+ id +"'");
		}

		public List<Dosier> GetDosiers(int id)
		{
			var Result = db.Query<Dosier>("SELECT * FROM Dosier WHERE ID='"+id+"'");

			return Result;
		}

		public List<Notes> GetNotesFromDosier(int id)
		{
			var Result = db.Query<Notes>("SELECT * FROM Notes WHERE ID='"+id+"'");

			return Result;
		}

		public User GetUserById(int id)
		{
			var Result = db.Query<User>("SELECT * FROM User WHERE id='"+id+"'");

			foreach (var Query in Result) {
				ReturnUser = Query;
			}

			return ReturnUser;
		}

		public Notes GetMediaFromNoteByID(int id)
		{
			var Result = db.Query<Notes>("SELECT * FROM Notes WHERE id='"+id+"' LIMIT 1");

			foreach (var Query in Result) {
				ReturnNote = Query;
			}
			return ReturnNote;
		}

	}
}

