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
		}

		public void UpdateButtonNumber()
		{
			var count = dataHelper.SummaryItemsCount();

			var buttonTitle = String.Format ("Er zijn {0} mediaitems toegevoegd...", count);
			btnMediaSummary.SetTitle (buttonTitle, UIControlState.Normal);
		}

		public override void ViewDidAppear (bool animated)
		{
			UpdateButtonNumber();
		}


		private void InitializeUI()
		{

			btnSave.TouchUpInside += SaveNoteData;
			btnPickVideo.TouchUpInside += PickVideo;
			btnPickPhoto.TouchUpInside += PickImage;

			UIPopoverController listOfEmbeddedMediaListPopOver = new UIPopoverController(new MediaSummaryViewController());

			btnMediaSummary.TouchUpInside += (sender, e) => {
				listOfEmbeddedMediaListPopOver.PopoverContentSize = new SizeF (450f, 420f);
				listOfEmbeddedMediaListPopOver.PresentFromRect (btnMediaSummary.Frame, this.View, UIPopoverArrowDirection.Down, true);
				UpdateButtonNumber();
			};   

			txtInput = new UITextView();
			txtInput.Frame = new RectangleF(20, 20, 380, 320);
			txtInput.BackgroundColor = UIColor.FromRGB(242, 242, 242);
			txtInput.Text = "Voer hier tekst in...";
			View.Add(txtInput);
		}

		private void SaveNoteData(Object sender, EventArgs args)
		{
			dataHelper.OnNotePosted += () => {
				parentView.NavigationController.PushViewController(new TimelineViewController(), false);
			};

			dataHelper.SetNewNote (txtInput.Text);
		}


		private void PickImage(Object sender, EventArgs args)
		{
			camera.PickPhotoAsync (UpdateButtonNumber);
		}

		private void PickVideo(Object sender, EventArgs args)
		{
			camera.PickVideoAsync(UpdateButtonNumber);
		}
			
		private TimelineViewController parentView;
		private DataHelper dataHelper;
		private UITextView txtInput;
		private Camera camera;

	}
}

