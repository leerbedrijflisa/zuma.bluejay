using System;
using System.IO;

namespace Lisa.Zuma.BlueJay.IOS.Data
{
	public class Database
	{
		private string pathToDatabase;
		private SQLite.SQLiteConnection db;

		public Database ()
		{
			var documents = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			pathToDatabase = Path.Combine(documents, "BlueJay_DB.db");

			db = new SQLite.SQLiteConnection (pathToDatabase);

			db.CreateTable<Dosier>();
			db.CreateTable<Notes>();
			db.CreateTable<Profile>();
			db.CreateTable<User>();

			User parent = new User { Role = "Parent", Name = "Debbie"};
			User superVisor = new User { Role = "Supervisor", Name = "Marie-Antoinette"};

			db.Insert (superVisor);
			db.Insert(parent);
		}

		public void DummyLoggedIn(int id)
		{
			db.Query<User> ("UPDATE UPDATE User SET LoggedIn = 1 WHERE ID = '"+ id +"'");
		}
	}
}

