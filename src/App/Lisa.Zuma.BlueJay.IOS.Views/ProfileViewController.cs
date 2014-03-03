using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Lisa.Zuma.BlueJay.IOS
{
	public partial class ProfileViewController : UIViewController
	{
		public ProfileViewController () : base ("ProfileViewController", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{

			base.DidReceiveMemoryWarning ();
			

		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			btnNewItem.TouchUpInside += (sender, e) => {
				NewProfileItemViewController newProfileItemViewController = new NewProfileItemViewController();
				this.NavigationController.PushViewController(newProfileItemViewController, true);
			};
		}
	}
}

