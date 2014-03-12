using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using  Lisa.Zuma.BlueJay.IOS.Models;
using System.IO;
using Xamarin.Media;
using System.Threading.Tasks;

namespace Lisa.Zuma.BlueJay.IOS
{
	public partial class NewNoteViewController : UIViewController
	{
		private TimelineViewController timeLineViewController;
		private TimelineViewController parentview;
		private DataHelper dataHelper;
		private UIImagePickerController imagePicker;

		public NewNoteViewController (TimelineViewController thisview) : base ("NewNoteViewController", null)
		{
			timeLineViewController = new TimelineViewController ();
			dataHelper = new DataHelper ();
			parentview = thisview;
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			UITextView inputText = new UITextView ();
			inputText.Frame = new RectangleF(30, 70, this.View.Frame.Width - 60, this.View.Frame.Width - 60);
			inputText.BackgroundColor = UIColor.FromRGB (242,242,242);
			View.Add (inputText);

			imagePicker = new UIImagePickerController ();

			btnSave.TouchUpInside += (sender, e) => {
				parentview.NavigationController.PushViewController(timeLineViewController, false);
				dataHelper.SetNewNote(inputText.Text);
			};


			btnCamera.TouchUpInside += (sender, e) => {
				Camera camera = new Camera();

				camera.Capture(DateTime.Today.ToString());
			};

			btnPickVideo.TouchUpInside += (sender, e) => {

				string FileName = String.Format("{0:d-M-yyyy-HH-mm-ss}", DateTime.Now) +".mp4";
				string excualPath = "../Documents/"+FileName;

				var picker = new MediaPicker();
				picker.PickVideoAsync().ContinueWith (t => {
					MediaFile file = t.Result;
					using (var f = file.GetStream() ){
						using(var dest = File.Create(excualPath)){
							f.CopyTo(dest);
							var kaas = File.Exists(excualPath);
							System.Diagnostics.Debug.WriteLine("kaas? " + kaas.ToString());
							dataHelper.InsertNewDataElement(1, excualPath);
						}
					}
				}, TaskScheduler.FromCurrentSynchronizationContext());
			};

		}

	}
}

