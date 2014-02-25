using System;
using System.IO;
using System.Collections.Generic;

namespace Lisa.Zuma.BlueJay.IOS.Data
{
	public class Database
	{
		private string pathToDatabase;
		private SQLite.SQLiteConnection db;

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

			db.Insert (new Dosier { Name = "Martijn Olga" });

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
	}
}

