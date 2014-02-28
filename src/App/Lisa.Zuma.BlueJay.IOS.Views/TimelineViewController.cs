using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Lisa.Zuma.BlueJay.IOS.Models;
using System.IO;

namespace Lisa.Zuma.BlueJay.IOS
{
	public partial class TimelineViewController : UIViewController
	{
		private HTMLTemplates templateParser;
		private UIView label;
		UIView OverlayView;

		public TimelineViewController () : base ("TimelineViewController", null)
		{
			templateParser = new HTMLTemplates ();
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
			updateList ();

			this.NavigationItem.SetRightBarButtonItem(
				new UIBarButtonItem(UIBarButtonSystemItem.Add, (sender,args) => {
				NewNote();
			})
				, true);

			// Perform any additional setup after loading the view, typically from a nib.
		}

		public void updateList()
		{
			var ParsedHTML = templateParser.ParseTimeLine(1);

			string contentDirectoryPath = Path.Combine (NSBundle.MainBundle.BundlePath, "HTML/");

			wvTimeline.LoadHtmlString(ParsedHTML, new NSUrl(contentDirectoryPath, true));

			wvTimeline.ScalesPageToFit = true;
		}

		public void NewNote()
		{
			NewNoteViewController newNoteViewController = new NewNoteViewController(this);

			label = new UIView ();
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

		
		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			updateList ();
		}
	}
}

