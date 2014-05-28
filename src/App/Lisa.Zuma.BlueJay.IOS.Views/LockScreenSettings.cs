using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Lisa.Zuma.BlueJay.IOS.Views
{
	public partial class LockScreenSettings : UIViewController
	{
		public LockScreenSettings () : base ("LockScreenSettings", null)
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

//			swLockScreen.TouchUpInside += (sender, e) => {
//				Console.WriteLine(swLockScreen.On.ToString());
//			};

//			btnSetNewLockscreen.TouchUpInside += (sender, e) => {
////				sum.NavigationController.PopViewControllerAnimated(true);
//				this.View.Frame = new RectangleF(0, 0, View.Frame.Width + 20f, View.Frame.Height + 20f);
//
//			};
//


		}
	}
}

