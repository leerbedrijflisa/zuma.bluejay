using System;
using System.Drawing;
using System.Threading.Tasks;
using System.IO;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.AssetsLibrary;

using Xamarin.Media;

using Lisa.Zuma.BlueJay.IOS.Data;
using Lisa.Zuma.BlueJay.IOS.Models;

namespace Lisa.Zuma.BlueJay.IOS.Views
{
	public class Camera
	{
		public Camera ()
		{
			dataHelper = new DataHelper ();
			mediaSummary = new MediaSummaryViewController ();
		}

		public void CaptureVideo(string date, Action Ready){

			var picker = new MediaPicker ();
				picker.TakeVideoAsync (new StoreVideoOptions {
					Name = date+".mp4",
					Directory = "TemporaryFiles"
				}).ContinueWith (t => {
					ALAssetsLibrary library = new ALAssetsLibrary();                   
					library.WriteVideoToSavedPhotosAlbum (new NSUrl(t.Result.Path), (assetUrl, error) =>{});
				SaveFileToMedialibrary(t.Result, ".mp4", () => Ready());
				}, TaskScheduler.FromCurrentSynchronizationContext());
		}

		public void CapturePhoto(string date, Action Ready){
		
			var picker = new MediaPicker ();
		
			picker.TakePhotoAsync (new StoreCameraMediaOptions {
				Name = date+".png",
				Directory = "TemporaryFiles"
			}).ContinueWith (t => {
				var Image = UIImage.FromFile(t.Result.Path);
				Image.SaveToPhotosAlbum((image, error) => {});
				SaveFileToMedialibrary(t.Result, ".png", () => Ready());
			}, TaskScheduler.FromCurrentSynchronizationContext());
		}

		public void PickVideoAsync( Action Ready)
		{
			var picker = new MediaPicker();
			picker.PickVideoAsync().ContinueWith (t => SaveFileToMedialibrary(t.Result, ".mp4", () => Ready()), TaskScheduler.FromCurrentSynchronizationContext());

		}

		public void PickPhotoAsync(Action Ready)
		{
			var picker = new MediaPicker();
			picker.PickPhotoAsync().ContinueWith (t => SaveFileToMedialibrary(t.Result, ".png", () => Ready()), TaskScheduler.FromCurrentSynchronizationContext());
		}

		private void SaveFileToMedialibrary(MediaFile file, string ext, Action ResultFunc)
		{
			var FileName = Guid.NewGuid()+ext;
			var documents = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
			var excualPath = Path.Combine (documents, FileName);

			using (var f = file.GetStream() ){
				using(var dest = File.Create(excualPath)){
					f.CopyTo(dest);
					dataHelper.InsertNewDataElement(1, excualPath);
				}
			}

			ResultFunc ();


		}

		private DataHelper dataHelper;
		private MediaSummaryViewController mediaSummary;
	}
}

