using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Lisa.Zuma.BlueJay.IOS.Models;
using System.IO;
using Xamarin.Media;
using System.Threading.Tasks;

namespace Lisa.Zuma.BlueJay.IOS
{
	public partial class NewNoteViewController : UIViewController
	{
		public NewNoteViewController (TimelineViewController thisView) : base ("NewNoteViewController", null)
		{
			dataHelper = new DataHelper ();
			parentView = thisView;
			camera = new Camera();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			InitializeUI ();

			btnSave.TouchUpInside += SaveNoteData;
			btnCamera.TouchUpInside += TakeVideo;
			btnPhotoCamera.TouchUpInside += TakePhoto;
			btnPickVideo.TouchUpInside += PickVideo;
			btnPickPhoto.TouchUpInside += PickImage;

		}

		private void InitializeUI()
		{
			var width = 60;
			var height = 60;

			txtInput = new UITextView();
			txtInput.Frame = new RectangleF(30, 70, width, height);
			txtInput.BackgroundColor = UIColor.FromRGB(242, 242, 242);

			View.Add(txtInput);
		}

		private void SaveNoteData(Object sender, EventArgs args)
		{
			parentView.NavigationController.PushViewController(new TimelineViewController(), false);
			dataHelper.SetNewNote (txtInput.Text);
		}

		private void TakeVideo(Object sender, EventArgs args)
		{
			camera.CaptureVideo(String.Format("{0:d-M-yyyy-HH-mm-ss}", DateTime.Now));
		}

		private void TakePhoto(Object sender, EventArgs args)
		{
			camera.CapturePhoto(String.Format("{0:d-M-yyyy-HH-mm-ss}", DateTime.Now));
		}

		private void PickImage(Object sender, EventArgs args)
		{
			camera.PickPhotoAsync ();
		}

		private void PickVideo(Object sender, EventArgs args)
		{
			camera.PickVideoAsync();
		}

		private TimelineViewController timeLineViewController;
		private TimelineViewController parentView;
		private DataHelper dataHelper;
		private UITextView txtInput;
		private Camera camera;

	}
}

