using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Lisa.Zuma.BlueJay.IOS.Models;
using System.IO;
using GCDiscreetNotification;

namespace Lisa.Zuma.BlueJay.IOS
{
	public partial class TimelineViewController : UIViewController
	{
		private HTMLTemplates templateParser;
		private UIView label;
		UIView OverlayView;
		private DataHelper dataHelper;
		private GCDiscreetNotificationView notificationView; 

		public TimelineViewController () : base ("TimelineViewController", null)
		{
			templateParser = new HTMLTemplates ();
			dataHelper = new DataHelper ();
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



//			Console.WriteLine (ParsedHTML);


			btnNewNote.TouchUpInside += (sender, e) => {
				NewNote();
			};

			btnEditProfile.TouchUpInside +=  (sender, e) => {

				ProfileViewController profileViewController = new ProfileViewController();
				this.NavigationController.PushViewController(profileViewController, true);

			};

			dataHelper.SyncNotesByID (1, () => {
				updateList ();
			});

			// Perform any additional setup after loading the view, typically from a nib.

			btnRefresh.TouchUpInside += (sender, e) => {
				updateList ();
			};
		}

		public void updateList()
		{
			var ParsedHTML = templateParser.ParseTimeLine(1);

			string contentDirectoryPath = Path.Combine (NSBundle.MainBundle.BundlePath, "HTML/");
			InvokeOnMainThread (() => {

				wvTimeline.LoadHtmlString(ParsedHTML, new NSUrl(contentDirectoryPath, true));

				wvTimeline.ScalesPageToFit = true;

			});

		}


		public void NewNote()
		{
			NewNoteViewController newNoteViewController = new NewNoteViewController(this);

			label = new UIButton ();
			label.Frame = new RectangleF (0, 0, this.View.Frame.Width, this.View.Frame.Height);
			label.BackgroundColor = UIColor.Black;
			label.Alpha = 0.5f;
			View.Add (label);

			OverlayView = new UIView ();
			OverlayView.Frame = new RectangleF (this.View.Frame.Height / 2 - 400, this.View.Frame.Width / 2 - 300, 400, 600);
			View.Add (OverlayView);

			OverlayView.Add (newNoteViewController.View);

			this.NavigationController.SetNavigationBarHidden (false, true);
		}

		public void hideNoteCreator()
		{
			label.Hidden = true;
			this.NavigationController.SetNavigationBarHidden (false, true);
		}

		public void ShowLoadingBar()
		{
			notificationView = new GCDiscreetNotificationView (
				text: "Note plaatsen...",
				activity: false,
				presentationMode: GCDNPresentationMode.Bottom,
				view: View
				);

			notificationView.Show (animated: true);
		}

		public void HideLoadingBar()
		{
			notificationView.Hide (animated: true);
		}

		
		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			this.NavigationController.SetNavigationBarHidden (true, true);
			updateList ();
		}
	}
}

