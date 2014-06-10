using System;
using Lisa.Zuma.BlueJay.IOS.Data;
using System.Collections.Generic;
using System.IO;
using RestSharp;
using Newtonsoft.Json;
using System.Net.Http;
using System.Linq;
using System.Net;
using Newtonsoft.Json.Linq;
using MonoTouch.UIKit;
using Lisa.Zuma.BlueJay.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Lisa.Zuma.BlueJay.IOS.Models
{
	public class DataHelper
	{
		public delegate void NotePostedEventHandler();
		public event NotePostedEventHandler OnNotePosted;

		public DataHelper ()
		{
			database = new Database ();
			client = new RestClient ("http://zumabluejay-apitest.azurewebsites.net");
		}

		public void SignIn(string username, string password, Action SuccessFunction, Action FailFunction)
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
						database.AccessToken();

						//
						SyncDossiers(SuccessFunction);
					}
					else
					{
						FailFunction();
					}
				});
		}
			
		public void SetNewNote (string text)
		{
			var note = new Note{Text = text, DateCreated = DateTime.Now, Media = GetAllDataElements()};
			if (note.Media.Count > 0 || !string.IsNullOrEmpty (note.Text))
			{
				//var user = database.GetCurrentUser ();
				var request = new RestRequest (string.Format ("api/dossier/{0}/Notes/", database.getCurrentDossier()), Method.POST);
				request.AddHeader ("Authorization", "bearer " + database.accessToken);
				request.RequestFormat = DataFormat.Json;
				request.AddBody (note);

				var response = client.Execute<Note>(request);
				Store (response.Data);
				DeleteAllDataElements ();

				if (OnNotePosted != null)
				{

					OnNotePosted();
				}

			}
			else
			{
				new UIAlertView("Leeg bericht", "Je probeert een leeg bericht te plaatsen, dit is niet toegestaan !"
					, null, "Begrepen !", null).Show();
			}
		}

		private void Store(Note note)
		{
			var dbNote = new NotesData (note.Text);
			database.Insert (dbNote);
			StoreMediaWithNote (note, dbNote);
		}

		private void StoreMediaWithNote(Note note, NotesData dbNote)
		{
			foreach (var media in note.Media) 
			{
				var extension = Path.GetExtension("../Documents/" + media.Name);
				extension = extension.Substring(1);		// remove the period from the extension
			
				using (var uploadResponse = StoreFileContentInAzure("../Documents/" + media.Name, media.Location, MimeTypesHelper.MimeTypes[extension]))
				{
					var noteMedia = RequestMediaUrl(note.Id, media);
					var mediaData = new Media 
					{
						mediaId = noteMedia.Id,
						Name = noteMedia.Name,
						Location = noteMedia.Location
					};

					dbNote.Media.Add(mediaData);
					database.Update(dbNote);
				}
			}
		}

		private NoteMedia RequestMediaUrl(int noteId, NoteMedia media)
		{
			var request = new RestRequest("api/dossier/{dosierId}/Notes/{noteId}/media/{id}", Method.PUT);
			request.RequestFormat = DataFormat.Json;
			request.AddUrlSegment("noteId", noteId.ToString());
			request.AddUrlSegment("dosierId", database.getCurrentDossier().ToString());
			request.AddUrlSegment("id", media.Id.ToString());		
			request.AddHeader("Authorization", "bearer "+ database.accessToken);
			request.AddBody(media);

			return client.Execute<NoteMedia>(request).Data;
		}

		private HttpResponseMessage StoreFileContentInAzure(string filePath, string location, string mimeType)
		{
			using (var fileStream = File.OpenRead(filePath))
			{
				using (var httpClient = new HttpClient ()) {
					var content = new StreamContent (fileStream);
					content.Headers.Add ("Content-Type", mimeType);
					content.Headers.Add ("x-ms-blob-type", "BlockBlob");

					return httpClient.PutAsync (location, content).Result;
				}
			}
		}

		public IList<string> picker()
		{
			return database.GetPickerItems;
		}

		public void AddDossierDetail(string category, string content, Action completed) {

			var detail = new DossierDetail () {
				Category = category,
				Contents = content
			};

			var request = new RestRequest ("api/dossier/{dossierId}/detail", Method.POST);
			request.RequestFormat = DataFormat.Json;
			request.AddHeader ("Authorization", string.Format ("bearer {0}", database.accessToken))
				.AddUrlSegment ("dossierId", database.getCurrentDossier ().ToString())
				.AddObject (detail);

			client.ExecuteAsync<DossierDetail> (request, response => {

				completed();
			});
		}

		public void SyncNotesDataByID(int id, Action DoSomething)
		{
			SyncAllNotesDataFromDosierData(id, ()=>{
					DoSomething();
			});
		}

		public void SyncDossiers(Action DoFunc)
		{
//			database.deleteDossiers ();

			var request = new RestRequest ("api/dossier/", Method.GET);
			request.AddHeader  ("Authorization", "bearer "+ database.accessToken);
			client.ExecuteAsync (request, response => {

				var callback = JsonConvert.DeserializeObject<List<Dossier>> (response.Content);

				database.deleteDossiers();
				database.Clear("ProfileItemsData");

				foreach(var dossiers in callback )
				{
					database.Insert(new DosierData{Name = dossiers.Name, DossierId = dossiers.Id});
					dossiers.Details
						.Select(d => new ProfileItemsData() {
							Title = d.Category,
							Content = d.Contents,
							ProfileID = d.Id,
							DossierDataID = dossiers.Id
						})
						.ToList()
						.ForEach(pi => database.InsertProfileItem(pi));
				}

				DoFunc();

			});
		}

		public void SyncAllNotesDataFromDosierData(int dosier, Action AsyncFunc){

			database.DeleteAllNotesForSync();

			var request = new RestRequest (string.Format("api/dossier/{0}/Notes/", getCurrentDossier()), Method.GET);
			request.AddHeader  ("Authorization", "bearer "+ database.accessToken);
			client.ExecuteAsync (request, response => {

				var callback = JsonConvert.DeserializeObject<List<Note>> (response.Content);
				if(callback != null){
					callback
						.Select (n => new NotesData () {
						DosierDataID = dosier, 
						OwnerID = 1, 
						Text = n.Text, 
						noteId = n.Id,
						Date = n.DateCreated,
						Media = n.Media
						
									.Select (m => new Media () { 
									mediaId = m.Id,
									Name = m.Name, 
							Location = m.Location 
						})
									.ToList ()
					})
						.ToList ()
						.ForEach (n => {
						database.Insert (n);
					});
				}
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

		public void insertNewCurrentDossier(int id)
		{
			database.setCurrentDossier (id);
		}

		public int getCurrentDossier()
		{
			return database.getCurrentDossier ();
		}
	
//		public List<ProfileItemsData> GetProfileItems()
//		{
//			return database.GetProfileItemsByProfileID (1);
//		}

		public IEnumerable<ProfileItemsData> GetProfileItems() {
			return database.GetProfileItemsByDossierId (getCurrentDossier ());
		}

		public string GetCurrentDossierDataName()
		{
			DosierData value =  database.GetCurrentDossier();

			return value.Name;
		}

		public void DeleteAllDataElements()
		{
			database.DeleteAllTemporaryMediaItems();
		}

		public void InsertProfileItem(string title, string content)
		{
			database.InsertProfileItem (new ProfileItemsData{Title = title, Content = content, DossierDataID = getCurrentDossier()});
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

		public void newCombination(string combination)
		{
			database.Update (new LockScreenData { IsActive = 1, SecurityCode = int.Parse (combination) });
		}

		public string token()
		{
			return database.accessToken;
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

