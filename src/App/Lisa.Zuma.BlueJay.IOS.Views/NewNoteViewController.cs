using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using  Lisa.Zuma.BlueJay.IOS.Models;

namespace Lisa.Zuma.BlueJay.IOS
{
	public partial class NewNoteViewController : UIViewController
	{
		private TimelineViewController timeLineViewController;
		private TimelineViewController parentview;
		private DataHelper dataHelper;

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

			btnSave.TouchUpInside += (sender, e) => {
				parentview.NavigationController.PushViewController(timeLineViewController, false);
				dataHelper.SetNewNote(inputText.Text, "");
			};


			btnCamera.TouchUpInside += (sender, e) => {
				Camera camera = new Camera();

				camera.Capture(DateTime.Today.ToString());
			};

			if(Reachability.InternetConnectionStatus == NetworkStatus.ReachableViaCarrierDataNetwork) {
				new UIAlertView("Offline modus", "De iPad is verbonden met een mobiel netwerk, hierdoor is filmpjes plaatsen niet aangeraden!", null, "ok", null).Show(); 
			}

		}
	}
}

