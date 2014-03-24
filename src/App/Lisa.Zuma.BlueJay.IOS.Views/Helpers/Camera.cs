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

namespace Lisa.Zuma.BlueJay.IOS
{
	public class Camera
	{
		public Camera ()
		{
			dataHelper = new DataHelper ();
		}

		public void CaptureVideo(string date){
			var picker = new MediaPicker ();
				picker.TakeVideoAsync (new StoreVideoOptions {
					Name = date+".mp4",
					Directory = "TemporaryFiles"
				}).ContinueWith (t => {
					ALAssetsLibrary library = new ALAssetsLibrary();                   
					library.WriteVideoToSavedPhotosAlbum (new NSUrl(t.Result.Path), (assetUrl, error) =>{});
				}, TaskScheduler.FromCurrentSynchronizationContext());
		}

		public void CapturePhoto(string date){
		
			var picker = new MediaPicker ();
		
			picker.TakePhotoAsync (new StoreCameraMediaOptions {
				Name = date+".png",
				Directory = "TemporaryFiles"
			}).ContinueWith (t => {
				var Image = UIImage.FromFile(t.Result.Path);
				Image.SaveToPhotosAlbum((image, error) => {});
			}, TaskScheduler.FromCurrentSynchronizationContext());
		}

		public void PickVideoAsync()
		{
			var picker = new MediaPicker();
			picker.PickVideoAsync().ContinueWith (t => SaveFileToMedialibrary(t.Result, ".mp4"), TaskScheduler.FromCurrentSynchronizationContext());
		}

		public void PickPhotoAsync()
		{
			var picker = new MediaPicker();
			picker.PickPhotoAsync().ContinueWith (t => SaveFileToMedialibrary(t.Result, ".png"), TaskScheduler.FromCurrentSynchronizationContext());
		}

		private void SaveFileToMedialibrary(MediaFile file, string ext)
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
		}

		private DataHelper dataHelper;
	}
}

