using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Lisa.Zuma.BlueJay.IOS
{
	public partial class NewProfileItemViewController : UIViewController
	{
		public NewProfileItemViewController () : base ("NewProfileItemViewController", null)
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

			txtContent.Frame = new RectangleF(txtContent.Frame.Y, txtContent.Frame.X, txtContent.Frame.Width, 200);



			// Perform any additional setup after loading the view, typically from a nib.
		}
	}
}

