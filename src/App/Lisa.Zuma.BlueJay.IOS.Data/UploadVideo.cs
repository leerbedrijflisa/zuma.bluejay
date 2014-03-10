using System;
using System.IO;
using System.Net.Http;
using RestSharp;

namespace Lisa.Zuma.BlueJay.IOS.Data
{
	public class UploadVideo
	{
		public async void Store(NoteMediaModel media, string path, int noteId)
		{

			try{
				var extension = Path.GetExtension(path);
				if (extension.StartsWith("."))
				{
					extension = extension.Substring(1);
				}

				using (var httpClient = new HttpClient())
				{
					using (var fileStream = File.OpenRead(path))
					{
						var content = new StreamContent(fileStream);
						content.Headers.Add("Content-Type", "video/mp4");
						content.Headers.Add("x-ms-blob-type", "BlockBlob");

						using (var uploadResponse = await httpClient.PutAsync(media.Location, content))
						{
							var restClient = new RestClient("http://zumabluejay-test.azurewebsites.net/");
							var request = new RestRequest("api/dossier/1/notes/{noteId}/media/{id}", Method.PUT);
							request.RequestFormat = DataFormat.Json;
							request.AddUrlSegment("noteId", noteId.ToString());
							request.AddUrlSegment("id", media.Id.ToString());
							request.AddBody(media);

							var resp = restClient.Execute(request);

							Console.WriteLine("klaar");
						}
					}
				}
			}catch(Exception e){
				Console.WriteLine (e.Message);
			}
	}

	}

}

