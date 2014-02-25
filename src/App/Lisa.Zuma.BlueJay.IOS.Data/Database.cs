using System;
using System.IO;

namespace Lisa.Zuma.BlueJay.IOS.Data
{
	public class Database
	{
		private string pathToDatabase;

		public Database ()
		{
			// Figure out where the SQLite database will be.
			var documents = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			pathToDatabase = Path.Combine(documents, "BlueJay_DB.db");

			using (var db = new SQLite.SQLiteConnection(pathToDatabase))
			{
				db.CreateTable<Dosier>();
				db.CreateTable<Notes>();
				db.CreateTable<Profile>();
				db.CreateTable<User>();
			}
		}


	}
}

