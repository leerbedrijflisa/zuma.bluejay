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
		private User ReturnUserLoggedIn;

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

			CreateDummyInfo ();
		}

		public void CreateDummyInfo()
		{
			var count = db.Query<Dosier>("SELECT * FROM Dosier");

			if (count.Count == 0) {
				db.Insert (new Dosier{Name = "Martijn"});
			}

 			var count2 = db.Query<User>("SELECT * FROM User");

			if (count2.Count == 0) {
				db.Insert (new User{Role = 1, Name = "Marie-antoinette"});
				db.Insert (new User{Role = 2, Name = "Debbie"});
			}
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
			var Result = db.Query<Notes>("SELECT * FROM Notes WHERE DosierID='"+id+"' ORDER BY ID DESC");

			return Result;
		}

		public User GetUserById(int id)
		{
			var Result = db.Query<User>("SELECT * FROM User WHERE ID='"+id+"'");

			foreach (var Query in Result) {
				ReturnUser = Query;
			}

			return ReturnUser;
		}
		public User GetCurrentUser()
		{
			var Result = db.Query<User>("SELECT * FROM User WHERE LoggedIn=1");

			foreach (var Query in Result) {
				ReturnUserLoggedIn = Query;
			}
			return ReturnUserLoggedIn;
			
		}

		public Notes GetMediaFromNoteByID(int id)
		{
			var Result = db.Query<Notes>("SELECT * FROM Notes WHERE ID='"+id+"' LIMIT 1");

			foreach (var Query in Result) {
				ReturnNote = Query;
			}
			return ReturnNote;
		}

		public void InsertNote(Notes note)
		{
			var user = this.GetCurrentUser ();

			note.OwnerID = user.ID;

			db.Insert (note);
		}

	}
}

