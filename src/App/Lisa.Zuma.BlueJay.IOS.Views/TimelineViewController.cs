using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Lisa.Zuma.BlueJay.IOS.Models;

namespace Lisa.Zuma.BlueJay.IOS
{
	public partial class TimelineViewController : UIViewController
	{
		private TemplateParser templateParser;

		public TimelineViewController () : base ("TimelineViewController", null)
		{
			templateParser = new TemplateParser ();
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

			var ParsedHTML = templateParser.ParseTimelineFromDosier(1);

			wvTimeline.LoadHtmlString(ParsedHTML, null);

			wvTimeline.ScalesPageToFit = true;
			
			// Perform any additional setup after loading the view, typically from a nib.
		}
	}
}

