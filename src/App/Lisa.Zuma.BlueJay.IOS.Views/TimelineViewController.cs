using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Lisa.Zuma.BlueJay.IOS
{
	public partial class TimelineViewController : UIViewController
	{
		public TimelineViewController () : base ("TimelineViewController", null)
		{
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

			string url = "http://maikeldehoog.nl/timeline";
			wvTimeline.LoadRequest(new NSUrlRequest(new NSUrl(url)));

			wvTimeline.ScalesPageToFit = true;
			
			// Perform any additional setup after loading the view, typically from a nib.
		}
	}
}

