using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Lisa.Zuma.BlueJay.IOS
{
	public partial class NewNoteViewController : UIViewController
	{
		public NewNoteViewController () : base ("NewNoteViewController", null)
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

			UITextView inputText = new UITextView ();
			inputText.Frame = new RectangleF( 100, 200, 400, 600 );
			inputText.BackgroundColor = UIColor.Gray;
			View.Add (inputText);
		}
	}
}

