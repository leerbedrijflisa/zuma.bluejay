using System;
using System.Drawing;
using System.Threading.Tasks;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

using Xamarin.Media;
using MonoTouch.AssetsLibrary;
using System.IO;
using Lisa.Zuma.BlueJay.IOS.Data;

namespace Lisa.Zuma.BlueJay.IOS.Models
{
	public class Camera
	{

		private UploadVideo uploadVideo;

		public void Capture(string date){

			var picker = new MediaPicker ();

			if (!picker.IsCameraAvailable)
				Console.WriteLine("No camera!");
			else {
				picker.TakeVideoAsync (new StoreVideoOptions {
					Name = date+".m4v",
					Directory = "TemporaryFiles"
				}).ContinueWith (t => {
					var message = t;

					if (message.IsCanceled) {
						Console.WriteLine("User cancelled");
						return;
					}

					ALAssetsLibrary library = new ALAssetsLibrary();                   
					library.WriteVideoToSavedPhotosAlbum(new NSUrl(message.Result.Path), (assetURL, error) => {
						uploadVideo = new UploadVideo();
						uploadVideo.Store(new NoteMediaModel{Id = 1, Location = message.Result.Path}, message.Result.Path, 1);
				
						File.Delete(message.Result.Path);
					});

				}, TaskScheduler.FromCurrentSynchronizationContext());

			}
		}
	}
}

