using System;
using System.Drawing;
using System.Threading.Tasks;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

using Xamarin.Media;
using MonoTouch.AssetsLibrary;
using System.IO;
using Lisa.Zuma.BlueJay.IOS.Data;
using Lisa.Zuma.BlueJay.IOS.Models;

namespace Lisa.Zuma.BlueJay.IOS
{
	public class Camera
	{
		private DataHelper dataHelper = new DataHelper();


		public void CaptureVideo(string date){

			var picker = new MediaPicker ();

			if (!picker.IsCameraAvailable)
				Console.WriteLine("No camera!");
			else {
				picker.TakeVideoAsync (new StoreVideoOptions {
					Name = date+".mp4",
					Directory = "TemporaryFiles"
				}).ContinueWith (t => {
					var message = t;

					if (message.IsCanceled) {
						Console.WriteLine("User cancelled");
						return;
					}

					Console.WriteLine(t.Result.Path);


					ALAssetsLibrary library = new ALAssetsLibrary();                   
					library.WriteVideoToSavedPhotosAlbum (new NSUrl(t.Result.Path), (assetUrl, error) =>{
						Console.WriteLine ("assetUrl:"+assetUrl);
					});


				}, TaskScheduler.FromCurrentSynchronizationContext());

			}
		}

		public void CapturePhoto(string date){
		
			var picker = new MediaPicker ();

			if (!picker.IsCameraAvailable)
				Console.WriteLine("No camera!");
			else {
				picker.TakePhotoAsync (new StoreCameraMediaOptions {
					Name = date+".png",
					Directory = "TemporaryFiles"
				}).ContinueWith (t => {
					var message = t;

					if (message.IsCanceled) {
						Console.WriteLine("User cancelled");
						return;
					}

					Console.WriteLine(t.Result.Path);

					var ImageTaked = UIImage.FromFile(t.Result.Path);

					ImageTaked.SaveToPhotosAlbum((image, error) => {});

//					ALAssetsLibrary library = new ALAssetsLibrary();                   
//					library.WriteImageToSavedPhotosAlbum(new NSUrl(t.Result.Path) as UIImage, new NSDictionary(), null, (assetUrl, error) =>{
//						Console.WriteLine ("assetUrl:"+assetUrl);
//					});


				}, TaskScheduler.FromCurrentSynchronizationContext());

			}

		}

		public void PickVideoAsync()
		{
			string FileName = String.Format("{0:d-M-yyyy-HH-mm-ss}", DateTime.Now) +".mp4";
			var documents =
				Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
			var excualPath = Path.Combine (documents, FileName);

			var picker = new MediaPicker();
			picker.PickVideoAsync().ContinueWith (t => {
				MediaFile file = t.Result;
				using (var f = file.GetStream() ){
					using(var dest = File.Create(excualPath)){
						f.CopyTo(dest);
						var kaas = File.Exists(excualPath);
						dataHelper.InsertNewDataElement(1, excualPath);
					}
				}
			}, TaskScheduler.FromCurrentSynchronizationContext());
		}

		public void PickPhotoAsync()
		{
			string FileName = String.Format("{0:d-M-yyyy-HH-mm-ss}", DateTime.Now) +".png";
			var documents =
				Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
			var excualPath = Path.Combine (documents, FileName);

			var picker = new MediaPicker();
			picker.PickPhotoAsync().ContinueWith (t => {
				MediaFile file = t.Result;
				using (var f = file.GetStream() ){
					using(var dest = File.Create(excualPath)){
						f.CopyTo(dest);
						var kaas = File.Exists(excualPath);
						dataHelper.InsertNewDataElement(1, excualPath);
					}
				}
			}, TaskScheduler.FromCurrentSynchronizationContext());
		}
	}
}

