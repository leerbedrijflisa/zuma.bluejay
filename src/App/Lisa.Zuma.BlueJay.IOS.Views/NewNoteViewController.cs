using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Lisa.Zuma.BlueJay.IOS.Models;
using System.IO;
using Xamarin.Media;
using System.Threading.Tasks;

namespace Lisa.Zuma.BlueJay.IOS.Views
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

			UIPopoverController myPopOver = new UIPopoverController(new MediaSummaryViewController()); 

			btnMediaSummary.TouchUpInside += (sender, e) => {
				myPopOver.PopoverContentSize = new SizeF (450f, 420f);
				myPopOver.PresentFromRect (btnMediaSummary.Frame, this.View, UIPopoverArrowDirection.Down, true);
				UpdateButtonNumber();
			};   

			txtInput = new UITextView();
			txtInput.Frame = new RectangleF(20, 70, 380, 350);
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
			camera.CaptureVideo(String.Format("{0:d-M-yyyy-HH-mm-ss}", DateTime.Now), () => UpdateButtonNumber ());
		}

		private void TakePhoto(Object sender, EventArgs args)
		{
			camera.CapturePhoto(String.Format("{0:d-M-yyyy-HH-mm-ss}", DateTime.Now), () => UpdateButtonNumber ());
		}

		private void PickImage(Object sender, EventArgs args)
		{
			camera.PickPhotoAsync (() => {
				UpdateButtonNumber ();
			});

		}

		private void PickVideo(Object sender, EventArgs args)
		{
			camera.PickVideoAsync(() => UpdateButtonNumber ());
		}

		public void UpdateButtonNumber()
		{
			var count = dataHelper.SummaryItemsCount ();

			var buttonTitle = String.Format ("Er zijn {0} mediaitems toegevoegd...", count);
			btnMediaSummary.SetTitle (buttonTitle, UIControlState.Normal);
		}

		public override void ViewDidAppear (bool animated)
		{
			UpdateButtonNumber ();
		}
			
		private TimelineViewController parentView;
		private DataHelper dataHelper;
		private UITextView txtInput;
		private Camera camera;

	}
}

