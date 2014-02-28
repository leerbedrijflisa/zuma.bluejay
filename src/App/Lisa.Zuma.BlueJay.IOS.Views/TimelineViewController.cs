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

			var ParsedHTML = templateParser.ParseTimeLine(1);

			string contentDirectoryPath = Path.Combine (NSBundle.MainBundle.BundlePath, "HTML/");

			wvTimeline.LoadHtmlString(ParsedHTML, new NSUrl(contentDirectoryPath, true));

			wvTimeline.ScalesPageToFit = true;

			Console.WriteLine (ParsedHTML);
			
			// Perform any additional setup after loading the view, typically from a nib.
		}
	}
}

