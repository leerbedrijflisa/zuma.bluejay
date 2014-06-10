using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using RestSharp;
using SQLite;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Lisa.Zuma.BlueJay.IOS.Data
{
	public class Database
	{
		private string pathToDatabase;
		private SQLiteConnection db;
		private UserData ReturnUser;
		private NotesData ReturnNote;
		private UserData ReturnUserLoggedIn;
		public string accessToken;

		public Database ()
		{


			var documents = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			Console.WriteLine (documents);
			pathToDatabase = Path.Combine(documents, "BlueJay_DB.db");
			db = new SQLite.SQLiteConnection (pathToDatabase);

			db.CreateTable<DossierData>();
			db.CreateTable<NotesData>();
			db.CreateTable<ProfileItemsData>();
			db.CreateTable<UserData> ();
			db.CreateTable<TemporaryItemMediaData> ();
			db.CreateTable<CurrentDossier> ();
			AccessToken ();
		}

		public void CreateDummyInfo()
		{
			var count = db.Query<DossierData>("SELECT * FROM DossierData");

			if (count.Count == 0) {
				db.Insert(new DossierData{Name = "Martijn"});
			}

		}

		public void AccessToken()
		{
			var token = db.Table<UserData> ();
			foreach (var accesstoken in token) {
				accessToken = accesstoken.AccesToken;
			}
		}

		public string getCurrentUserId()
		{
			var result = db.Table<UserData> ();

			return result.First ().userId;
		}

		public void setCurrentDossier(int id)
		{
			db.Query<CurrentDossier> ("DELETE FROM CurrentDossier");

			var result = db.Query<DossierData>("SELECT * FROM DossierData WHERE DossierId =" + id + " LIMIT 1");

			foreach (var res in result) {
				db.Insert (new CurrentDossier{ currentDossier = res.DossierId });
			}
		}

		int ReturnInt;

		public int getCurrentDossier()
		{


			var result = db.Query<CurrentDossier> ("SELECT * FROM CurrentDossier ORDER BY Id DESC LIMIT 1");
	
			foreach(var value in result)
			{
				ReturnInt = value.currentDossier;
			}

			return ReturnInt;

		}

		public void DummyLoggedIn(int id)
		{
			db.Query<UserData>("UPDATE UserData SET LoggedIn = 0");
			db.Query<UserData>("UPDATE UserData SET LoggedIn = 1 WHERE ID = '"+ id +"'");
		}

		public List<DossierData> GetDosierDatas(int id)
		{
			var Result = db.Query<DossierData>("SELECT * FROM DossierData WHERE ID='"+id+"'");

			return Result;
		}

		public List<DossierData> GetAllDossierDatas()
		{
			var result = db.Query<DossierData>("SELECT * FROM DossierData");

			return result;
		}

		public IEnumerable<NotesData> GetNotesDataFromDosierData(int id)
		{
			var Result = db.Query<NotesData>("SELECT * FROM NotesData WHERE DossierDataID='"+id+"' ORDER BY ID DESC");
			var result = db.Table<NotesData> ().Where(t => t.DossierDataID == id).OrderByDescending(t => t.ID);

			return result;
		}

		public void deleteDossiers()
		{
			db.Query<DossierData> ("DELETE FROM DossierData");
		}

		public UserData GetUserById(int id)
		{
			var Result = db.Query<UserData>("SELECT * FROM UserData WHERE ID='"+id+"'");

			foreach (var Query in Result) {
				ReturnUser = Query;
			}

			return ReturnUser;
		}

		DossierData dossierData;

		public DossierData GetCurrentDossier()
		{
			var result = db.Query<DossierData> ("SELECT * FROM DossierData WHERE DossierId = " + getCurrentDossier());

			dossierData = new DossierData ();

			foreach (var res in result) {
				dossierData.Name = res.Name;
				dossierData.DossierId = res.DossierId;
			}

			return dossierData;
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
			//newItem.ProfileID = 1
			db.Insert (newItem);
		}

		public List<ProfileItemsData> GetProfileItemsByProfileID (int profileId)
		{
		var result = db.Query<ProfileItemsData> ("SELECT * FROM ProfileItemsData WHERE ProfileID='" + profileId + "'");

			return result;
		}

		public IEnumerable<ProfileItemsData> GetProfileItemsByDossierId(int dossierId) {
		
			var result = db.Query<ProfileItemsData> ("SELECT * FROM ProfileItemsData WHERE DossierDataID = '" + dossierId + "'");

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

		public void Clear(string tableName)
		{
			db.Execute ("DELETE FROM " + tableName);
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

//		public void ClearLockScreenInformation ()
//		{
//			db.Query<LockScreenData> ("DELETE FROM LockScreenData");
//		}

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

