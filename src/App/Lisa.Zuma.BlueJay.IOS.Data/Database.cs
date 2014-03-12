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
		private User ReturnUser;
		private Notes ReturnNote;
		private User ReturnUserLoggedIn;
		private RestClient client;

		public Database ()
		{
			var documents = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			Console.WriteLine (documents);
			pathToDatabase = Path.Combine(documents, "BlueJay_DB.db");

			db = new SQLite.SQLiteConnection (pathToDatabase);

			db.CreateTable<Dosier>();
			db.CreateTable<Notes>();
			db.CreateTable<Profile>();
			db.CreateTable<ProfileItems>();
			db.CreateTable<User>();
			db.DropTable<TemporaryItemMedia> ();
			db.CreateTable<TemporaryItemMedia> ();


			CreateDummyInfo ();

			client = new RestClient ("http://zumabluejay.azurewebsites.net");
		}

		public void SyncAllNotesFromDosier(int dosier, Action AsyncFunc){

			db.DropTable<Notes> ();

			var request = new RestRequest (string.Format("api/dossier/{0}/notes/", dosier), Method.GET);

			client.ExecuteAsync (request, response => {

				var callback = JsonConvert.DeserializeObject<List<NoteModel>>(response.Content);

				foreach(var Result in callback){
					var note = new Notes{DosierID = dosier, OwnerID = 1, Text = Result.Text, Media = Result.Media};
					db.Insert(note);
				}

				AsyncFunc();

			});
		}

		public void CreateDummyInfo()
		{
			var count = db.Query<Dosier>("SELECT * FROM Dosier");

			if (count.Count == 0) {
				db.Insert(new Dosier{Name = "Martijn"});
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
			//			db.Table<Notes> ().Where(t => t.ID == id).OrderByDescending(t => t.ID);
			//
			//			var result = from note in db.Table<Notes>()
			//				where note.DosierID == id
			//				orderby note.ID descending
			//				select new { Name = note.ID.ToString() };

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

		public void InsertNote(NoteModel note)
		{
			var user = this.GetCurrentUser ();


			var request = new RestRequest (string.Format("api/dossier/{0}/notes/", 1), Method.POST);

			request.RequestFormat = DataFormat.Json;
			request.AddBody(note);
			Console.WriteLine(request.ToString ());

			Console.WriteLine (request);

			client.ExecuteAsync(request, response => {

				Console.WriteLine("klaar :"+ response.Content);
				var callback = JsonConvert.DeserializeObject<NoteModel>(response.Content);

				Store(callback);
			});


			//this.DeleteAllTemporaryMediaItems ();

		}

			public void InsertProfileItem (ProfileItems newItem)
			{
				newItem.ProfileID = 1;
				db.Insert (newItem);
			}

			public List<ProfileItems> GetProfileItemsByProfileID (int profileId)
			{
				var Result = db.Query<ProfileItems> ("SELECT * FROM ProfileItems WHERE ProfileID='" + profileId + "'");

				return Result;

			}

			public void InsertNewTemporaryMediaItem (TemporaryItemMedia item)
			{
				db.Insert (item);
			}

			public void DeleteAllTemporaryMediaItems ()
			{
				db.Query<TemporaryItemMedia> ("DELETE * FROM TemporaryItemMedia");
			}

			public List<TemporaryItemMedia> ReturnAllTemporaryMediaItems ()
			{
				var Result = db.Query<TemporaryItemMedia> ("SELECT * FROM TemporaryItemMedia");

				return Result;
			}

			public readonly IList<string> colors = new List<string> {
				"Algemeen",
				"Medicijngebruik",
				"Allergieën",
				"Contactgegevens",
				"Hulpmiddelen",
				"Verzorging"
			};
			public IList<string> GetPickerItems = new List<string> {
				"Algemeen",
				"Medicijngebruik",
				"Allergieën",
				"Contactgegevens",
				"Hulpmiddelen",
				"Verzorging"
			};

		private async void Store(NoteModel note) {
			var dbNote = new Notes {
				Text = note.Text,
				Media = new List<NoteMediaModel>()
			};

			db.Insert (dbNote);

			foreach (var media in note.Media) {
				string url;
				var extension = Path.GetExtension("../Documents/"+media.Name);
				if (extension.StartsWith("."))
				{
					extension = extension.Substring(1);
				}

				using (var httpClient = new HttpClient())
				{
					using (var fileStream = File.OpenRead("../Documents/"+media.Name))
					{
						var content = new StreamContent(fileStream);
						content.Headers.Add("Content-Type", "video/mp4");
						content.Headers.Add("x-ms-blob-type", "BlockBlob");

						using (var uploadResponse = await httpClient.PutAsync(media.Location, content))
						{

							var request = new RestRequest("api/dossier/1/notes/{noteId}/media/{id}", Method.PUT);
							request.RequestFormat = DataFormat.Json;
							request.AddUrlSegment("noteId", note.Id.ToString());
							request.AddUrlSegment("id", media.Id.ToString());
							request.AddBody(media);

							var resp = client.Execute<NoteMediaModel>(request);
							dbNote.Media.Add (resp.Data);
							db.Update (dbNote);


						}
					}
				}
			}

		}

	}
}

