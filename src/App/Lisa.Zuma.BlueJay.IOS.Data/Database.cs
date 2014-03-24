using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using RestSharp;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Lisa.Zuma.BlueJay.IOS.Data
{
	public class Database
	{
		private string pathToDatabase;
		private SQLite.SQLiteConnection db;
		private UserData ReturnUser;
		private NotesData ReturnNote;
		private UserData ReturnUserLoggedIn;
		private RestClient client;

		public Database ()
		{


			var documents = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			Console.WriteLine (documents);
			pathToDatabase = Path.Combine(documents, "BlueJay_DB.db");
			db = new SQLite.SQLiteConnection (pathToDatabase);

			db.CreateTable<DosierData>();
			db.CreateTable<NotesData>();
			db.CreateTable<ProfileItemsData>();
			db.CreateTable<UserData>();
			//db.DropTable<TemporaryItemMedia> ();
			db.CreateTable<TemporaryItemMediaData> ();


			CreateDummyInfo ();
		}

		public void CreateDummyInfo()
		{
			var count = db.Query<DosierData>("SELECT * FROM DosierData");

			if (count.Count == 0) {
				db.Insert(new DosierData{Name = "Martijn"});
			}

			var count2 = db.Query<UserData>("SELECT * FROM UserData");

			if (count2.Count == 0) {
				db.Insert (new UserData{Role = 1, Name = "Marie-antoinette"});
				db.Insert (new UserData{Role = 2, Name = "Debbie"});
			}
		}

		public void DummyLoggedIn(int id)
		{
			db.Query<UserData>("UPDATE UserData SET LoggedIn = 0");
			db.Query<UserData>("UPDATE UserData SET LoggedIn = 1 WHERE ID = '"+ id +"'");
		}

		public List<DosierData> GetDosierDatas(int id)
		{
			var Result = db.Query<DosierData>("SELECT * FROM DosierData WHERE ID='"+id+"'");

			return Result;
		}

		public List<DosierData> GetAllDosierDatas()
		{
			var result = db.Query<DosierData>("SELECT * FROM DosierData");

			return result;
		}

		public List<NotesData> GetNotesDataFromDosierData(int id)
		{
			var Result = db.Query<NotesData>("SELECT * FROM NotesData WHERE DosierDataID='"+id+"' ORDER BY ID DESC");
			//			db.Table<NotesData> ().Where(t => t.ID == id).OrderByDescending(t => t.ID);
			//
			//			var result = from note in db.Table<NotesData>()
			//				where note.DosierDataID == id
			//				orderby note.ID descending
			//				select new { Name = note.ID.ToString() };

			return Result;
		}

		public UserData GetUserById(int id)
		{
			var Result = db.Query<UserData>("SELECT * FROM UserData WHERE ID='"+id+"'");

			foreach (var Query in Result) {
				ReturnUser = Query;
			}

			return ReturnUser;
		}
		public UserData GetCurrentUser()
		{
			var Result = db.Query<UserData>("SELECT * FROM UserData WHERE LoggedIn=1");

			foreach (var Query in Result) {
				ReturnUserLoggedIn = Query;
			}
			return ReturnUserLoggedIn;

		}

		public NotesData GetMediaFromNoteByID(int id)
		{
			var Result = db.Query<NotesData>("SELECT * FROM NotesData WHERE ID='"+id+"' LIMIT 1");

			foreach (var Query in Result) {
				ReturnNote = Query;
			}
			return ReturnNote;
		}

		public void InsertNote(NotesData note)
		{
			db.Insert (note);
		}

		public void InsertProfileItem (ProfileItemsData newItem)
		{
			newItem.ProfileID = 1;
			db.Insert (newItem);
		}

		public List<ProfileItemsData> GetProfileItemsByProfileID (int profileId)
		{
		var result = db.Query<ProfileItemsData> ("SELECT * FROM ProfileItemsData WHERE ProfileID='" + profileId + "'");

			return result;
		}

		public void Insert<T>(T item) where T : class
		{
			if(TableExists(typeof(T))) 
			{
				db.Insert (item);
			}
		}

		public void Update<T>(T item) where T : class 
		{
			if (TableExists (typeof(T))) 
			{
				db.Update (item);
			}
		}

		public void DeleteAllNotesForSync()
		{
			db.Query<NotesData> ("DELETE FROM NotesData");
		}

		public void InsertNewTemporaryMediaItem (TemporaryItemMediaData item)
		{
			db.Insert(item);
		}

		public void DeleteAllTemporaryMediaItems ()
		{
			db.Query<TemporaryItemMediaData> ("DELETE FROM TemporaryItemMediaData");
		}

		public List<TemporaryItemMediaData> ReturnAllTemporaryMediaItems ()
		{
		var Result = db.Query<TemporaryItemMediaData> ("SELECT * FROM TemporaryItemMediaData");

			return Result;
		}

		public readonly IList<string> GetPickerItems = new List<string> {
			"Algemeen",
			"Medicijngebruik",
			"Allergieën",
			"Contactgegevens",
			"Hulpmiddelen",
			"Verzorging"
		};

		private bool TableExists<T>(T item) where T : class 
		{
			return TableExists (typeof(T));
		}

		private bool TableExists(Type type) 
		{
			return db.TableMappings.Count (m => m.MappedType == type) > 0;
		}
	}
}

