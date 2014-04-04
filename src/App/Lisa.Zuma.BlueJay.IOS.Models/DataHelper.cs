using System;
using Lisa.Zuma.BlueJay.IOS.Data;
using System.Collections.Generic;
using System.IO;
using Lisa.Zuma.BlueJay.Models;
using RestSharp;
using Newtonsoft.Json;
using System.Net.Http;
using System.Linq;
using System.Net;
using Newtonsoft.Json.Linq;

namespace Lisa.Zuma.BlueJay.IOS.Models
{
	public class DataHelper
	{
		public DataHelper ()
		{
			database = new Database ();
			client = new RestClient ("http://zumabluejay-test.azurewebsites.net");
		}

		public void SignIn(string username, string password, Action kaas)
		{
			client.Authenticator = new SimpleAuthenticator("username", username, "password", password);

			var request = new RestRequest("token", Method.POST);
			request.AddParameter ("grant_type", "password");
			client.ExecuteAsync(request, response =>
				{
					if (response.StatusCode == HttpStatusCode.OK)
					{
						var jsonResponse = JsonConvert.DeserializeObject<signInRequestInformation>(response.Content);
						database.Clear("UserData");
						database.Insert(new UserData{ Name = jsonResponse.userName, AccesToken = jsonResponse.access_token });
						kaas();
					}
					else
					{
						Console.WriteLine("nok");
					}
				});
		}

		public void SetNewNote (string text)
		{
			var note = new Note{Text = text, DateCreated = DateTime.Now, Media = GetAllDataElements()};
			//var user = database.GetCurrentUser ();
			var request = new RestRequest (string.Format("api/dossier/{0}/Notes/", 1), Method.POST);
			request.AddHeader  ("Authorization", "bearer "+ database.accessToken);
			request.RequestFormat = DataFormat.Json;
			request.AddBody(note);

			client.ExecuteAsync(request, response => {
				Console.WriteLine("klaar :"+ response.Content);
				var callback = JsonConvert.DeserializeObject<Note>(response.Content);

				Store(callback,  () => { DeleteAllDataElements(); });
			});
		}

		private async void Store(Note note, Action AsynFunc) {

			var dbNote = new NotesData 
			{
				Text = note.Text,
				Media = new List<Media>()
			};

			database.Insert(dbNote);

			foreach (var media in note.Media) 
			{
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
						content.Headers.Add("Content-Type", MimeTypesHelper.MimeTypes[extension]);
						content.Headers.Add("x-ms-blob-type", "BlockBlob");

						using (var uploadResponse = await httpClient.PutAsync(media.Location, content))
						{

							var request = new RestRequest("api/dossier/1/Notes/{noteId}/media/{id}", Method.PUT);
							request.RequestFormat = DataFormat.Json;
							request.AddUrlSegment("noteId", note.Id.ToString());
							request.AddUrlSegment("id", media.Id.ToString());		
							request.AddHeader("Authorization", "bearer "+ database.accessToken);
							request.AddBody(media);

							var resp = client.Execute<NoteMedia>(request);
							var noteMedia = new Media 
							{
								Name = resp.Data.Name,
								Location = resp.Data.Location
							};

							dbNote.Media.Add (noteMedia);
							database.Update(dbNote);


						}
					}
				}
			}
			AsynFunc ();
		}

		public IList<string> picker()
		{
			return database.GetPickerItems;
		}

		public void SyncNotesDataByID(int id, Action DoSomething)
		{
			SyncAllNotesDataFromDosierData(id, ()=>{
					DoSomething();
			});
		}

		public void SyncAllNotesDataFromDosierData(int dosier, Action AsyncFunc){

			database.DeleteAllNotesForSync();

			var request = new RestRequest (string.Format("api/dossier/{0}/Notes/", dosier), Method.GET);
			request.AddHeader  ("Authorization", "bearer "+ database.accessToken);
			client.ExecuteAsync (request, response => {

				var callback = JsonConvert.DeserializeObject<List<Note>> (response.Content);

				callback
					.Select (n => new NotesData () {
					DosierDataID = dosier, 
					OwnerID = 1, 
					Text = n.Text, 
					Date = n.DateCreated,
					Media = n.Media
					
								.Select (m => new Media () { 
						Name = m.Name, 
						Location = m.Location 
					})
								.ToList ()
				})
					.ToList ()
					.ForEach (n => {
					database.Insert (n);
				});
				AsyncFunc ();

			});

//				foreach(var Result in callback){
//
//
//					var note = new NotesData{DosierDataID = dosier, OwnerID = 1, Text = Result.Text, Media = Result.Media};
//					database.Insert(note);
//				}

		}

		public void InsertNewDataElement(int type, string path)
		{
			database.InsertNewTemporaryMediaItem (new TemporaryItemMediaData{Type = type, fileName = Path.GetFileName(path), Path = path});
		}

		public List<DosierData> GetDosierDatas()
		{
			return database.GetAllDosierDatas ();
		}
	
		public List<ProfileItemsData> GetProfileItems()
		{
			return database.GetProfileItemsByProfileID (1);
		}

		public void DeleteAllDataElements()
		{
			database.DeleteAllTemporaryMediaItems();
		}

		public void InsertProfileItem(string title, string content)
		{
			database.InsertProfileItem (new ProfileItemsData{Title = title, Content = content});
		}

		public List<TemporaryItemMediaData> GetSummaryOfMediaItems()
		{
			return database.ReturnAllTemporaryMediaItems ();
		}

		public int SummaryItemsCount()
		{
			return database.ReturnAllTemporaryMediaItems ()
						   .Count;
		}

		private List<NoteMedia> GetAllDataElements()
		{
			List<NoteMedia> MediaModel = new List<NoteMedia> ();

			foreach(var x in database.ReturnAllTemporaryMediaItems()){
				MediaModel.Add (new NoteMedia { Name = x.fileName });
			}
		
			return MediaModel;
		}


		private Database database;
		private RestClient client;
	}
}

